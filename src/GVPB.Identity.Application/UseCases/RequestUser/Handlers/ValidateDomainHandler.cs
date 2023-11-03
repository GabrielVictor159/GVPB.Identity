using FluentValidator;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Application.UseCases;
using GVPB.Identity.Domain.Enum;

namespace GVPB.Identity.Application;

public class ValidateDomainHandler : Handler<RequestUserRequest, RequestUserComunications>
{
    private readonly INotificationService notificationService;

    public ValidateDomainHandler(INotificationService notificationService)
    {
        this.notificationService = notificationService;
    }

    public override void ProcessRequest(RequestUserRequest request, RequestUserComunications? comunications)
    {
        comunications!.AddLog(LogType.Process, "Executing ValidateDomainHandler");
        if(!request.NewUser.IsValid)
        {
            notificationService.AddNotifications(request.NewUser.ValidationResult);
        }
        sucessor?.ProcessRequest(request,comunications);
    }
}
