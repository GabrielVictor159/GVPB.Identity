
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

    }
    protected override void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new InfrastructureModule());

    }
}

