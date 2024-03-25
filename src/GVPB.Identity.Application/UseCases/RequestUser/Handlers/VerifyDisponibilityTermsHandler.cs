using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Application.UseCases;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Models;
using Newtonsoft.Json;

namespace GVPB.Identity.Application.UseCases.RequestUser.Handlers;

public class VerifyDisponibilityTermsHandler : Handler<RequestUserRequest, RequestUserComunications>
{
    private readonly INotificationService notificationService;
    private readonly IUserRepository userRepository;
    private readonly IRequestUserRepository requestUserRepository;

    public VerifyDisponibilityTermsHandler
        (INotificationService notificationService, 
        IUserRepository userRepository, 
        IRequestUserRepository requestUserRepository)
    {
        this.notificationService = notificationService;
        this.userRepository = userRepository;
        this.requestUserRepository = requestUserRepository;
    }

    protected override void ProcessRequest(RequestUserRequest request, RequestUserComunications? comunications)
    {
        VerifyProperty(request.NewUser, u => u.UserName, "existing username", "user name", request);
        VerifyProperty(request.NewUser, u => u.Email, "existing email", "email", request);
        VerifyRequestUserEmail(request.NewUser.Email, "existing email request", request);

        if (notificationService.HasNotifications)
        {
            Break();
            return;
        }
    }
    private void VerifyRequestUserEmail(string email, string notificationKey, RequestUserRequest request)
    {
        var existingUser = requestUserRepository.GetByFilter(e => JsonConvert.DeserializeObject<User>(e.Body)!.Email == email).ToList().Count;
        if (existingUser >= 3)
            notificationService.AddNotification(notificationKey, $"{request.Localizer.GetKey("LOGINNOTFOUND").Value} Email={email}");
    }
    private void VerifyProperty(User user, Func<User, string> propertySelector, string notificationKey, string propertyDescription, RequestUserRequest request)
    {
        var value = propertySelector(user);
        var existingUser = userRepository.GetByFilter(e => propertySelector(e) == value).FirstOrDefault();
        if (existingUser != null)
            notificationService.AddNotification(notificationKey, $"{request.Localizer.GetKey("LOGINNOTFOUND").Value} {propertyDescription}={value}");
    }
}
