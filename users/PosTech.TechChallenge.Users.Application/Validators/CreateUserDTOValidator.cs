using FluentValidation;

using PosTech.TechChallenge.Users.Application.DTOs;

namespace PosTech.TechChallenge.Users.Application.Validators;

public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
{
    public CreateUserDTOValidator()
    {
        RuleFor(user => user.UserName)
                .NotEmpty()
                .WithMessage("UserName is required.");

        RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Email is required.");

        RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage("Password is required.");

        RuleFor(user => user.RePassword)
                .NotEmpty()
                .WithMessage("RePassword is required.")
                .Must((user, repassword) => user.Password == repassword)
                .WithMessage("RePassword and Password do not match.");

    }
}
