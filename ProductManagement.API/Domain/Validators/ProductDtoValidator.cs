using System.Data;
using FluentValidation;
using ProductManagement.API.Domain.Models;

namespace ProductManagement.API.Domain.Validators
{
    public class ProductDtoValidator:AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty();
            RuleFor(p => p.CategoryName).NotNull().NotEmpty();
            RuleFor(p => p.Quantity).NotNull();
            RuleFor(p => p.Price).NotNull().NotEmpty();
        }
    }
}