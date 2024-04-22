
using FluentAssertions;
using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.ListRequest;
using GVPB.Identity.Application.UseCases.ListRequestUser.Handlers;
using GVPB.Identity.BuildersTests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.ListRequestUser.Handlers;
[UseAutofacTestFramework]
public class ListRequestUserHandlerTest
{
    private readonly IRequestUserRepository requestUserRepository;
    private readonly ListRequestUserHandler listRequestUserHandler;

    public ListRequestUserHandlerTest
        (IRequestUserRepository requestUserRepository, 
        ListRequestUserHandler listRequestUserHandler)
    {
        this.requestUserRepository = requestUserRepository;
        this.listRequestUserHandler = listRequestUserHandler;
    }

    [Fact]
    public void Should_Sucess_Search()
    {
        var requestUser = RequestUserBuilder.New().Build();
        requestUserRepository.Add(requestUser);
        var request = new ListRequestUserRequest() { Expression = e=>e.Id==requestUser.Id};
        var comunications = new ListRequestUserComunications();
        listRequestUserHandler.Execute(request, comunications);
        comunications.RequestUsers.Should().NotBeEmpty();
    }
}

