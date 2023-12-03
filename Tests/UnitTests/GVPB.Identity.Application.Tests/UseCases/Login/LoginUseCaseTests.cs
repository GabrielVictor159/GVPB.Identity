
using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.Login;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.Login;
[UseAutofacTestFramework]
public class LoginUseCaseTests
{
    private readonly IUserRepository userRepository;
    private readonly INotificationService notificationService;
    private readonly ILoginUseCase loginUseCase;
    private readonly LoginPresenter loginPresenter;
    private readonly LanguageManager<SharedResources> languageService;

    public LoginUseCaseTests
        (IUserRepository userRepository, 
        INotificationService notificationService, 
        ILoginUseCase loginUseCase,
        LoginPresenter loginPresenter,
        LanguageManager<SharedResources> languageService)
    {
        this.userRepository = userRepository;
        this.notificationService = notificationService;
        this.loginUseCase = loginUseCase;
        this.loginPresenter = loginPresenter;
        this.languageService = languageService;
    }

    [Fact]
    public void Should_Logs_And_Notifications_By_Sucess()
    {
        string password = "password";
        var user = UserBuilder.New().WithPassword(password).Build();
        userRepository.Add(user);
        var request = new LoginRequest() { UserNameOrUserEmail = user.UserName, Password = password, Localizer = languageService };
        loginUseCase.Execute(request);
        notificationService.HasNotifications.Should().BeFalse();
        loginPresenter.ErrorMessage.Should().BeNull();
        loginPresenter.StandardOutput.Should().NotBeNull();
    }
}

