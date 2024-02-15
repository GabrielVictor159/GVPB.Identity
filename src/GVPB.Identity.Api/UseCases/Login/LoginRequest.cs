namespace GVPB.Identity.Api.UseCases.Login;

public class LoginRequest
{
    public required string UserNameOrUserEmail { get; init; } 
    public required string Password { get; init; } 
}
