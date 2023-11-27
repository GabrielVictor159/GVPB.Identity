using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.Login.Handlers;
using GVPB.Identity.Application.UseCases.Login;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Application.UseCases.RequestUser.Handlers;

namespace GVPB.Identity.Application.UseCases.RequestUser;

public class RequestUserUseCase : IRequestUserUseCase
{
    private readonly ILogRepository logRepository;
    private readonly ValidateDomainHandler validateDomainHandler;
    private readonly IOutputPort<RequestUserResponse> outputPort;

    public RequestUserUseCase
        (ILogRepository logRepository, 
        ValidateDomainHandler validateDomainHandler, 
        VerifyDisponibilityTermsHandler verifyDisponibilityTermsHandler,
        SaveRequestUserHandler saveRequestUserHandler,
        SendEmailHandler sendEmailHandler,
        IOutputPort<RequestUserResponse> outputPort)
    {
        this.logRepository = logRepository;
        validateDomainHandler
            .SetSucessor(verifyDisponibilityTermsHandler
            .SetSucessor(saveRequestUserHandler
            .SetSucessor(sendEmailHandler)));

        this.validateDomainHandler = validateDomainHandler;
        this.outputPort = outputPort;
    }

    public void Execute(RequestUserRequest request)
    {
        var RequestUserComunications = new RequestUserComunications();
        try
        {
            validateDomainHandler.Execute(request, RequestUserComunications);
            outputPort.Standard(new());
        }
        catch (Exception e)
        {
            RequestUserComunications.AddLog(
            LogType.Error,
                    $"Occurred an error to Login UseCase" +
                    $"Error: {e.Message ?? e.InnerException?.Message}, stacktrace: {e.StackTrace}");
            outputPort.Error(e.Message!);
        }
        finally
        {
            logRepository.AddRange(RequestUserComunications.Logs);
        }
    }
}
