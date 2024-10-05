using API06.Service.DTOs.Blog;
using API06.Service.Extensions;
using FluentValidation;

namespace API06.Service.Validations.Blog;

public class BlogPostDtoValidation:AbstractValidator<BlogPostDto>
{
    public BlogPostDtoValidation()
    {
        RuleFor(b=>b.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50);
        RuleFor(b => b.Description)
            .NotEmpty()
            .NotNull()
            .MaximumLength(10000);
        RuleFor(b => b)
            .Custom((b, context) =>
            {
                if (!b.File.IsImage())
                {
                    context.AddFailure("File", "File is not an image");
                }
                if (!b.File.IsSizeOk(5))
                {
                    context.AddFailure("File", "File size cannot exceed 5 mb");
                }
            });
    }
}