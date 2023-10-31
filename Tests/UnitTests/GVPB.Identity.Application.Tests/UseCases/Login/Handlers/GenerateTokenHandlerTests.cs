
using FluentAssertions;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.Login;
using GVPB.Identity.Application.UseCases.Login.Handlers;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.Login.Handlers;
[UseAutofacTestFramework]
public class GenerateTokenHandlerTests
{
    private readonly IUserRepository userRepository;
    private readonly GenerateTokenHandler generateTokenHandler;

    public GenerateTokenHandlerTests
        (IUserRepository userRepository, 
        GenerateTokenHandler generateTokenHandler)
    {
        this.userRepository = userRepository;
        this.generateTokenHandler = generateTokenHandler;
    }

    [Fact]
    public void Should_Generate_Token_Sucess()
    {
        var request = new LoginRequest() 
        {   
            UserName = "", 
            Password = ""
        };
        var loginComunications = new LoginComunications() { User = UserBuilder.New().Build()};
        generateTokenHandler.ProcessRequest(request, loginComunications);
        loginComunications.Token.Should().NotBeNullOrEmpty();
    }
}

