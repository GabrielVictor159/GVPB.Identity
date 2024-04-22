
using FluentAssertions;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.ListRequest;
using GVPB.Identity.BuildersTests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.ListRequestUser;
[UseAutofacTestFramework]
public class ListRequestUserUseCaseTest
{
    private readonly ListRequestUserPresenter listRequestUserPresenter;
    private readonly IListRequestUserUseCase listRequestUserUseCase;
    private readonly IRequestUserRepository requestUserRepository;

    public ListRequestUserUseCaseTest
        (ListRequestUserPresenter listRequestUserPresenter, 
        IListRequestUserUseCase listRequestUserUseCase, 
        IRequestUserRepository requestUserRepository)
    {
        this.listRequestUserPresenter = listRequestUserPresenter;
        this.listRequestUserUseCase = listRequestUserUseCase;
        this.requestUserRepository = requestUserRepository;
    }

    [Fact]
    public void Should_Sucess()
    {
        var requestUser = RequestUserBuilder.New().Build();
        requestUserRepository.Add(requestUser);
        var request = new ListRequestUserRequest() { Expression = e => e.Id == requestUser.Id };
        listRequestUserUseCase.Execute(request);
        listRequestUserPresenter.ErrorMessage.Should().BeNull();
        listRequestUserPresenter.StandardOutput.Should().NotBeNull();
    }
}

