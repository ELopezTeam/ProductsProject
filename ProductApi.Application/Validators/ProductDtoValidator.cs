using System;
using FluentValidation;
using ProductApi.Application.DTOs;

namespace ProductApi.Application.Validators
{
    public class ProductDtoValidator : AbstractValidator<CUProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId must be greater than zero.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.");

            RuleFor(x => x.Status)
                .InclusiveBetween(0, 1).WithMessage("Status must be 0 or 1.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description can be up to 1000 characters long.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

        }
    }
}