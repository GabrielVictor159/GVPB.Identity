
using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.ListRequestUser.Handlers;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.UseCases.ListRequest;

public class ListRequestUserUseCase : IListRequestUserUseCase
{
    private readonly IOutputPort<ListRequestUserResponse> outputPort;
    private readonly ILogRepository logRepository;
    private readonly ListRequestUserHandler listRequestUserHandler;

    public ListRequestUserUseCase
        (IOutputPort<ListRequestUserResponse> outputPort, 
        ILogRepository logRepository,
        ListRequestUserHandler listRequestUserHandler)
    {
        this.outputPort = outputPort;
        this.logRepository = logRepository;
        this.listRequestUserHandler = listRequestUserHandler;
    }

    public void Execute(ListRequestUserRequest request)
    {
        var ListRequestUserComunications = new ListRequestUserComunications() { };
        try
        {
            listRequestUserHandler.Execute(request, ListRequestUserComunications);
            outputPort.Standard(new() { RequestUsers = ListRequestUserComunications.RequestUsers});
        }
        catch (Exception e)
        {
            ListRequestUserComunications.AddLog(
            LogType.Error,
                    $"Occurred an error to List Request User UseCase" +
                    $"Error: {e.Message ?? e.InnerException?.Message}, stacktrace: {e.StackTrace}");
            outputPort.Error(e.Message!);
        }
        finally
        {
            logRepository.AddRange(ListRequestUserComunications.Logs);
        }
    }
}

