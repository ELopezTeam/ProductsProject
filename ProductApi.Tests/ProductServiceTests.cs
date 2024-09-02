using System;
using ProductApi.Application.DTOs;
using ProductApi.Application.Services;
using ProductApi.Domain.Interfaces;
using Moq;
using ProductApi.Domain.Entities;

namespace ProductApi.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IDiscountService> _discountServiceMock;
        private readonly Mock<ICacheService> _cacheServiceMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _discountServiceMock = new Mock<IDiscountService>();
            _cacheServiceMock = new Mock<ICacheService>();
            _productService = new ProductService(_productRepositoryMock.Object, _cacheServiceMock.Object, _discountServiceMock.Object);
        }

        [Fact]
        public async Task AddProductAsync_ShouldAddProduct()
        {
            // Arrange
            var productDto = new CUProductDto { Name = "Test Product", Status = 1, Stock = 10, Description = "Test Product", Price = 100, ProductId = 6 };

            // Act
            await _productService.AddAsync(productDto);

            // Assert
            _productRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateProduct()
        {
            // Arrange
            var productDto = new CUProductDto { Name = "Test Product 1", Status = 1, Stock = 10, Description = "Test Product 1", Price = 10, ProductId = 6 };

            // Act
            await _productService.UpdateAsync(productDto);

            // Assert
            _productRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
