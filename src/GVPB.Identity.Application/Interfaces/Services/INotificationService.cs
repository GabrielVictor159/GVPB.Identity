using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using FluentValidator;
namespace GVPB.Identity.Application.Interfaces.Services;
public interface INotificationService
{
    List<Notification> Notifications { get; set; }
    bool HasNotifications { get; }
    void AddNotification(string key, string message);
    void AddNotifications(ValidationResult? validationResult);
}
