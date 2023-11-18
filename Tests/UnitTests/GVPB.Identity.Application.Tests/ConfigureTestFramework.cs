
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentAssertions.Common;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Tests.Modules;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Modules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
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
        Environment.SetEnvironmentVariable
            ("PATH_IMAGES_APLICATIONS",
            "../../../../../../src/GVPB.Identity.Application/Resources/Images/");
    }
    protected override void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new InfrastructureModule());
        builder.RegisterModule(new ApplicationModule());
        builder.RegisterModule(new TestModule());
        
    }
}

