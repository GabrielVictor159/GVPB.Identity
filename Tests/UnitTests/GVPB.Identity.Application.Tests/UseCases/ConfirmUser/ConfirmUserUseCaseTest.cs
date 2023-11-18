
using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.ConfirmUser;
using GVPB.Identity.BuildersTests.Builders;
using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Models;
using GVPB.Identity.Infraestructure.Database.Repositories;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Newtonsoft.Json;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.ConfirmUser;
[UseAutofacTestFramework]
public class ConfirmUserUseCaseTest
{
    private readonly IRequestUserRepository requestUserRepository;
    private readonly IUserRepository userRepository;
    private readonly INotificationService notificationService;
    private readonly IConfirmUserUseCase confirmUserUseCase;
    private readonly ConfirmUserPresenter confirmUserPresenter;
    private readonly LanguageManager<SharedResources> languageService;

    public ConfirmUserUseCaseTest
        (IRequestUserRepository requestUserRepository,
        IUserRepository userRepository,
        INotificationService notificationService,
        IConfirmUserUseCase confirmUserUseCase,
        ConfirmUserPresenter confirmUserPresenter,
        LanguageManager<SharedResources> languageService)
    {
        this.requestUserRepository = requestUserRepository;
        this.notificationService = notificationService;
        this.confirmUserUseCase = confirmUserUseCase;
        this.confirmUserPresenter = confirmUserPresenter;
        this.languageService = languageService;
        this.userRepository = userRepository;
    }

    [Fact]
    public void Should_Execute_Sucess()
    {
        var requestUser = RequestUserBuilder.New().Build();
        requestUserRepository.Add(requestUser);

        confirmUserUseCase.Execute(new() { Id = requestUser.Id, localizer = languageService });

        userRepository.GetOne(JsonConvert.DeserializeObject<User>(requestUser.Body)!.Id).Should().NotBeNull();
        notificationService.HasNotifications.Should().BeFalse();
        confirmUserPresenter.ErrorMessage.Should().BeNull();
        confirmUserPresenter.StandardOutput.Should().NotBeNull();
    }
}

