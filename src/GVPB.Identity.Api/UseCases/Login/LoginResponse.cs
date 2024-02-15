using GVPB.Identity.Domain;

namespace GVPB.Identity.Api.UseCases.Login;

public class LoginResponse
{
    public string Token { get; init; } = "";
    public LoginResponse(Application.Bundaries.LoginResponse bundaries, LanguageManager<SharedResources> languageService)
    {
        this.Token = bundaries.Token;
    }
}
