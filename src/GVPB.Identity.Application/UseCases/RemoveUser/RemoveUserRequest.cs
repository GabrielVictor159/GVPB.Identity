using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.UseCases.RemoveUser;

public class RemoveUserRequest
{
    public required Guid IdUser {get; init;}
    public required ILanguageManager localizer { get; init; }
}

public class RemoveUserComunications : IComunications
{
    public required IOutputPort<RemoveUserResponse> OutputPort {get; init;} 
    public User? User {get; set;}
}
