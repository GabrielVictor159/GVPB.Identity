using GVPB.Identity.Application.Interfaces.Database;

namespace GVPB.Identity.Application.UseCases.UpdateUser.Handlers;

public class UpdateUserHandler : Handler<UpdateUserRequest, UpdateUserComunications>
{
    private readonly IUserRepository userRepository;

    public UpdateUserHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    protected override void ProcessRequest(UpdateUserRequest request, UpdateUserComunications? comunications = null)
    {
        userRepository.Update(comunications!.User!);
    }
}
