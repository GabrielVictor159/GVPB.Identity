using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Domain.Models;
using Newtonsoft.Json;

namespace GVPB.Identity.Application.UseCases.RecoverPassword.Handlers;

public class CreateRequestUserHandler : Handler<RecoverPasswordRequest, RecoverPasswordComunications>
{
    private readonly IRequestUserRepository requestUserRepository;

    public CreateRequestUserHandler(IRequestUserRepository requestUserRepository)
    {
        this.requestUserRepository = requestUserRepository;
    }

    protected override void ProcessRequest(RecoverPasswordRequest request, RecoverPasswordComunications? comunications = null)
    {
        var requestUser = new Domain.Models.RequestUser()
        {
            Id = Guid.NewGuid(),
            Body = JsonConvert.SerializeObject(comunications!.user),
            CreationDate = DateTime.Now,
            RequestUserType = Domain.Enum.RequestUserType.RecoverPassword
        };
        requestUserRepository.Add(requestUser);
        SetObjectsLog(requestUser);
    }
}
