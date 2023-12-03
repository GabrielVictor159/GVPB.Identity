
using GVPB.Identity.Application.Interfaces.Database;

namespace GVPB.Identity.Application.UseCases.UpdateUser.Handlers;

public class SearchUserHandler : Handler<UpdateUserRequest, UpdateUserComunications>
{
    private readonly IUserRepository userRepository;

    public SearchUserHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    protected override void ProcessRequest(UpdateUserRequest request, UpdateUserComunications? comunications = null)
    {
        var user = userRepository.GetOne(request.IdUser);
        if(user==null)
        {
            comunications!.outputPort.NotFound(request.localizer.GetKey("UPDATEUSER_NOTFOUND")+$" Id = {request.IdUser}");
            Break();
            return;
        }
        comunications!.User = user;
        SetObjectsLog(user);
    }
}