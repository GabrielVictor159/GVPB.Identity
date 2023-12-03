

using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Application.UseCases.UpdateUser;
using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Helpers;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.UpdateUser;
[UseAutofacTestFramework]
public class UpdateUserUseCaseTests
{
    private readonly IUpdateUserUseCase updateUserUseCase;
    private readonly UpdateUserPresenter updateUserPresenter;
    private readonly IUserRepository userRepository;
    private readonly LanguageManager<SharedResources> languageService;
    private readonly INotificationService notificationService;

    public UpdateUserUseCaseTests
    (IUpdateUserUseCase updateUserUseCase, 
    UpdateUserPresenter updateUserPresenter, 
    IUserRepository userRepository, 
    LanguageManager<SharedResources> languageService, 
    INotificationService notificationService)
    {
        this.updateUserUseCase = updateUserUseCase;
        this.updateUserPresenter = updateUserPresenter;
        this.userRepository = userRepository;
        this.languageService = languageService;
        this.notificationService = notificationService;
    }

    [Fact]
    public void Should_Execute_Sucess()
    {
        var user = UserBuilder.New().Build();
        userRepository.Add(user);
        string NewPassword = "TestUpdateUser";
        var request = new UpdateUserRequest(){IdUser = user.Id, NewPassword = NewPassword, localizer = languageService};
        updateUserUseCase.Execute(request);
        notificationService.HasNotifications.Should().BeFalse();
        updateUserPresenter.StandardOutput.Should().NotBeNull();
        userRepository.GetOne(user.Id)!.Password.Should().Be(NewPassword.md5Hash());
    }
}

