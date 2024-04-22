
using FluentAssertions;
using GVPB.Identity.BuildersTests.Builders;
using GVPB.Identity.Domain.Models;
using Xunit;

namespace GVPB.Identity.Domain.Tests.Validators;

public class RequestUserValidatorTests
{
    [Fact]
    public void Should_Create_Sucess()
    {
        RequestUserBuilder.New().Build().IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Create_Id_Failure()
    {
        RequestUserBuilder.New().WithId(Guid.Empty).Build().IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Create_CreationDate_Failure()
    {
        RequestUserBuilder.New().WithCreationDate(DateTime.MinValue).Build().IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Create_Body_Failure()
    {
        RequestUserBuilder.New().WithBody("").Build().IsValid.Should().BeFalse();
    }
}

