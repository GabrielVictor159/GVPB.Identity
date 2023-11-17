using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application;

public class RequestUserRequest
{
    public required User NewUser {get; init;}
    public string Culture { get; init; } = "en";

    public required ILanguageManager Localizer;
}

public class RequestUserComunications : IComunications
{
    public RequestUser? requestUser {get; set;}
}
