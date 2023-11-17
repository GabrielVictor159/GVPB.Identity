
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

    protected override void ProcessRequest(LoginRequest request, LoginComunications? loginComunications)
    {
        loginComunications!.Token = tokenService.GenerateToken(loginComunications.User!);
    }
}

