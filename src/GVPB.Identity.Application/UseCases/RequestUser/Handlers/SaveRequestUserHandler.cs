using GVPB.Identity.Application.UseCases;
using GVPB.Identity.Domain.Models;
using Newtonsoft.Json;

namespace GVPB.Identity.Application;

public class SaveRequestUserHandler : Handler<RequestUserRequest, RequestUserComunications>
{
    protected override void ProcessRequest(RequestUserRequest request, RequestUserComunications? comunications)
    {
        var requestUser = new RequestUser(request.Localizer)
        {
            Id = Guid.NewGuid(),
            Body = JsonConvert.SerializeObject(request.NewUser),
            CreationDate = DateTime.Now
        };
        comunications!.requestUser = requestUser;

        SetObjectsLog(requestUser);

    }
}
