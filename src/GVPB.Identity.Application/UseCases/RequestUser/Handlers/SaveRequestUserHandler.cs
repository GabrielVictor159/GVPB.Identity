using GVPB.Identity.Application.UseCases;

namespace GVPB.Identity.Application;

public class SaveRequestUserHandler : Handler<RequestUserRequest, RequestUserComunications>
{
    public override void ProcessRequest(RequestUserRequest request, RequestUserComunications? comunications)
    {
        throw new NotImplementedException();
    }
}
