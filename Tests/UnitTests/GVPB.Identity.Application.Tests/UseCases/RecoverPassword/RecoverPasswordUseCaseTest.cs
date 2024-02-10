
using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.RecoverPassword;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.RecoverPassword;
[UseAutofacTestFramework]
public class RecoverPasswordUseCaseTest
{
    private readonly RecoverPasswordPresenter recoverPasswordPresenter;
    private readonly LanguageManager<SharedResources> languageService;
    private readonly IUserRepository userRepository;
    private readonly IRecoverPasswordUseCase recoverPasswordUseCase;

    public RecoverPasswordUseCaseTest
        (RecoverPasswordPresenter recoverPasswordPresenter, 
        LanguageManager<SharedResources> languageService, 
        IUserRepository userRepository, 
        IRecoverPasswordUseCase recoverPasswordUseCase)
    {
        this.recoverPasswordPresenter = recoverPasswordPresenter;
        this.languageService = languageService;
        this.userRepository = userRepository;
        this.recoverPasswordUseCase = recoverPasswordUseCase;
    }

    [Fact]
    public void Should_Execute_Sucess()
    {
        var user = UserBuilder.New().Build();
        userRepository.Add(user);
        var request = new RecoverPasswordRequest() { Email = user.Email, localizer = languageService };
        recoverPasswordUseCase.Execute(request);
        recoverPasswordPresenter.ErrorMessage.Should().BeNull();
        recoverPasswordPresenter.StandardOutput.Should().NotBeNull();
    }
}

