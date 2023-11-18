
using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.Login;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Newtonsoft.Json;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.RequestUser;
[UseAutofacTestFramework]
public class RequestUserUseCaseTest
{
    private readonly IRequestUserRepository requestUserRepository;
    private readonly INotificationService notificationService;
    private readonly IRequestUserUseCase requestUserUseCase;
    private readonly RequestUserPresenter requestUserPresenter;
    private readonly LanguageManager<SharedResources> languageService;

    public RequestUserUseCaseTest
        (IRequestUserRepository requestUserRepository, 
        INotificationService notificationService, 
        IRequestUserUseCase requestUserUseCase, 
        RequestUserPresenter requestUserPresenter, 
        LanguageManager<SharedResources> languageService)
    {
        this.requestUserRepository = requestUserRepository;
        this.notificationService = notificationService;
        this.requestUserUseCase = requestUserUseCase;
        this.requestUserPresenter = requestUserPresenter;
        this.languageService = languageService;
    }

    [Fact]
    public void Should_Execute_Sucess()
    {
        var user = UserBuilder.New().Build();
        requestUserUseCase.Execute(new() {NewUser= user, Localizer = languageService });
        requestUserRepository
            .GetByFilter(e=>e.Body.Equals(JsonConvert.SerializeObject(user))).Should().NotBeNullOrEmpty();
        notificationService.HasNotifications.Should().BeFalse();
        requestUserPresenter.ErrorMessage.Should().BeNull();
        requestUserPresenter.StandardOutput.Should().NotBeNull();
    }
}

