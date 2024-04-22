
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
public class UpdateUserHandlerTests
{
    private readonly UpdateUserHandler updateUserHandler;
    private readonly UpdateUserPresenter updateUserPresenter;
    private readonly IUserRepository userRepository;
    private readonly LanguageManager<SharedResources> languageService;

    public UpdateUserHandlerTests
    (UpdateUserHandler updateUserHandler, 
    UpdateUserPresenter updateUserPresenter, 
    IUserRepository userRepository, 
    LanguageManager<SharedResources> languageService)
    {
        this.updateUserHandler = updateUserHandler;
        this.updateUserPresenter = updateUserPresenter;
        this.userRepository = userRepository;
        this.languageService = languageService;
    }

    [Fact]
    public void Should_Execute_Sucess()
    {
        var user = UserBuilder.New().Build();
        userRepository.Add(user);
        var newUser = UserBuilder.New().WithId(user.Id).Build();
        var request = new UpdateUserRequest(){IdUser = user.Id, localizer = languageService };
        var comunications = new UpdateUserComunications(){outputPort=updateUserPresenter,User = newUser};
        updateUserHandler.Execute(request,comunications);
        userRepository.GetOne(user.Id)!.UserName.Should().Be(newUser.UserName);
    }
}

