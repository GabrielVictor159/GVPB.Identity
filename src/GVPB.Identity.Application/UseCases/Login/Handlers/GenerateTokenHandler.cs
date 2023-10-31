
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Domain.Enum;

namespace GVPB.Identity.Application.UseCases.Login.Handlers;

public class GenerateTokenHandler : Handler<LoginRequest>
{
    private readonly ITokenService tokenService;

    public GenerateTokenHandler(ITokenService tokenService)
    {
        this.tokenService = tokenService;
    }

    public override void ProcessRequest(LoginRequest request)
    {
        request.AddLog(LogType.Process, "Executing GenerateTokenHandler");
        request.Token = tokenService.GenerateToken(request.User!);
        sucessor?.ProcessRequest(request);
    }
}

