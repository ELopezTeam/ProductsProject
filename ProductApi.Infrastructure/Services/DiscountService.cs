using System.Text.Json;
using ProductApi.Domain.Entities;
using ProductApi.Domain.Interfaces;

namespace ProductApi.Infrastructure.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DiscountService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<int> GetDiscountAsync(int productId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync($"https://66d6071df5859a7042682cfa.mockapi.io/api/v1/products/discount/{productId}");
            var cleanedResponse = CleanJson(response);

            var discountResponse = JsonSerializer.Deserialize<Discount>(cleanedResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


            if (discountResponse == null || !int.TryParse(discountResponse.DiscountProduct, out var discount))
            {
                return 0;
            }

            return discount;
        }

        private string CleanJson(string json)
        {
            return json.Replace("\\\"", "\"").Replace("\\\\", "\\");
        }
    }
}
