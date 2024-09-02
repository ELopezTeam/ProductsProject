using ProductApi.Application.DTOs;
using ProductApi.Application.Exceptions;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Domain.Interfaces;

namespace ProductApi.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICacheService _cacheService;
        private readonly IDiscountService _discountService;

        public ProductService(IProductRepository productRepository, ICacheService cacheService, IDiscountService discountService)
        {
            _productRepository = productRepository;
            _cacheService = cacheService;
            _discountService = discountService;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new ProductNotFoundException(id);
            }

            var statusName = _cacheService.GetStatusName(product.Status);
            var discount = await _discountService.GetDiscountAsync(id);

            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                StatusName = statusName,
                Stock = product.Stock,
                Description = product.Description,
                Price = product.Price,
                Discount = discount,
                FinalPrice = product.Price * (100 - discount) / 100
            };
        }

        public async Task AddAsync(CUProductDto productDto)
        {
            var product = new Product
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                Status = productDto.Status,
                Stock = productDto.Stock,
                Description = productDto.Description,
                Price = productDto.Price
            };

            await _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(CUProductDto productDto)
        {
            var product = new Product
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                Status = productDto.Status,
                Stock = productDto.Stock,
                Description = productDto.Description,
                Price = productDto.Price
            };

            await _productRepository.UpdateAsync(product);
        }
    }
}