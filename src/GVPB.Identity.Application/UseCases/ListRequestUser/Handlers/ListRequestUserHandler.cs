
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.ListRequest;

namespace GVPB.Identity.Application.UseCases.ListRequestUser.Handlers;

public class ListRequestUserHandler : Handler<ListRequestUserRequest, ListRequestUserComunications>
{
    private readonly IRequestUserRepository RequestUserRepository;
    protected override void ProcessRequest(ListRequestUserRequest request, ListRequestUserComunications? comunications = null)
    {
        comunications!.RequestUsers = RequestUserRepository.GetByFilter(request.Expression);
    }
}

