using GVPB.Identity.Api.Filters;

namespace GVPB.Identity.Api.DependencyInjection;

public static class FiltersExtensions
{
    public static IServiceCollection AddFilters(this IServiceCollection services)
    {
        services.AddMvc(options =>
        {
            options.Filters.Add(typeof(NotificationFilter));
        });
        return services;
    }
}
