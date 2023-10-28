using FluentValidation;
using GVPB.Identity.Domain.Models;
using System.Text.RegularExpressions;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(e => e.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("Id is required.");

        RuleFor(e => e.UserName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(5)
            .WithMessage("Username is required and must be at least 5 characters long.");

        RuleFor(e => e.PasswordLength)
            .GreaterThan(5)
            .WithMessage("Password length must be greater than 5 characters.");

        RuleFor(e => e.Email)
            .NotNull()
            .NotEmpty()
            .Must(BeAValidEmail)
            .WithMessage("The email address is not valid.");

        RuleFor(e => e.Rule)
            .NotNull()
            .WithMessage("Rule is required.");
    }

    private bool BeAValidEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        return Regex.IsMatch(email, emailPattern);
    }
}
