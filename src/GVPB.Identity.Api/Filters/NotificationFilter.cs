using GVPB.Identity.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace GVPB.Identity.Api.Filters;

public class NotificationFilter : IAsyncResultFilter
{
    private readonly INotificationService notifications;
    public NotificationFilter(INotificationService notifications)
    {
        this.notifications = notifications;
    }
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (notifications.HasNotifications)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/json";
            var obj = JsonConvert.SerializeObject(notifications.Notifications);
            await context.HttpContext.Response.WriteAsync(obj);
            return;
        }
        await next();
    }
}
