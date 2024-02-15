
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Domain.Models;
using Newtonsoft.Json;

namespace GVPB.Identity.Application.UseCases.ConfirmUser.Handlers;

public class CreateUserHandler : Handler<ConfirmUserRequest, ConfirmUserComunications>
{
    private readonly IUserRepository userRepository;
    private readonly INotificationService notificationService;

    public CreateUserHandler
        (IUserRepository userRepository, 
        INotificationService notificationService)
    {
        this.userRepository = userRepository;
        this.notificationService = notificationService;
    }

    protected override void ProcessRequest(ConfirmUserRequest request, ConfirmUserComunications? comunications)
    {
       var user = JsonConvert.DeserializeObject<User>(comunications!.requestUser!.Body);
        comunications.user = user;
        var searchEmail = userRepository.GetByFilter(e => e.Email == user!.Email).FirstOrDefault();
        if (searchEmail != null)
        {
            notificationService.AddNotification("Invalid Email",$"{request.localizer.GetKey("CONFIRM_USER_EMAIL_EXISTIS")}");
            Break();
            return;
        }
       userRepository.Add(user!);
       SetObjectsLog(user!);
    }
}

