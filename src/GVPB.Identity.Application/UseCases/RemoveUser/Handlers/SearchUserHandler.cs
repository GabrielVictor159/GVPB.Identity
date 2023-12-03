using GVPB.Identity.Application.Interfaces.Database;

namespace GVPB.Identity.Application.UseCases.RemoveUser.Handlers;

public class SearchUserHandler : Handler<RemoveUserRequest, RemoveUserComunications>
{
    private readonly IUserRepository userRepository;

    public SearchUserHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    protected override void ProcessRequest(RemoveUserRequest request, RemoveUserComunications? comunications = null)
    {
        var user = userRepository.GetOne(request.IdUser);
        if(user==null)
        {
            comunications!.OutputPort.NotFound(request.localizer.GetKey("UPDATEUSER_NOTFOUND")+$" Id = {request.IdUser}");
            Break();
            return;
        }
        comunications!.User = user;
        SetObjectsLog(user);
    }

}
