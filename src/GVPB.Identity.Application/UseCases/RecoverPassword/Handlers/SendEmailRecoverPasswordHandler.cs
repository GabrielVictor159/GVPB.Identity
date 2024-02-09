using GVPB.Identity.Application.Resources.Images.Factory.Enum;
using GVPB.Identity.Application.Resources.Images.Factory;

namespace GVPB.Identity.Application.UseCases.RecoverPassword.Handlers;

public class SendEmailRecoverPasswordHandler : Handler<RecoverPasswordRequest, RecoverPasswordComunications>
{
    private readonly IEmailService emailService;

    public SendEmailRecoverPasswordHandler(IEmailService emailService)
    {
        this.emailService = emailService;
    }
    protected override void ProcessRequest(RecoverPasswordRequest request, RecoverPasswordComunications? comunications = null)
    {
        emailService.SendEmail
            (request.Email,
            request.localizer.GetKey("RecoverPassword_EmailSubject").Value,
            ApplicationImagesFactory.GetBase64Images(ImagesApplication.RECOVERPASSWORD, request.Culture)!,
            $"{Environment.GetEnvironmentVariable("URL_HOST")}/ResetPassword/{comunications!.requestUser!.Id}");
    }
}
