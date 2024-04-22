
using GVPB.Identity.Api;
using GVPB.Identity.Application.Tests.Mocks.Presenters;
using GVPB.Identity.Application.UseCases.RecoverPassword;
using GVPB.Identity.Application.UseCases.RecoverPassword.Handlers;
using GVPB.Identity.BuildersTests.Builders;
using GVPB.Identity.Domain;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.RecoverPassword.Handlers;
[UseAutofacTestFramework]
public class SendEmailRecoverPasswordHandlerTest
{
    private readonly SendEmailRecoverPasswordHandler sendEmailRecoverPasswordHandler;
    private readonly LanguageManager<SharedResources> languageService;
    private readonly RecoverPasswordPresenter recoverPasswordPresenter;

    public SendEmailRecoverPasswordHandlerTest
        (SendEmailRecoverPasswordHandler sendEmailRecoverPasswordHandler, 
        LanguageManager<SharedResources> languageService,
        RecoverPasswordPresenter recoverPasswordPresenter)
    {
        this.sendEmailRecoverPasswordHandler = sendEmailRecoverPasswordHandler;
        this.languageService = languageService;
        this.recoverPasswordPresenter = recoverPasswordPresenter;
    }

    [Fact]
    public void Should_Send_Email_Recover_Password_Sucess()
    {
        var request = new RecoverPasswordRequest() { Email = "SendEmailRecoverPasswordHandlerTest@gmail.com", localizer = languageService};
        var comunications = new RecoverPasswordComunications() {outputPort = recoverPasswordPresenter, requestUser = RequestUserBuilder.New().Build() };
        sendEmailRecoverPasswordHandler.Execute(request, comunications);
    }
}

