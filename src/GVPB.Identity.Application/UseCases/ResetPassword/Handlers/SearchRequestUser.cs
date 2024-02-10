
using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.Interfaces.Services;

namespace GVPB.Identity.Application.UseCases.ResetPassword.Handlers;

public class SearchRequestUser : Handler<ResetPasswordRequest, ResetPasswordComunications>
{
    private readonly IRequestUserRepository requestUserRepository;

    public SearchRequestUser(IRequestUserRepository requestUserRepository)
    {
        this.requestUserRepository = requestUserRepository;
    }

    protected override void ProcessRequest(ResetPasswordRequest request, ResetPasswordComunications? comunications = null)
    {
        var requestUser = requestUserRepository
            .GetByFilter(e => e.Id == request.IdRequest && e.RequestUserType == Domain.Enum.RequestUserType.RecoverPassword)
            .FirstOrDefault();

        if (requestUser == null)
        {
            comunications!.outputPort.NotFound($"{request.Localizer.GetKey("CONFIRMUSER_NOTFOUND")} Id = {request.IdRequest}");
            Break();
            return;
        }

        comunications!.requestUser = requestUser;
        this.SetObjectsLog(requestUser);
    }
}

