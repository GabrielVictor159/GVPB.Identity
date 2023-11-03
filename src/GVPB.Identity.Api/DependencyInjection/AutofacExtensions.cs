
using Autofac;
using GVPB.Identity.Infraestructure.Modules;
namespace GVPB.Identity.Api;
    public static class AutofacExtensions
    {
        public static ContainerBuilder AddAutofacRegistration(this ContainerBuilder builder)
        {
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<ApiModule>();
            return builder;
        }
    }

