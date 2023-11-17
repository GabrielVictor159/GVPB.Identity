using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests;
[UseAutofacTestFramework]
public class VerifyDisponibilityTermsHandlerTest
{
    private readonly VerifyDisponibilityTermsHandler verifyDisponibilityTermsHandler;
    private readonly IUserRepository userRepository;
    private readonly INotificationService notificationService;
    private readonly LanguageManager<SharedResources> languageService;

    public VerifyDisponibilityTermsHandlerTest
    (VerifyDisponibilityTermsHandler verifyDisponibilityTermsHandler, 
    IUserRepository userRepository, 
    INotificationService notificationService, 
    LanguageManager<SharedResources> languageService)
    {
        this.verifyDisponibilityTermsHandler = verifyDisponibilityTermsHandler;
        this.userRepository = userRepository;
        this.notificationService = notificationService;
        this.languageService = languageService;
    }

    [Fact]
    public void Should_Execute_Existing_UserName_Term()
    {
        var userBefore = UserBuilder.New().Build();
        userRepository.Add(userBefore);
        var newUser = UserBuilder.New().WithUserName(userBefore.UserName).Build();
        verifyDisponibilityTermsHandler.Execute(new(){NewUser=newUser,Localizer=languageService},new(){});
        notificationService.HasNotifications.Should().BeTrue();
    }

    [Fact]
    public void Should_Execute_Existing_Email_Term()
    {
        var userBefore = UserBuilder.New().Build();
        userRepository.Add(userBefore);
        var newUser = UserBuilder.New().WithEmail(userBefore.Email).Build();
        verifyDisponibilityTermsHandler.Execute(new(){NewUser=newUser,Localizer=languageService},new(){});
        notificationService.HasNotifications.Should().BeTrue();
    }

    [Fact]
    public void Should_Execute_NotExisting_Terms()
    {
        var newUser = UserBuilder.New().Build();
        verifyDisponibilityTermsHandler.Execute(new(){NewUser=newUser,Localizer=languageService},new(){});
        notificationService.HasNotifications.Should().BeFalse();
    }
}
