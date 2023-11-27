
using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.UseCases.UpdateUser;

public class UpdateUserRequest
{
    public required Guid IdUser {get; init;}
    public string NewUserName {get; init;} = "";
    public string NewPassword {get; init;} = "";
    public required ILanguageManager localizer { get; init; }
}

public class UpdateUserComunications : IComunications
{
    public User? User {get; set;}
    public required IOutputPort<UpdateUserResponse> outputPort {get; init;}
}
