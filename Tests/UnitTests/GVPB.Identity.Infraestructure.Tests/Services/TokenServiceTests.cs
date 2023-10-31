
using FluentAssertions;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;
using System.IdentityModel.Tokens.Jwt;

namespace GVPB.Identity.Infraestructure.Tests.Services;
[UseAutofacTestFramework]
public class TokenServiceTests
{
    private readonly ITokenService tokenService;

    public TokenServiceTests(ITokenService tokenService)
    {
        this.tokenService = tokenService;
    }

    [Fact]
    public void Should_Create_Token_Sucess()
    {
        var user = UserBuilder.New().Build();
        var token = tokenService.GenerateToken(user);
        token.Should().NotBeNull().And.NotBeEmpty();
    }

    [Fact]
    public void Should_Create_Token_Verify_Claim_User_Name()
    {
        var user = UserBuilder.New().Build();
        var token = tokenService.GenerateToken(user);
        var claims = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
        var claimName = claims.Where(e => e.Type == "User_Name").FirstOrDefault();
        claimName.Should().NotBeNull();
        claimName?.Value.Should().Be(user.UserName);
    }

    [Fact]
    public void Should_Create_Token_Verify_Claim_User_Rule()
    {
        var user = UserBuilder.New().Build();
        var token = tokenService.GenerateToken(user);
        var claims = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
        var claimName = claims.Where(e => e.Type == "User_Rule").FirstOrDefault();
        claimName.Should().NotBeNull();
        claimName?.Value.Should().Be(user.Rule.ToString());
    }

    [Fact]
    public void Should_Create_Token_Verify_Claim_User_Id()
    {
        var user = UserBuilder.New().Build();
        var token = tokenService.GenerateToken(user);
        var claims = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
        var claimName = claims.Where(e => e.Type == "User_Id").FirstOrDefault();
        claimName.Should().NotBeNull();
        claimName?.Value.Should().Be(user.Id.ToString());
    }

    [Fact]
    public void Should_Create_Token_Verify_Claim_User_Email()
    {
        var user = UserBuilder.New().Build();
        var token = tokenService.GenerateToken(user);
        var claims = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
        var claimName = claims.Where(e => e.Type == "User_Email").FirstOrDefault();
        claimName.Should().NotBeNull();
        claimName?.Value.Should().Be(user.Email.ToString());
    }
}

