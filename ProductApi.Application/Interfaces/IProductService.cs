using System;
using ProductApi.Application.DTOs;

namespace ProductApi.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task AddAsync(CUProductDto productDto);
        Task UpdateAsync(CUProductDto productDto);
    }
}

