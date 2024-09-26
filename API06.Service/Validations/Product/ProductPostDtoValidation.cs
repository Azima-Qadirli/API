using API06.Service.DTOs.Product;
using API06.Service.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace API06.Service.Validations.Product;

public class ProductPostDtoValidation:AbstractValidator<ProductPostDto>
{
    public ProductPostDtoValidation()
    {
        RuleFor(p=>p.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(p => p.Description)
            .NotEmpty()
            .NotNull()
            .MaximumLength(10000);
        RuleFor(p=>p.Price)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

        RuleFor(p => p.File).Custom((file, context) =>
        {
            if (!file.IsImage())
            {
                context.AddFailure((nameof(IFormFile)),"File is not an image");
            }

            if (!file.IsSizeOk(5))
            {
                context.AddFailure((nameof(IFormFile)),"File size must be maximum 2mb.");
            }
        });


    }
}