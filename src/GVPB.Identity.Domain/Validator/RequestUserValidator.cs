
using FluentValidation;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Domain.Validator;

public class RequestUserValidator : AbstractValidator<RequestUser>
{
    public RequestUserValidator()
    {

    }
}

