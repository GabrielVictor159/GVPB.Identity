
using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.Login;
using GVPB.Identity.Application.UseCases.Login.Handlers;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.Login.Handlers;
[UseAutofacTestFramework]
public class UserLoginHandlerTests
{
    private readonly UserLoginHandler userLoginHandler;
    private readonly IUserRepository userRepository;
    private readonly INotificationService notificationService;
    private readonly LoginPresenter loginPresenter;
    private readonly LanguageManager<SharedResources> languageService;

    public UserLoginHandlerTests
        (UserLoginHandler userLoginHandler,
        IUserRepository userRepository,
        INotificationService notificationService,
        LoginPresenter loginPresenter,
        LanguageManager<SharedResources> languageService)
    {
        this.userLoginHandler = userLoginHandler;
        this.userRepository = userRepository;
        this.notificationService = notificationService;
        this.loginPresenter = loginPresenter;
        this.languageService = languageService;
    }

    [Fact]
    public void Should_Login_Sucess_User()
    {
        string password = "password";
        var user = UserBuilder.New().WithPassword(password).Build();
        userRepository.Add(user);
        var loginComunications = new LoginComunications() { outputPort = loginPresenter };
        var request = new LoginRequest() { UserName = user.UserName, Password = password, Localizer= languageService };
        userLoginHandler.Execute(request, loginComunications);
        loginComunications.User.Should().NotBeNull();
    }

    [Fact]
    public void Should_Login_Failure_User()
    {
        var request = new LoginRequest() { UserName = "Teste", Password = "Teste", Localizer = languageService };
        var loginComunications = new LoginComunications() { outputPort = loginPresenter };
        userLoginHandler.Execute(request, loginComunications);
        loginComunications.User.Should().BeNull();
        notificationService.HasNotifications.Should().BeTrue();
        loginPresenter.NotFoundMessage.Should().NotBeNull();
    }
}

