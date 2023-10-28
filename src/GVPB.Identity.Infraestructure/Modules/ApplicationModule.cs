
using Autofac;

namespace GVPB.Identity.Infraestructure.Modules;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(Application.ApplicationException).Assembly)
        .AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();
        base.Load(builder);
    }
}

