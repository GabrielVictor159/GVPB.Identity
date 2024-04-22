
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
public class ValidateDomainHandlerTests
{
    private readonly ValidateDomainHandler validateDomainHandler;
    private readonly UpdateUserPresenter updateUserPresenter;
    private readonly IUserRepository userRepository;
    private readonly LanguageManager<SharedResources> languageService;
    private readonly INotificationService notificationService;

    public ValidateDomainHandlerTests
    (ValidateDomainHandler validateDomainHandler, 
    UpdateUserPresenter updateUserPresenter, 
    IUserRepository userRepository, 
    LanguageManager<SharedResources> languageService, 
    INotificationService notificationService)
    {
        this.validateDomainHandler = validateDomainHandler;
        this.updateUserPresenter = updateUserPresenter;
        this.userRepository = userRepository;
        this.languageService = languageService;
        this.notificationService = notificationService;
    }

    [Fact]
    public void Should_Execute_Not_Valid_Attributes()
    {
        var user = UserBuilder.New().WithUserName("a").Build();
        var request = new UpdateUserRequest(){IdUser= user.Id, NewUserName = user.UserName, localizer = languageService};
        var comunications = new UpdateUserComunications(){outputPort = updateUserPresenter, User=user};
        validateDomainHandler.Execute(request,comunications);
        notificationService.HasNotifications.Should().BeTrue();
    }

    [Fact]
    public void Should_Execute_Valid_Attributes()
    {
        var user = UserBuilder.New().Build();
        var request = new UpdateUserRequest(){IdUser= user.Id, NewUserName = user.UserName, localizer = languageService};
        var comunications = new UpdateUserComunications(){outputPort = updateUserPresenter, User = user};
        validateDomainHandler.Execute(request,comunications);
        comunications.User.Should().NotBeNull();
    }
}

