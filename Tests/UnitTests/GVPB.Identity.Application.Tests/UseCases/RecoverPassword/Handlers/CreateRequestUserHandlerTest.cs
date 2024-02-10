
using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.RecoverPassword;
using GVPB.Identity.Application.UseCases.RecoverPassword.Handlers;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.RecoverPassword.Handlers;
[UseAutofacTestFramework]
public class CreateRequestUserHandlerTest
{
    private readonly IRequestUserRepository requestUserRepository;
    private readonly CreateRequestUserHandler createRequestUserHandler;
    private readonly RecoverPasswordPresenter recoverPasswordPresenter;
    private readonly LanguageManager<SharedResources> languageService;
    private readonly IUserRepository userRepository;

    public CreateRequestUserHandlerTest
        (IRequestUserRepository requestUserRepository, 
        CreateRequestUserHandler createRequestUserHandler,
        RecoverPasswordPresenter recoverPasswordPresenter,
        IUserRepository userRepository,
        LanguageManager<SharedResources> languageService)
    {
        this.requestUserRepository = requestUserRepository;
        this.createRequestUserHandler = createRequestUserHandler;
        this.recoverPasswordPresenter = recoverPasswordPresenter;
        this.userRepository = userRepository;
        this.languageService = languageService;
    }

    [Fact]
    public void Should_Execute_Sucess()
    {
        var user = UserBuilder.New().WithEmail("EmailTesteRecoverPassword@gmail.com").Build();
        userRepository.Add(user);
        var request = new RecoverPasswordRequest() {Email = user.Email, localizer = languageService };
        var comunications = new RecoverPasswordComunications() {outputPort = recoverPasswordPresenter, user = user };
        createRequestUserHandler.Execute(request, comunications);
        requestUserRepository.GetByFilter(e => true).Should().NotBeNullOrEmpty();
    }
}

