
using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.ConfirmUser.Handlers;
using GVPB.Identity.BuildersTests.Builders;
using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Models;
using Newtonsoft.Json;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.ConfirmUser.Handlers;
[UseAutofacTestFramework]
public class CreateUserHandlerTest
{
    private readonly CreateUserHandler createUserHandler;
    private readonly IUserRepository userRepository;
    private readonly LanguageManager<SharedResources> languageService;
    private readonly ConfirmUserPresenter confirmUserPresenter;

    public CreateUserHandlerTest
        (CreateUserHandler createUserHandler,
        IUserRepository userRepository,
        LanguageManager<SharedResources> languageService,
        ConfirmUserPresenter confirmUserPresenter)
    {
        this.createUserHandler = createUserHandler;
        this.userRepository = userRepository;
        this.languageService = languageService;
        this.confirmUserPresenter = confirmUserPresenter;
    }

    [Fact]
    public void Should_Execute_Sucess()
    {
        var RequestUser = RequestUserBuilder.New().Build();

        createUserHandler.Execute
            (new() { Id = Guid.NewGuid(), localizer= languageService }, 
            new() { requestUser = RequestUser, outputPort= confirmUserPresenter });

        var user = JsonConvert.DeserializeObject<User>(RequestUser.Body);
        userRepository.GetByFilter(e => e.Id == user!.Id).Should().NotBeNullOrEmpty();
    }
}

