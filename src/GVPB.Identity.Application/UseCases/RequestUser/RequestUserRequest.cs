using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.UseCases.RequestUser;

public class RequestUserRequest
{
    public required User NewUser {get; init;}
    public string Culture { get; init; } = "en";

    public required ILanguageManager Localizer;
}

public class RequestUserComunications : IComunications
{
    public Domain.Models.RequestUser? requestUser {get; set;}
}
