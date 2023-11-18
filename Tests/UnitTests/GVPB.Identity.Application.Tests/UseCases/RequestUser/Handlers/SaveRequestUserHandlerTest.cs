
using FluentAssertions;
using GVPB.Identity.Api;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.RequestUser.Handlers;
[UseAutofacTestFramework]
public class SaveRequestUserHandlerTest
{
    private readonly SaveRequestUserHandler saveRequestUserHandler;
    private readonly LanguageManager<SharedResources> languageService;
    private readonly IRequestUserRepository requestUserRepository;

    public SaveRequestUserHandlerTest
        (SaveRequestUserHandler saveRequestUserHandler, 
        LanguageManager<SharedResources> languageService, 
        IRequestUserRepository requestUserRepository)
    {
        this.saveRequestUserHandler = saveRequestUserHandler;
        this.languageService = languageService;
        this.requestUserRepository = requestUserRepository;
    }

    [Fact]
    public void Should_Execute_Sucess()
    {
        var user = UserBuilder.New().Build();
        var comunications = new RequestUserComunications();
        saveRequestUserHandler.Execute(new() { NewUser = user, Localizer = languageService }, comunications);
        requestUserRepository.GetOne(comunications.requestUser!.Id).Should().NotBeNull();
    }
}

