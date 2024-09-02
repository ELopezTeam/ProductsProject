using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs;
using ProductApi.Application.Exceptions;
using ProductApi.Application.Interfaces;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IValidator<CUProductDto> _validator;


        public ProductsController(IProductService productService, IValidator<CUProductDto> validator)
        {
            _productService = productService;
            _validator = validator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var productDto = await _productService.GetByIdAsync(id);
                return Ok(productDto);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CUProductDto productDto)
        {
            var validationResult = await _validator.ValidateAsync(productDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                await _productService.AddAsync(productDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CUProductDto productDto)
        {
            var validationResult = await _validator.ValidateAsync(productDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                await _productService.UpdateAsync(productDto);
                return NoContent();
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}