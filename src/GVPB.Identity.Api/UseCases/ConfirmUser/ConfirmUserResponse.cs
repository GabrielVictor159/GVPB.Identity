using GVPB.Identity.Domain;

namespace GVPB.Identity.Api.UseCases.ConfirmUser;

public class ConfirmUserResponse
{
    public string Status { get; set; } = "";
    public string Message { get; set; } = "";
    public Guid IdUser { get; set; }

    public ConfirmUserResponse(Application.Bundaries.ConfirmUserResponse bundaries, LanguageManager<SharedResources> languageService)
    {
        this.Message = $"{languageService.GetKey("SUCESS_CONFIRMUSER")}";
        this.Status = "SUCESS";
        this.IdUser = bundaries.User.Id;
    }
}
