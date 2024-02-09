
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Validator;
using Microsoft.Extensions.Localization;

namespace GVPB.Identity.Domain.Models;

public class RequestUser : Entity<RequestUser, RequestUserValidator>
{
    public required Guid Id { get; init; }
    public required string Body { get; init; }
    public required DateTime CreationDate { get; init; }
    public required RequestUserType RequestUserType { get; init; }

    public RequestUser(ILanguageManager? Localizer = null)
        : base(new RequestUserValidator(Localizer))
    {
       
    }
}

