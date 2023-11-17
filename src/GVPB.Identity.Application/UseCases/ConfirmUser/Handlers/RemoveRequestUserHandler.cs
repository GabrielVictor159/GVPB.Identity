
using GVPB.Identity.Application.Interfaces.Database;

namespace GVPB.Identity.Application.UseCases.ConfirmUser.Handlers;

public class RemoveRequestUserHandler : Handler<ConfirmUserRequest, ConfirmUserComunications>
{
    private readonly IRequestUserRepository requestUserRepository;

    public RemoveRequestUserHandler(IRequestUserRepository requestUserRepository)
    {
        this.requestUserRepository = requestUserRepository;
    }

    protected override void ProcessRequest(ConfirmUserRequest request, ConfirmUserComunications? comunications)
    {
        requestUserRepository.Delete(comunications!.requestUser!);
    }
}

