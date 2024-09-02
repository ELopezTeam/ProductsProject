using System;
namespace ProductApi.Domain.Interfaces
{
	public interface IDiscountService
	{
        Task<int> GetDiscountAsync(int productId);

    }
}

