
using FluentValidation;
using GVPB.Identity.Domain.Models;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace GVPB.Identity.Domain.Validator;

public class RequestUserValidator : LocalizatorValidator<RequestUserValidator, RequestUser>
{
    public RequestUserValidator(ILanguageManager? localizer = null)
    : base(localizer)
    {
    }
    protected override void Configure()
    {
        RuleFor(e => e.Id)
           .NotNull()
           .NotEmpty()
           .WithMessage(WithMessageLocalizer("IDREQUESTUSER","Id is required."));

        RuleFor(e => e.CreationDate)
           .NotNull()
           .NotEmpty()
           .WithMessage(WithMessageLocalizer("CREATIONDATEREQUESTUSER","CreationDate is required."));

        RuleFor(e => e.Body)
            .NotNull()
            .NotEmpty()
            .WithMessage(WithMessageLocalizer("BODYREQUESTUSER","The Body address is not valid."));
    }

 
}

