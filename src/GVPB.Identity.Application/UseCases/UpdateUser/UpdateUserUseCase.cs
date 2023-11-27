using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.UpdateUser.Handlers;
using GVPB.Identity.Domain.Enum;

namespace GVPB.Identity.Application.UseCases.UpdateUser;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly ILogRepository logRepository;
    private readonly IOutputPort<UpdateUserResponse> outputPort;
    private readonly SearchUserHandler searchUserHandler;

    public UpdateUserUseCase
    (ILogRepository logRepository, 
    IOutputPort<UpdateUserResponse> outputPort, 
    SearchUserHandler searchUserHandler,
    ValidateDomainHandler validateDomainHandler,
    VerifyDisponibilityTermsHandler verifyDisponibilityTermsHandler,
    UpdateUserHandler updateUserHandler)
    {
        this.logRepository = logRepository;
        this.outputPort = outputPort;
        searchUserHandler
        .SetSucessor(validateDomainHandler
        .SetSucessor(verifyDisponibilityTermsHandler
        .SetSucessor(updateUserHandler)));
        this.searchUserHandler = searchUserHandler;
    }

    public void Execute(UpdateUserRequest request)
    {
        var UpdateUserComunications = new UpdateUserComunications(){outputPort=outputPort};
        try
        {
            searchUserHandler.Execute(request, UpdateUserComunications);
            outputPort.Standard(new(){NewAttributes= UpdateUserComunications.User!});
        }
        catch (Exception e)
        {
            UpdateUserComunications.AddLog(
            LogType.Error,
                    $"Occurred an error to Login UseCase" +
                    $"Error: {e.Message ?? e.InnerException?.Message}, stacktrace: {e.StackTrace}");
            outputPort.Error(e.Message!);
        }
        finally
        {
            logRepository.AddRange(UpdateUserComunications.Logs);
        }
    }
}
