
using Autofac;
using Autofac.Extensions.DependencyInjection;
using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GVPB.Identity.Application.Tests.Modules;

public class TestModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLocalization(options => options.ResourcesPath = "Resources");
        builder.Populate(serviceCollection);
        builder.RegisterType<LoggerFactory>().As<ILoggerFactory>().SingleInstance();
        builder.RegisterAssemblyTypes(typeof(ConfigureTestFramework).Assembly)
        .AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();
        base.Load(builder);
    }

}

