
using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.UseCases.ConfirmUser;

public class ConfirmUserRequest
{
    public required Guid Id { get; init; }
    public required ILanguageManager localizer { get; init; }
}

public class ConfirmUserComunications : IComunications
{
    public RequestUser? requestUser { get; set; }
    public User? user { get; set; }  
    public required IOutputPort<ConfirmUserResponse> outputPort { get; init; }
}

