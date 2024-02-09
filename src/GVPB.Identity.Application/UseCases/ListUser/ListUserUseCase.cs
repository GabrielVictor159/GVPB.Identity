using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.ListUser.Handlers;
using GVPB.Identity.Domain.Enum;

namespace GVPB.Identity.Application.UseCases.ListUser;

public class ListUserUseCase : IListUserUseCase
{
    private readonly ILogRepository logRepository;
    private readonly IOutputPort<ListUserResponse> outputPort;
    private readonly ListUsersHandler listUsersHandler;

    public ListUserUseCase
    (ILogRepository logRepository, 
    IOutputPort<ListUserResponse> outputPort, 
    ListUsersHandler listUsersHandler)
    {
        this.logRepository = logRepository;
        this.outputPort = outputPort;
        this.listUsersHandler = listUsersHandler;
    }

    public void Execute(ListUserRequest request)
    {
         var ListUserComunications = new ListUserComunications();
        try
        {
            listUsersHandler.Execute(request,ListUserComunications);
            outputPort.Standard(new(){Users = ListUserComunications.Users});
        }
        catch (Exception e)
        {
            ListUserComunications.AddLog(
            LogType.Error,
                    $"Occurred an error to List User UseCase" +
                    $"Error: {e.Message ?? e.InnerException?.Message}, stacktrace: {e.StackTrace}");
            outputPort.Error(e.Message!);
        }
        finally
        {
            logRepository.AddRange(ListUserComunications.Logs);
        }
    }
}
