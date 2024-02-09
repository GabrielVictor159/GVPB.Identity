using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.RecoverPassword.Handlers;
using GVPB.Identity.Application.UseCases.UpdateUser;
using GVPB.Identity.Domain.Enum;

namespace GVPB.Identity.Application.UseCases.RecoverPassword;

public class RecoverPasswordUseCase : IRecoverPasswordUseCase
{
    private readonly ILogRepository logRepository;
    private readonly IOutputPort<RecoverPasswordResponse> outputPort;
    private readonly SearchUserHandler searchUserHandler;

    public RecoverPasswordUseCase(ILogRepository logRepository, 
        IOutputPort<RecoverPasswordResponse> outputPort,
        SearchUserHandler searchUserHandler,
        CreateRequestUserHandler createRequestUserHandler,
        SendEmailRecoverPasswordHandler sendEmailRecoverPasswordHandler)
    {
        searchUserHandler
            .SetSucessor(createRequestUserHandler
            .SetSucessor(sendEmailRecoverPasswordHandler));

        this.logRepository = logRepository;
        this.outputPort = outputPort;
        this.searchUserHandler = searchUserHandler;
    }

    public void Execute(RecoverPasswordRequest request)
    {
        var RecoverPasswordComunications = new RecoverPasswordComunications() { outputPort = outputPort};
        try
        {
            searchUserHandler.Execute(request, RecoverPasswordComunications);
            outputPort.Standard(new() { Message = request.localizer.GetKey("RecoverPassword_Response").Value });
        }
        catch (Exception e)
        {
            RecoverPasswordComunications.AddLog(
            LogType.Error,
                    $"Occurred an error to Recover Password UseCase" +
                    $"Error: {e.Message ?? e.InnerException?.Message}, stacktrace: {e.StackTrace}");
            outputPort.Error(e.Message!);
        }
        finally
        {
            logRepository.AddRange(RecoverPasswordComunications.Logs);
        }
    }
}
