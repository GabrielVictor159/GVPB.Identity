
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.RecoverPassword.Handlers;
using GVPB.Identity.Application.UseCases.RecoverPassword;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;
using FluentAssertions;

namespace GVPB.Identity.Application.Tests.UseCases.RecoverPassword.Handlers;
[UseAutofacTestFramework]
public class SearchUserHandlerTest
{
    private readonly SearchUserHandler searchUserHandler;
    private readonly RecoverPasswordPresenter recoverPasswordPresenter;
    private readonly LanguageManager<SharedResources> languageService;
    private readonly IUserRepository userRepository;

    public SearchUserHandlerTest
        (SearchUserHandler searchUserHandler,
        RecoverPasswordPresenter recoverPasswordPresenter,
        IUserRepository userRepository,
        LanguageManager<SharedResources> languageService)
    {
        this.recoverPasswordPresenter = recoverPasswordPresenter;
        this.userRepository = userRepository;
        this.languageService = languageService;
        this.searchUserHandler = searchUserHandler;
    }

    [Fact]
    public void Should_Execute_Sucess()
    {
        var user = UserBuilder.New().WithEmail("EmailTesteRecoverPasswordSearchUserHandlerTest@gmail.com").Build();
        userRepository.Add(user);
        var request = new RecoverPasswordRequest() { Email = user.Email, localizer = languageService };
        var comunications = new RecoverPasswordComunications() { outputPort = recoverPasswordPresenter};
        searchUserHandler.Execute(request, comunications);
        recoverPasswordPresenter.NotFoundMessage.Should().BeNull();
        comunications.user.Should().NotBeNull();
    }


    [Fact]
    public void Should_Execute_NotFound()
    {
        var user = UserBuilder.New().WithEmail("EmailTesteRecoverPasswordSearchUserHandlerTestNotFound@gmail.com").Build();
        var request = new RecoverPasswordRequest() { Email = user.Email, localizer = languageService };
        var comunications = new RecoverPasswordComunications() { outputPort = recoverPasswordPresenter };
        searchUserHandler.Execute(request, comunications);
        recoverPasswordPresenter.NotFoundMessage.Should().NotBeNull();
        comunications.user.Should().BeNull();
    }
}

