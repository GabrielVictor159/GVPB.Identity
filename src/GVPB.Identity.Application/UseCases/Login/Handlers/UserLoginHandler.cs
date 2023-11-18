
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

    protected override void ProcessRequest(LoginRequest request, LoginComunications? loginComunications)
    {
        var password = request.Password.md5Hash();
        var entity = userRepository.GetByFilter(e=>
        e.UserName==request.UserName 
        && e.Password.Equals(password)).FirstOrDefault();
        if(entity == null)
        {
            loginComunications!.outputPort?.NotFound(request.Localizer.GetKey("LOGINNOTFOUND").Value);
            notificationService.AddNotification("User Invalid", request.Localizer.GetKey("LOGINNOTFOUND").Value);
            return;
        }
        loginComunications!.User = entity;
        SetObjectsLog(entity);
    }
}

