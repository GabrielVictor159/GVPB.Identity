
using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.ConfirmUser.Handlers;
using GVPB.Identity.Domain.Enum;

namespace GVPB.Identity.Application.UseCases.ConfirmUser;

public class ConfirmUserUseCase : IConfirmUserUseCase
{
    private readonly ILogRepository logRepository;
    private readonly GetRequestUserHandler getRequestUserHandler;
    private readonly IOutputPort<ConfirmUserComunications> outputPort;

    public ConfirmUserUseCase
    (ILogRepository logRepository, 
    GetRequestUserHandler getRequestUserHandler,
    CreateUserHandler createUserHandler,
    RemoveRequestUserHandler removeRequestUserHandler,
    IOutputPort<ConfirmUserComunications> outputPort)
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
        var RequestUserComunications = new ConfirmUserComunications();
        try
        {
            getRequestUserHandler.Execute(request, RequestUserComunications);
        }
        catch (Exception e)
        {
            RequestUserComunications.AddLog(
            LogType.Error,
                    $"Occurred an error to Login UseCase" +
                    $"Error: {e.Message ?? e.InnerException?.Message}, stacktrace: {e.StackTrace}");
        }
        finally
        {
            logRepository.AddRange(RequestUserComunications.Logs);
        }
    }
}

