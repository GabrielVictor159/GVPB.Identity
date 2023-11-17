
using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.UseCases.Login;

public class LoginRequest
{
    public required string UserName { get; init;}
    public required string Password { get; init;}
    public required ILanguageManager Localizer;
}

public class LoginComunications : IComunications
{
    public string Token { get; set; } = "";
    public User? User { get; set; }
    public IOutputPort<LoginResponse>? outputPort { get; set; }
}

