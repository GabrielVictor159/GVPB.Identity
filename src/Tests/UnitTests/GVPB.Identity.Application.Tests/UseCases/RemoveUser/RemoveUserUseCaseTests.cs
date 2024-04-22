using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.RemoveUser;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.RemoveUser;
[UseAutofacTestFramework]
public class RemoveUserUseCaseTests
{
    private readonly IUserRepository userRepository;
    private readonly RemoveUserPresenter removeUserPresenter;
    private readonly LanguageManager<SharedResources> languageManager;
    private readonly IRemoveUserUseCase removeUserUseCase;

    public RemoveUserUseCaseTests
    (IUserRepository userRepository, 
    RemoveUserPresenter removeUserPresenter, 
    LanguageManager<SharedResources> languageManager, 
    IRemoveUserUseCase removeUserUseCase)
    {
        this.userRepository = userRepository;
        this.removeUserPresenter = removeUserPresenter;
        this.languageManager = languageManager;
        this.removeUserUseCase = removeUserUseCase;
    }

    [Fact]
    public void Should_Execute_Sucess()
    {
        var user = UserBuilder.New().Build();
        userRepository.Add(user);
        var request = new RemoveUserRequest(){IdUser = user.Id, localizer = languageManager};
        removeUserUseCase.Execute(request);
        removeUserPresenter.StandardOutput.Should().NotBeNull();
        userRepository.GetOne(user.Id).Should().BeNull();
    }
}
