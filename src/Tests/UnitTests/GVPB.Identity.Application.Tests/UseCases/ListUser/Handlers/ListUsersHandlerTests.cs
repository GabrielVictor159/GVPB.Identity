using FluentAssertions;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.ListUser;
using GVPB.Identity.Application.UseCases.ListUser.Handlers;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.ListUser.Handlers;

[UseAutofacTestFramework]
public class ListUsersHandlerTests
{
    private readonly ListUsersHandler listUsersHandler;
    private readonly IUserRepository userRepository;

    public ListUsersHandlerTests(ListUsersHandler listUsersHandler, IUserRepository userRepository)
    {
        this.listUsersHandler = listUsersHandler;
        this.userRepository = userRepository;
    }

    [Fact]
    public void Should_List_Users_Successfully()
    {
        var user = UserBuilder.New().Build();
        userRepository.Add(user);
        var request = new ListUserRequest { expression = e => e.Id == user.Id };
        var comunications = new ListUserComunications();

        listUsersHandler.Execute(request, comunications);

        comunications.Users.Should().NotBeNull();
    }
}
