
using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Application.UseCases.UpdateUser;
using GVPB.Identity.Application.UseCases.UpdateUser.Handlers;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.UpdateUser.Handlers;
[UseAutofacTestFramework]
public class VerifyDisponibilityTermsHandlerTests
{
    private readonly VerifyDisponibilityTermsHandler verifyDisponibilityTermsHandler;
    private readonly UpdateUserPresenter updateUserPresenter;
    private readonly IUserRepository userRepository;
    private readonly LanguageManager<SharedResources> languageService;
    private readonly INotificationService notificationService;

    public VerifyDisponibilityTermsHandlerTests
    (VerifyDisponibilityTermsHandler verifyDisponibilityTermsHandler, 
    UpdateUserPresenter updateUserPresenter, 
    IUserRepository userRepository, 
    LanguageManager<SharedResources> languageService, 
    INotificationService notificationService)
    {
        this.verifyDisponibilityTermsHandler = verifyDisponibilityTermsHandler;
        this.updateUserPresenter = updateUserPresenter;
        this.userRepository = userRepository;
        this.languageService = languageService;
        this.notificationService = notificationService;
    }

    [Fact]
    public void Should_Execute_Not_Disponibility_Term()
    {
        var user = UserBuilder.New().Build();
        userRepository.Add(user);
        var newUser = UserBuilder.New().WithUserName(user.UserName).Build();
        var request = new UpdateUserRequest(){IdUser = newUser.Id, NewUserName = newUser.UserName, localizer = languageService};
        var comunications = new UpdateUserComunications(){User= newUser, outputPort = updateUserPresenter};
        verifyDisponibilityTermsHandler.Execute(request,comunications);
        notificationService.HasNotifications.Should().BeTrue();
    }
}

