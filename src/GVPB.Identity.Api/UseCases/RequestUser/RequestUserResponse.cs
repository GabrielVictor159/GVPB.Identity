using GVPB.Identity.Domain;

namespace GVPB.Identity.Api.UseCases.RequestUser;

public class RequestUserResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = "";

    public RequestUserResponse(Application.Bundaries.RequestUserResponse bundaries, LanguageManager<SharedResources> languageService)
    {
        this.Success = bundaries.Sucess;
        this.Message = $"{languageService.GetKey("SUCESS_REQUESTUSER")}";
    }
}
