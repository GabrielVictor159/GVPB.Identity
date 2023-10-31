
using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.Login.Handlers;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.UseCases.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly ILogRepository logRepository;
    private readonly UserLoginHandler userLoginHandler;
    private readonly IOutputPort<LoginResponse> outputPort;
    public LoginUseCase
        (ILogRepository logRepository, 
        UserLoginHandler userLoginHandler,
        GenerateTokenHandler generateTokenHandler,
        IOutputPort<LoginResponse> outputPort)
    {
        userLoginHandler.SetSucessor(generateTokenHandler);
        this.logRepository = logRepository;
        this.userLoginHandler = userLoginHandler;
        this.outputPort = outputPort;   
    }

    public void Execute(LoginRequest request)
    {
        var loginComunications = new LoginComunications() { outputPort = outputPort };
        try
        {
            userLoginHandler.ProcessRequest(request, loginComunications);
            outputPort.Standard(new() { Token = loginComunications.Token });
        }
        catch(Exception e)
        {
            loginComunications.AddLog(
                    LogType.Error,
                    $"Occurred an error to Login UseCase" +
                    $"Error: {e.Message ?? e.InnerException?.Message}, stacktrace: {e.StackTrace}");
        }
        finally
        {
            logRepository.AddRange(loginComunications.Logs);
        }
    }
}

