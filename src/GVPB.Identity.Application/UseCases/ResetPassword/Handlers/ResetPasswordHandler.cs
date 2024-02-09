
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Domain.Models;
using Newtonsoft.Json;

namespace GVPB.Identity.Application.UseCases.ResetPassword.Handlers;

public class ResetPasswordHandler : Handler<ResetPasswordRequest, ResetPasswordComunications>
{
    private readonly IUserRepository UserRepository;
    private readonly INotificationService notificationService;

    public ResetPasswordHandler
        (IUserRepository userRepository, 
        INotificationService notificationService)
    {
        UserRepository = userRepository;
        this.notificationService = notificationService;
    }

    protected override void ProcessRequest(ResetPasswordRequest request, ResetPasswordComunications? comunications = null)
    {
        var user = JsonConvert.DeserializeObject<User>(comunications!.requestUser!.Body);
        user!.Password = request.NewPassword;

        if(!user.IsValid)
        {
            notificationService.AddNotifications(user.ValidationResult);
            Break();
            return;
        }

        UserRepository.Update(user);
        this.SetObjectsLog(user);
    }
}

