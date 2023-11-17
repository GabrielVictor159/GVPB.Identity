using GVPB.Identity.Application.Resources.Images.Factory;
using GVPB.Identity.Application.Resources.Images.Factory.Enum;
using GVPB.Identity.Application.UseCases;

namespace GVPB.Identity.Application;

public class SendEmailHandler : Handler<RequestUserRequest, RequestUserComunications>
{
    private readonly IEmailService emailService;

    public SendEmailHandler(IEmailService emailService)
    {
        this.emailService = emailService;
    }

    protected override void ProcessRequest(RequestUserRequest request, RequestUserComunications? comunications)
    {
        emailService.SendEmail
            (request.NewUser.Email, 
            request.Localizer.GetKey("REQUESTUSER_EMAILSUBJECT").Value,
            ApplicationImagesFactory.GetBase64Images(ImagesApplication.CONFIRM_USER,request.Culture)!,
            $"{Environment.GetEnvironmentVariable("URL_HOST")}/UserConfirmation/{comunications!.requestUser!.Id}");

    }
}
