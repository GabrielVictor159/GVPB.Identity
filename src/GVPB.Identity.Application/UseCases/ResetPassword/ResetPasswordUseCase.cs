
using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.ResetPassword.Handlers;
using GVPB.Identity.Domain.Enum;

namespace GVPB.Identity.Application.UseCases.ResetPassword;

public class ResetPasswordUseCase : IResetPasswordUseCase
{
    private readonly IOutputPort<ResetPasswordResponse> outputPort;
    private readonly ILogRepository logRepository;
    private readonly SearchRequestUser searchRequestUser;

    public ResetPasswordUseCase
        (IOutputPort<ResetPasswordResponse> outputPort, 
        ILogRepository logRepository, 
        SearchRequestUser searchRequestUser,
        ResetPasswordHandler resetPasswordHandler)
    {
        this.outputPort = outputPort;
        this.logRepository = logRepository;
        searchRequestUser.SetSucessor(resetPasswordHandler);
        this.searchRequestUser = searchRequestUser;
    }

    public void Execute(ResetPasswordRequest request)
    {
        var ResetPasswordComunications = new ResetPasswordComunications() { outputPort = outputPort };
        try
        {
            searchRequestUser.Execute(request, ResetPasswordComunications);
            outputPort.Standard(new());
        }
        catch (Exception e)
        {
            ResetPasswordComunications.AddLog(
            LogType.Error,
                    $"Occurred an error to Reset Password UseCase" +
                    $"Error: {e.Message ?? e.InnerException?.Message}, stacktrace: {e.StackTrace}");
            outputPort.Error(e.Message!);
        }
        finally
        {
            logRepository.AddRange(ResetPasswordComunications.Logs);
        }
    }
}

