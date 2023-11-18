using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases;
using GVPB.Identity.Domain.Models;
using Newtonsoft.Json;

namespace GVPB.Identity.Application;

public class SaveRequestUserHandler : Handler<RequestUserRequest, RequestUserComunications>
{
    private readonly IRequestUserRepository requestUserRepository;

    public SaveRequestUserHandler(IRequestUserRepository requestUserRepository)
    {
        this.requestUserRepository = requestUserRepository;
    }

    protected override void ProcessRequest(RequestUserRequest request, RequestUserComunications? comunications)
    {
        var requestUser = new RequestUser(request.Localizer)
        {
            Id = Guid.NewGuid(),
            Body = JsonConvert.SerializeObject(request.NewUser),
            CreationDate = DateTime.Now
        };
        requestUserRepository.Add(requestUser);
        comunications!.requestUser = requestUser;

        SetObjectsLog(requestUser);

    }
}
