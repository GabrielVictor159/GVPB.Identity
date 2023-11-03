using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Application.UseCases;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application
{
    public class VerifyDisponibilityTermsHandler : Handler<RequestUserRequest, RequestUserComunications>
    {
        private readonly INotificationService notificationService;
        private readonly IUserRepository userRepository;

        public VerifyDisponibilityTermsHandler(INotificationService notificationService, IUserRepository userRepository)
        {
            this.notificationService = notificationService;
            this.userRepository = userRepository;
        }

        public override void ProcessRequest(RequestUserRequest request, RequestUserComunications? comunications)
        {
            comunications!.AddLog(LogType.Process, "Executing VerifyDisponibilityTermsHandler");
            VerifyProperty(request.NewUser, u => u.UserName, "existing username", "user name");
            VerifyProperty(request.NewUser, u => u.Email, "existing email", "email");
            
            if(!notificationService.HasNotifications)
            sucessor?.ProcessRequest(request, comunications);
        }

        private void VerifyProperty(User user, Func<User, string> propertySelector, string notificationKey, string propertyDescription)
        {
            var value = propertySelector(user);
            var existingUser = userRepository.GetByFilter(e => propertySelector(e) == value).FirstOrDefault();
            if (existingUser != null)
                notificationService.AddNotification(notificationKey, $"There is already a user with the {propertyDescription} {value} in the database");
        }
    }
}
