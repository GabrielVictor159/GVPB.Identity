
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;

namespace GVPB.Identity.Application.UseCases.ConfirmUser.Handlers;

public class GetRequestUserHandler : Handler<ConfirmUserRequest, ConfirmUserComunications>
{
    private readonly IRequestUserRepository requestUserRepository;
    private readonly INotificationService notificationService;

    public GetRequestUserHandler(IRequestUserRepository requestUserRepository, INotificationService notificationService)
    {
        this.requestUserRepository = requestUserRepository;
        this.notificationService = notificationService;
    }

    protected override void ProcessRequest(ConfirmUserRequest request, ConfirmUserComunications? comunications)
    {
        var requestUser = requestUserRepository.GetOne(request.Id);
        if(requestUser == null)
        {
            notificationService.AddNotification("Object Not found",$"");
            return;
        }

    }
}

