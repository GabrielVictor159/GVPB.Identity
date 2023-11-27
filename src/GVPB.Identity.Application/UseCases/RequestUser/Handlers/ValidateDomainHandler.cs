using FluentValidator;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Application.UseCases;
using GVPB.Identity.Domain.Enum;

namespace GVPB.Identity.Application.UseCases.RequestUser.Handlers;

public class ValidateDomainHandler : Handler<RequestUserRequest, RequestUserComunications>
{
    private readonly INotificationService notificationService;

    public ValidateDomainHandler(INotificationService notificationService)
    {
        this.notificationService = notificationService;
    }

    protected override void ProcessRequest(RequestUserRequest request, RequestUserComunications? comunications)
    {
        if(!request.NewUser.IsValid)
        {
            notificationService.AddNotifications(request.NewUser.ValidationResult);
            Break();
            return;
        }

    }
}
