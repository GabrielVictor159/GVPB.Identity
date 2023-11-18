
using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.ConfirmUser.Handlers;
using GVPB.Identity.BuildersTests.Builders;
using GVPB.Identity.Domain;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.ConfirmUser.Handlers;
[UseAutofacTestFramework]
public class GetRequestUserHandlerTest
{
    private readonly GetRequestUserHandler getRequestUserHandler;
    private readonly LanguageManager<SharedResources> languageService;
    private readonly IRequestUserRepository requestUserRepository;
    private readonly ConfirmUserPresenter presenter;
    private readonly INotificationService notificationService;

    public GetRequestUserHandlerTest
        (GetRequestUserHandler getRequestUserHandler, 
        LanguageManager<SharedResources> languageService, 
        IRequestUserRepository requestUserRepository, 
        ConfirmUserPresenter presenter,
        INotificationService notificationService)
    {
        this.getRequestUserHandler = getRequestUserHandler;
        this.languageService = languageService;
        this.requestUserRepository = requestUserRepository;
        this.presenter = presenter;
        this.notificationService = notificationService;
    }

    [Fact]
    public void Should_Execute_Existing_Request()
    {
        var requestUser = RequestUserBuilder.New().Build();
        requestUserRepository.Add(requestUser);

        getRequestUserHandler.Execute
            (new() { Id = requestUser.Id, localizer = languageService },
            new() { outputPort = presenter });

        notificationService.HasNotifications.Should().BeFalse();
    }

    [Fact]
    public void Should_Execute_Not_Existing_Request()
    {
        var requestUser = RequestUserBuilder.New().Build();

        getRequestUserHandler.Execute
            (new() { Id = requestUser.Id, localizer = languageService },
            new() { outputPort = presenter });

        notificationService.HasNotifications.Should().BeTrue();
        presenter.NotFoundMessage.Should().NotBeNull();
    }
}

