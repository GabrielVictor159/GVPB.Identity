using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.UseCases.UpdateUser.Handlers;

public class ValidateDomainHandler : Handler<UpdateUserRequest, UpdateUserComunications>
{
    private readonly INotificationService notificationService;

    public ValidateDomainHandler(INotificationService notificationService)
    {
        this.notificationService = notificationService;
    }

    protected override void ProcessRequest(UpdateUserRequest request, UpdateUserComunications? comunications = null)
    {
        var newUser = new User()
        {
            Id=request.IdUser, 
            Email = comunications!.User!.Email,
            UserName = request.NewUserName is null or ""? comunications.User.UserName : request.NewUserName,
            Password = request.NewPassword is null or ""? comunications.User.Password : request.NewPassword,
            Rule = comunications.User.Rule
        };
        
        if(!newUser.IsValid)
        {
            notificationService.AddNotifications(newUser.ValidationResult);
            Break();
            return;
        }

        comunications.User = newUser;
    }
}
