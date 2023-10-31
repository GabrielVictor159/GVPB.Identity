
using FluentAssertions;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Application.UseCases.Login;
using GVPB.Identity.Application.UseCases.Login.Handlers;
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

    public UserLoginHandlerTests
        (UserLoginHandler userLoginHandler, 
        IUserRepository userRepository,
        INotificationService notificationService)
    {
        this.userLoginHandler = userLoginHandler;
        this.userRepository = userRepository;
        this.notificationService = notificationService;
    }

    [Fact]
    public void Should_Login_Sucess_User()
    {
        string password = "password";
        var user = UserBuilder.New().WithPassword(password).Build();
        userRepository.Add(user);
        var request = new LoginRequest() {UserName = user.UserName, Password = password};
        userLoginHandler.ProcessRequest(request);
        request.User.Should().NotBeNull();
    }

    [Fact]
    public void Should_Login_Failure_User()
    {
        var request = new LoginRequest() { UserName = "Teste", Password = "Teste" };
        userLoginHandler.ProcessRequest(request);
        request.User.Should().BeNull();
        notificationService.HasNotifications.Should().BeTrue();
    }
}

