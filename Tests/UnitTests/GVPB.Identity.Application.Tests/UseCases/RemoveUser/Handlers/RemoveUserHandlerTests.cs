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
public class RemoveUserHandlerTests
{
    private readonly IUserRepository userRepository;
    private readonly RemoveUserPresenter removeUserPresenter;
    private readonly LanguageManager<SharedResources> languageManager;
    private readonly RemoveUserHandler removeUserHandler;

    public RemoveUserHandlerTests
    (IUserRepository userRepository, 
    RemoveUserPresenter removeUserPresenter, 
    LanguageManager<SharedResources> languageManager, 
    RemoveUserHandler removeUserHandler)
    {
        this.userRepository = userRepository;
        this.removeUserPresenter = removeUserPresenter;
        this.languageManager = languageManager;
        this.removeUserHandler = removeUserHandler;
    }

    [Fact]
    public void Should_Execute_Sucess()
    {
        var user = UserBuilder.New().Build();
        userRepository.Add(user);
        var request = new RemoveUserRequest(){localizer = languageManager, IdUser = Guid.NewGuid()};
        var comunications = new RemoveUserComunications(){OutputPort = removeUserPresenter, User= user};
        removeUserHandler.Execute(request,comunications);
        userRepository.GetOne(user.Id).Should().BeNull();

    }
}
