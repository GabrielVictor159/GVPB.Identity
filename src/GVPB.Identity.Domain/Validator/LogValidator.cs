
using FluentValidation;
using GVPB.Identity.Domain.Models;
using Microsoft.Extensions.Localization;

namespace GVPB.Identity.Domain.Validator;

public class LogValidator : LocalizatorValidator<RequestUserValidator, Log>
{
    public LogValidator(ILanguageManager? localizer = null)
    : base(localizer)
    {
    }
    protected override void Configure()
    {
        RuleFor(e => e.Id)
           .NotNull()
           .NotEmpty()
           .WithMessage(WithMessageLocalizer("IDLOG","Id is required."));

        RuleFor(e => e.Message)
            .NotNull()
            .NotEmpty()
            .WithMessage(WithMessageLocalizer("LOGMESSAGE","Message is required."));

        RuleFor(e => e.Type)
            .NotNull()
            .WithMessage(WithMessageLocalizer("LOGTYPE","Type is required."));

        RuleFor(e => e.LogDate)
            .NotNull()
            .NotEmpty()
            .WithMessage(WithMessageLocalizer("LOGDATE","LogDate is required."));
    }
}

