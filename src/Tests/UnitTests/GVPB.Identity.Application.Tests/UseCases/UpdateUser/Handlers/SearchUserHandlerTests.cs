
using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.UpdateUser;
using GVPB.Identity.Application.UseCases.UpdateUser.Handlers;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.UpdateUser.Handlers;
[UseAutofacTestFramework]
public class SearchUserHandlerTests
{
    private readonly SearchUserHandler searchUserHandler;
    private readonly UpdateUserPresenter updateUserPresenter;
    private readonly IUserRepository userRepository;
    private readonly LanguageManager<SharedResources> languageService;

    public SearchUserHandlerTests
    (SearchUserHandler searchUserHandler, 
    UpdateUserPresenter updateUserPresenter, 
    IUserRepository userRepository, 
    LanguageManager<SharedResources> languageService)
    {
        this.searchUserHandler = searchUserHandler;
        this.updateUserPresenter = updateUserPresenter;
        this.userRepository = userRepository;
        this.languageService = languageService;
    }

    [Fact]
    public void Should_Search_Found_User()
    {
        var user = UserBuilder.New().Build();
        userRepository.Add(user);
        var comunications = new UpdateUserComunications(){outputPort=updateUserPresenter};
        var request = new UpdateUserRequest(){IdUser=user.Id, localizer = languageService};
        searchUserHandler.Execute(request,comunications);
        comunications.User.Should().NotBeNull();
    }

    [Fact]
    public void Should_Search_Not_Found_User()
    {
        var comunications = new UpdateUserComunications(){outputPort=updateUserPresenter};
        var request = new UpdateUserRequest(){IdUser=Guid.NewGuid(), localizer = languageService};
        searchUserHandler.Execute(request,comunications);
        comunications.User.Should().BeNull();
        updateUserPresenter.NotFoundMessage.Should().NotBeNull();
    }
}
