
using FluentValidation;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Domain.Validator;

public class LogValidator : AbstractValidator<Log>
{
    public LogValidator()
    {
        RuleFor(e => e.Id)
           .NotNull()
           .NotEmpty()
           .WithMessage("Id is required.");

        RuleFor(e => e.Message)
            .NotNull()
            .NotEmpty()
            .WithMessage("Message is required.");

        RuleFor(e => e.Type)
            .NotNull()
            .WithMessage("Type is required.");

        RuleFor(e => e.LogDate)
            .NotNull()
            .NotEmpty()
            .WithMessage("LogDate is required.");
    }
}

