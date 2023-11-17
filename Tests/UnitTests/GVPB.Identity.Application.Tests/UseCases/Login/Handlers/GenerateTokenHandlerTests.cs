
using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.Login;
using GVPB.Identity.Application.UseCases.Login.Handlers;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.Login.Handlers;
[UseAutofacTestFramework]
public class GenerateTokenHandlerTests
{
    private readonly IUserRepository userRepository;
    private readonly GenerateTokenHandler generateTokenHandler;
    private readonly LanguageManager<SharedResources> languageService;

    public GenerateTokenHandlerTests
        (IUserRepository userRepository, 
        GenerateTokenHandler generateTokenHandler,
        LanguageManager<SharedResources> languageService)
    {
        this.userRepository = userRepository;
        this.generateTokenHandler = generateTokenHandler;
        this.languageService = languageService;
    }

    [Fact]
    public void Should_Generate_Token_Sucess()
    {
        var request = new LoginRequest()
        {
            UserName = "",
            Password = "",
            Localizer = languageService
        };
        var loginComunications = new LoginComunications() { User = UserBuilder.New().Build() };
        generateTokenHandler.Execute(request, loginComunications);
        loginComunications.Token.Should().NotBeNullOrEmpty();
    }
}

