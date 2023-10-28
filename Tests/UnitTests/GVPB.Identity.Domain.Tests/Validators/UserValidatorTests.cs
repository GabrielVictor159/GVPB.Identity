
using FluentAssertions;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;

namespace GVPB.Identity.Domain.Tests.Validators;

public class UserValidatorTests
{
    [Fact]
    public void Should_Create_Sucess()
    {
        UserBuilder.New().Build().IsValid.Should().BeTrue();
    }


    [Fact]
    public void Should_Create_Id_Failure()
    {
        UserBuilder.New().WithId(Guid.Empty).Build().IsValid.Should().BeFalse();
    }


    [Fact]
    public void Should_Create_UserName_Failure()
    {
        UserBuilder.New().WithUserName("").Build().IsValid.Should().BeFalse();
    }


    [Fact]
    public void Should_Create_Email_Failure()
    {
        UserBuilder.New().WithEmail("").Build().IsValid.Should().BeFalse();
    }


    [Fact]
    public void Should_Create_Password_Failure()
    {
        UserBuilder.New().WithPassword("te").Build().IsValid.Should().BeFalse();
    }
}

