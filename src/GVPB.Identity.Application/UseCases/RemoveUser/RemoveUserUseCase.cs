using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.RemoveUser.Handlers;
using GVPB.Identity.Domain.Enum;

namespace GVPB.Identity.Application.UseCases.RemoveUser;

public class RemoveUserUseCase : IRemoveUserUseCase
{
    private readonly ILogRepository logRepository;
    private readonly IOutputPort<RemoveUserResponse> outputPort;
    private readonly SearchUserHandler searchUserHandler;

    public RemoveUserUseCase
    (ILogRepository logRepository, 
    IOutputPort<RemoveUserResponse> outputPort, 
    SearchUserHandler searchUserHandler,
    RemoveUserHandler removeUserHandler)
    {
        this.logRepository = logRepository;
        this.outputPort = outputPort;
        searchUserHandler
        .SetSucessor(removeUserHandler);
        this.searchUserHandler = searchUserHandler;
    }

    public void Execute(RemoveUserRequest request)
    {
        var RemoveUserComunications = new RemoveUserComunications(){OutputPort=outputPort};
        try
        {
            searchUserHandler.Execute(request, RemoveUserComunications);
            outputPort.Standard(new());
        }
        catch (Exception e)
        {
            RemoveUserComunications.AddLog(
            LogType.Error,
                    $"Occurred an error to Remove User UseCase" +
                    $"Error: {e.Message ?? e.InnerException?.Message}, stacktrace: {e.StackTrace}");
            outputPort.Error(e.Message!);
        }
        finally
        {
            logRepository.AddRange(RemoveUserComunications.Logs);
        }
    }
}
