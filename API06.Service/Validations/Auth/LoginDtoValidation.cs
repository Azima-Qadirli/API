using API06.Service.DTOs.Auths;
using FluentValidation;

namespace API06.Service.Validations.Auth;

public class LoginDtoValidation:AbstractValidator<LoginDto>
{
    public LoginDtoValidation()
    {
        RuleFor(l=>l.Username)
            .NotEmpty().WithMessage("Username can not be empty")
            .NotNull().WithMessage("Username can not be null");
        RuleFor(l=>l.Password)
            .NotEmpty().WithMessage("Password can not be empty")
            .NotNull().WithMessage("Password can not be null");
    }   
}