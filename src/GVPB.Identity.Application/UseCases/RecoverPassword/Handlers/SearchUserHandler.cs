using GVPB.Identity.Application.Interfaces.Database;

namespace GVPB.Identity.Application.UseCases.RecoverPassword.Handlers;

public class SearchUserHandler : Handler<RecoverPasswordRequest, RecoverPasswordComunications>
{
    private readonly IUserRepository userRepository;

    public SearchUserHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    protected override void ProcessRequest
        (RecoverPasswordRequest request, 
        RecoverPasswordComunications? comunications = null)
    {
        var user = userRepository.GetByFilter(e => e.Email == request.Email).FirstOrDefault();
        if (user == null)
        {
            comunications!.outputPort.NotFound(request.localizer.GetKey("RecoverPasswordUserNotFound") 
                + $"{request.Email}");
            Break();
            return;
        }
        comunications!.user = user;
        SetObjectsLog(user);
    }
}
