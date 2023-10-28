
using FluentValidation;
using GVPB.Identity.Domain.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace GVPB.Identity.Domain.Validator;

public class RequestUserValidator : AbstractValidator<RequestUser>
{
    public RequestUserValidator()
    {
        RuleFor(e => e.Id)
           .NotNull()
           .NotEmpty()
           .WithMessage("Id is required.");

        RuleFor(e => e.CreationDate)
           .NotNull()
           .NotEmpty()
           .WithMessage("CreationDate is required.");

        RuleFor(e => e.Body)
            .Must(BeAValidBody)
            .WithMessage("The Body address is not valid.");
    }

    private bool BeAValidBody(string body)
    {
        try
        {
            var entity = JsonConvert.DeserializeObject<User>(body);

            if (entity != null)
            {
                return entity.IsValid;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

