
using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Helpers;

namespace GVPB.Identity.Application.UseCases.Login.Handlers;

public class UserLoginHandler : Handler<LoginRequest, LoginComunications>
{
    private readonly IUserRepository userRepository;
    private readonly INotificationService notificationService;

    public UserLoginHandler
        (IUserRepository userRepository, 
        INotificationService notificationService)
    {
        this.userRepository = userRepository;
        this.notificationService = notificationService;
    }

    public override void ProcessRequest(LoginRequest request, LoginComunications? loginComunications)
    {
        loginComunications!.AddLog(LogType.Process, "Executing UserLoginHandler");
        var entity = userRepository.GetByFilter(e=>
        e.UserName==request.UserName 
        && e.Password == request.Password.md5Hash()).FirstOrDefault();
        if(entity == null)
        {
            loginComunications.outputPort?.NotFound("Unable to find any user with the provided data.");
            notificationService.AddNotification("User Invalid", "Unable to find any user with the provided data.");
            return;
        }
        loginComunications.User = entity;
        sucessor?.ProcessRequest(request, loginComunications);
    }
}

