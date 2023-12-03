
using Autofac;
using GVPB.Identity.Infraestructure.Modules;
using Xunit;
using Xunit.Abstractions;
using Xunit.Frameworks.Autofac;

[assembly: TestFramework("GVPB.Identity.Infraestructure.Tests.ConfigureTestFramework", "GVPB.Identity.Infraestructure.Tests")]
namespace GVPB.Identity.Infraestructure.Tests;

public class ConfigureTestFramework : AutofacTestFramework
{
    public ConfigureTestFramework(IMessageSink diagnosticMessageSink)
       : base(diagnosticMessageSink)
    {
        Environment.SetEnvironmentVariable("DBCONN", null);
        Environment.SetEnvironmentVariable("SECRET",Guid.NewGuid().ToString());
        Environment.SetEnvironmentVariable("TOKEN_EXPIRES", "8");
    }
    protected override void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new InfrastructureModule());

    }
}

