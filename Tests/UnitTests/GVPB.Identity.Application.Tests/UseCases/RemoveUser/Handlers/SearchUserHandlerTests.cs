using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.RemoveUser;
using GVPB.Identity.Application.UseCases.RemoveUser.Handlers;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.RemoveUser.Handlers;
[UseAutofacTestFramework]
public class SearchUserHandlerTests
{
    private readonly IUserRepository userRepository;
    private readonly RemoveUserPresenter removeUserPresenter;
    private readonly LanguageManager<SharedResources> languageManager;
    private readonly SearchUserHandler searchUserHandler;

    public SearchUserHandlerTests
    (IUserRepository userRepository, 
    RemoveUserPresenter removeUserPresenter, 
    LanguageManager<SharedResources> languageManager, 
    SearchUserHandler searchUserHandler)
    {
        this.userRepository = userRepository;
        this.removeUserPresenter = removeUserPresenter;
        this.languageManager = languageManager;
        this.searchUserHandler = searchUserHandler;
    }

    [Fact]
    public void Should_Execute_Not_Found()
    {

        var request = new RemoveUserRequest(){localizer = languageManager, IdUser = Guid.NewGuid()};
        var comunications = new RemoveUserComunications(){OutputPort = removeUserPresenter};
        searchUserHandler.Execute(request,comunications);
        comunications.User.Should().BeNull();
        removeUserPresenter.NotFoundMessage.Should().NotBeNull();
    }

    [Fact]
    public void Should_Execute_Found()
    {
        var user = UserBuilder.New().Build();
        userRepository.Add(user);
        var request = new RemoveUserRequest(){localizer = languageManager, IdUser = user.Id};
        var comunications = new RemoveUserComunications(){OutputPort = removeUserPresenter};
        searchUserHandler.Execute(request,comunications);
        comunications.User.Should().NotBeNull();
    }
}
