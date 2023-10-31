﻿
using FluentAssertions;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.Login;
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

    public LoginUseCaseTests
        (IUserRepository userRepository, 
        INotificationService notificationService, 
        ILoginUseCase loginUseCase,
        LoginPresenter loginPresenter)
    {
        this.userRepository = userRepository;
        this.notificationService = notificationService;
        this.loginUseCase = loginUseCase;
        this.loginPresenter = loginPresenter;
    }

    [Fact]
    public void Should_Logs_And_Notifications_By_Sucess()
    {
        string password = "password";
        var user = UserBuilder.New().WithPassword(password).Build();
        userRepository.Add(user);
        var request = new LoginRequest() { UserName = user.UserName, Password = password };
        loginUseCase.Execute(request);
        request.Logs.Should().NotContain(e=>e.Type == Domain.Enum.LogType.Error);
        notificationService.HasNotifications.Should().BeFalse();
        loginPresenter.ErrorMessage.Should().BeNull();
        loginPresenter.StandardOutput.Should().NotBeNull();
    }
}
