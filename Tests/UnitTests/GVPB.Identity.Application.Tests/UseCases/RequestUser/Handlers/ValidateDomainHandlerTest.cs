using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests;

[UseAutofacTestFramework]
public class ValidateDomainHandlerTest
{
    private readonly ValidateDomainHandler validateDomainHandler;
    private readonly INotificationService notificationService;
    private readonly LanguageManager<SharedResources> languageService;

    public ValidateDomainHandlerTest
    (ValidateDomainHandler validateDomainHandler, 
    INotificationService notificationService,
    LanguageManager<SharedResources> languageService)
    {
        this.validateDomainHandler = validateDomainHandler;
        this.notificationService = notificationService;
        this.languageService = languageService;
    }

    [Fact]
    public void Should_Execute_Valid_Domain()
    {
        var user = UserBuilder.New().Build();
        validateDomainHandler.Execute(new(){NewUser=user,Localizer=languageService},new(){});
        notificationService.HasNotifications.Should().BeFalse();
    }

    [Fact]
    public void Should_Execute_InValid_Domain()
    {
        var user = UserBuilder.New().WithUserName("").Build();
        validateDomainHandler.Execute(new(){NewUser=user,Localizer=languageService},new(){});
        notificationService.HasNotifications.Should().BeTrue();
    }
}
