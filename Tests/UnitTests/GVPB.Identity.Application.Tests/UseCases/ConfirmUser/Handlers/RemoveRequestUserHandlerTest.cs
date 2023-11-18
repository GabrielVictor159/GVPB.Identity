
using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.ConfirmUser.Handlers;
using GVPB.Identity.BuildersTests.Builders;
using GVPB.Identity.Domain;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.ConfirmUser.Handlers;
[UseAutofacTestFramework]
public class RemoveRequestUserHandlerTest
{
    private readonly IRequestUserRepository requestUserRepository;
    private readonly RemoveRequestUserHandler removeRequestUserHandler;
    private readonly LanguageManager<SharedResources> languageService;
    private readonly ConfirmUserPresenter confirmUserPresenter;

    public RemoveRequestUserHandlerTest
        (IRequestUserRepository requestUserRepository, 
        RemoveRequestUserHandler removeRequestUserHandler, 
        LanguageManager<SharedResources> languageService, 
        ConfirmUserPresenter confirmUserPresenter)
    {
        this.requestUserRepository = requestUserRepository;
        this.removeRequestUserHandler = removeRequestUserHandler;
        this.languageService = languageService;
        this.confirmUserPresenter = confirmUserPresenter;
    }

    [Fact]
    public void Should_Execute_Sucess()
    {
        var requestUser = RequestUserBuilder.New().Build();
        requestUserRepository.Add(requestUser);

        removeRequestUserHandler.Execute
            (new() { Id = requestUser.Id, localizer = languageService },
            new() {outputPort = confirmUserPresenter, requestUser = requestUser });

        requestUserRepository.GetOne(requestUser.Id).Should().BeNull();
    }
}

