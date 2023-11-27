using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases;

namespace GVPB.Identity.Application.UseCases.ListUser.Handlers;

public class ListUsersHandler : Handler<ListUserRequest, ListUserComunications>
{
    private readonly IUserRepository userRepository;

    public ListUsersHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    protected override void ProcessRequest(ListUserRequest request, ListUserComunications? comunications = null)
    {
        comunications!.Users = userRepository.GetByFilter(request.expression);
    }
}
