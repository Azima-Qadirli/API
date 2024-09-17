using API06.App.DTOs.Category;
using FluentValidation;

namespace API06.App.Validations.Category;

public class CategoryPostDTOValidation:AbstractValidator<CategoryPostDto>
{
    public CategoryPostDTOValidation()
    {
        RuleFor(c=>c.Name)
            .NotNull().WithMessage("Item can not be null.")
            .NotEmpty().WithMessage("Item can not be empty.")
            .MaximumLength(30);
    }
}