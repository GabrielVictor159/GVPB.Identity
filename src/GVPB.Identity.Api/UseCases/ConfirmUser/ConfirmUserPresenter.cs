using GVPB.Identity.Domain;

namespace GVPB.Identity.Api.UseCases.ConfirmUser;

public class ConfirmUserPresenter : Presenter<Application.Bundaries.ConfirmUserResponse, ConfirmUserResponse>
{
    public ConfirmUserPresenter(LanguageManager<SharedResources> languageService) : base(languageService)
    {
    }
}
