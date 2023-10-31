
using Autofac;
using GVPB.Identity.Infraestructure.Modules;
using Xunit;
using Xunit.Abstractions;
using Xunit.Frameworks.Autofac;

[assembly: TestFramework("GVPB.Identity.Application.Tests.ConfigureTestFramework", "GVPB.Identity.Application.Tests")]
namespace GVPB.Identity.Application.Tests;

public class ConfigureTestFramework : AutofacTestFramework
{
    public ConfigureTestFramework(IMessageSink diagnosticMessageSink)
       : base(diagnosticMessageSink)
    {
        Environment.SetEnvironmentVariable("SECRET", Guid.NewGuid().ToString());
        Environment.SetEnvironmentVariable("TOKEN_EXPIRES", "8");
    }
    protected override void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new InfrastructureModule());
        builder.RegisterModule(new ApplicationModule());
        builder.RegisterAssemblyTypes(typeof(ConfigureTestFramework).Assembly)
        .AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();

    }
}

