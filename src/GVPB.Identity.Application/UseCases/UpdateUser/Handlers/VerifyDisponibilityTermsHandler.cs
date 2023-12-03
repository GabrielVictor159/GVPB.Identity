using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.UseCases.UpdateUser.Handlers;

public class VerifyDisponibilityTermsHandler : Handler<UpdateUserRequest, UpdateUserComunications>
{
    private readonly INotificationService notificationService;
    private readonly IUserRepository userRepository;

    public VerifyDisponibilityTermsHandler(INotificationService notificationService, IUserRepository userRepository)
    {
        this.notificationService = notificationService;
        this.userRepository = userRepository;
    }
    protected override void ProcessRequest(UpdateUserRequest request, UpdateUserComunications? comunications = null)
    {
        if(request.NewUserName is not null && request.NewUserName !="")
       VerifyProperty(comunications!.User!, u => u.UserName, "existing username", "user name", request);
       
       if(notificationService.HasNotifications)
        {
            Break();
            return;
        }
    }

    private void VerifyProperty(User user, Func<User, string> propertySelector, string notificationKey, string propertyDescription, UpdateUserRequest request)
    {
        var value = propertySelector(user);
        var existingUser = userRepository.GetByFilter(e => propertySelector(e) == value).FirstOrDefault();
        if (existingUser != null)
            notificationService.AddNotification(notificationKey, $"{request.localizer.GetKey("LOGINNOTFOUND").Value} {propertyDescription}={value}");
    }
}
