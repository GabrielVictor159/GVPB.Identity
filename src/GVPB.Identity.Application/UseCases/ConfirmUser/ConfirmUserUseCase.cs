
using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.ConfirmUser.Handlers;
using GVPB.Identity.Domain.Enum;

namespace GVPB.Identity.Application.UseCases.ConfirmUser;

public class ConfirmUserUseCase : IConfirmUserUseCase
{
    private readonly ILogRepository logRepository;
    private readonly GetRequestUserHandler getRequestUserHandler;
    private readonly IOutputPort<ConfirmUserResponse> outputPort;

    public ConfirmUserUseCase
    (ILogRepository logRepository, 
    GetRequestUserHandler getRequestUserHandler,
    CreateUserHandler createUserHandler,
    RemoveRequestUserHandler removeRequestUserHandler,
    IOutputPort<ConfirmUserResponse> outputPort)
    {
        this.logRepository = logRepository;
        getRequestUserHandler
        .SetSucessor(createUserHandler
        .SetSucessor(removeRequestUserHandler));
        this.getRequestUserHandler = getRequestUserHandler;
        this.outputPort = outputPort;
    }

    public void Execute(ConfirmUserRequest request)
    {
        var RequestUserComunications = new ConfirmUserComunications() { outputPort = outputPort };
        try
        {
            getRequestUserHandler.Execute(request, RequestUserComunications);
            outputPort.Standard(new ConfirmUserResponse(){User = RequestUserComunications.user!});
        }
        catch (Exception e)
        {
            RequestUserComunications.AddLog(
            LogType.Error,
                    $"Occurred an error to Confirm User UseCase" +
                    $"Error: {e.Message ?? e.InnerException?.Message}, stacktrace: {e.StackTrace}");
            outputPort.Error(e.Message!);
        }
        finally
        {
            logRepository.AddRange(RequestUserComunications.Logs);
        }
    }
}

