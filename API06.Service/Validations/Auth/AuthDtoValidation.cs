using API06.Service.DTOs.Auths;
using FluentValidation;

namespace API06.Service.Validations.Auth;

public class AuthDtoValidation:AbstractValidator<RegisterDto>
{
    public AuthDtoValidation()
    {
        RuleFor(a => a.UserName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(100);
        RuleFor(a => a.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();
        RuleFor(a => a.Password)
            .NotEmpty()
            .NotNull()
            .MaximumLength(20);
        RuleFor(a => a)
            .Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.Password), "Passwords don't match!!!Please try again!");
                }
            });
    }
}