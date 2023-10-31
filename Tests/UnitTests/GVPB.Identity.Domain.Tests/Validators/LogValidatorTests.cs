
using FluentAssertions;
using GVPB.Identity.BuildersTests.Builders;
using Xunit;

namespace GVPB.Identity.Domain.Tests.Validators;

public class LogValidatorTests
{
    [Fact]
    public void Should_Create_Sucess()
    {
        LogBuilder.New().Build().IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Create_Id_Failure()
    {
        LogBuilder.New().WithId(Guid.Empty).Build().IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Create_Message_Failure()
    {
        LogBuilder.New().WithMessage("").Build().IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Create_LogDate_Failure()
    {
        LogBuilder.New().WithLogDate(DateTime.MinValue).Build().IsValid.Should().BeFalse();
    }
}

