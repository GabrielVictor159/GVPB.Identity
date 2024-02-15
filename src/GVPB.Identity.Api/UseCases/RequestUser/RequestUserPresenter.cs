using GVPB.Identity.Domain;

namespace GVPB.Identity.Api.UseCases.RequestUser;

public class RequestUserPresenter : Presenter<Application.Bundaries.RequestUserResponse, RequestUserResponse>
{
    public RequestUserPresenter(LanguageManager<SharedResources> languageService) : base(languageService)
    {
    }
}
