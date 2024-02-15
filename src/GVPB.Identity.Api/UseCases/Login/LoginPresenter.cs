using GVPB.Identity.Domain;

namespace GVPB.Identity.Api.UseCases.Login;

public class LoginPresenter : Presenter<Application.Bundaries.LoginResponse, LoginResponse>
{
    public LoginPresenter(LanguageManager<SharedResources> languageService) : base(languageService)
    {
    }
}
