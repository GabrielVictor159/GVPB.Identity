
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Domain.Enum;

namespace GVPB.Identity.Application.UseCases.Login.Handlers;

public class GenerateTokenHandler : Handler<LoginRequest, LoginComunications>
{
    private readonly ITokenService tokenService;

    public GenerateTokenHandler(ITokenService tokenService)
    {
        this.tokenService = tokenService;
    }

    public override void ProcessRequest(LoginRequest request, LoginComunications? loginComunications)
    {
        loginComunications!.AddLog(LogType.Process, "Executing GenerateTokenHandler");
        loginComunications.Token = tokenService.GenerateToken(loginComunications.User!);
        sucessor?.ProcessRequest(request, loginComunications);
    }
}

