using FluentValidation;
using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Models;
using GVPB.Identity.Domain.Validator;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

public class UserValidator : LocalizatorValidator<UserValidator, User>
{
    public UserValidator(ILanguageManager? localizer = null)
    : base(localizer)
    {
    }
    protected override void Configure()
    {
         RuleFor(e => e.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage(WithMessageLocalizer("IDUSER","Id Not Defined"));

        RuleFor(e => e.UserName)
            .NotNull()
            .NotEmpty()
            .WithMessage(WithMessageLocalizer("USERNAMEEMPTY","Email Not Valid"))
            .MinimumLength(5)
            .WithMessage(WithMessageLocalizer("USERNAMELENGTH","UserName Not Defined"));

        RuleFor(e => e.PasswordLength)
            .GreaterThan(5)
            .WithMessage(WithMessageLocalizer("USERPASSWORD","PasswordLength Must be greater than 5"));

        RuleFor(e => e.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage(WithMessageLocalizer("USEREMAILEMPTY","Email Not Valid"))
            .Must(BeAValidEmail)
            .WithMessage(WithMessageLocalizer("USEREMAILNOTDEFINID","Email Not Valid"));

        RuleFor(e => e.Rule)
            .NotNull()
            .WithMessage(WithMessageLocalizer("USERRULE","Rule Not Valid"));
    }
    private bool BeAValidEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        return Regex.IsMatch(email, emailPattern);
    }
}
