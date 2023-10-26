
using FluentValidation;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Domain.Validator;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        
    }
}

