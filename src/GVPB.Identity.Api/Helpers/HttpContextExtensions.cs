using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace GVPB.Identity.Api.Helpers;

public static class HttpContextExtensions
{
    public static CultureInfo GetCulture(this HttpContext httpContext)
    {
        return httpContext.Request.HttpContext.Features.Get<IRequestCultureFeature>()!.RequestCulture.Culture;
    }
}
