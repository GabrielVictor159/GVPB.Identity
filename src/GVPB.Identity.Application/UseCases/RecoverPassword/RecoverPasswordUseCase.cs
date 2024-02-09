using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.UpdateUser;
using GVPB.Identity.Domain.Enum;

namespace GVPB.Identity.Application.UseCases.RecoverPassword;

public class RecoverPasswordUseCase : IRecoverPasswordUseCase
{
    private readonly ILogRepository logRepository;
    private readonly IOutputPort<RecoverPasswordResponse> outputPort;

    public RecoverPasswordUseCase(ILogRepository logRepository, 
        IOutputPort<RecoverPasswordResponse> outputPort)
    {
        this.logRepository = logRepository;
        this.outputPort = outputPort;
    }

    public void Execute(RecoverPasswordRequest request)
    {
        var RecoverPasswordComunications = new RecoverPasswordComunications() { outputPort = outputPort};
        try
        {

        }
        catch (Exception e)
        {
            RecoverPasswordComunications.AddLog(
            LogType.Error,
                    $"Occurred an error to Login UseCase" +
                    $"Error: {e.Message ?? e.InnerException?.Message}, stacktrace: {e.StackTrace}");
            outputPort.Error(e.Message!);
        }
        finally
        {
            logRepository.AddRange(RecoverPasswordComunications.Logs);
        }
    }
}
