using GVPB.Identity.Application.Interfaces.Database;

namespace GVPB.Identity.Application.UseCases.RemoveUser.Handlers;

public class RemoveUserHandler : Handler<RemoveUserRequest, RemoveUserComunications>
{
    private readonly IUserRepository userRepository;

    public RemoveUserHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    protected override void ProcessRequest(RemoveUserRequest request, RemoveUserComunications? comunications = null)
    {
        userRepository.Delete(comunications!.User!);
    }
}
