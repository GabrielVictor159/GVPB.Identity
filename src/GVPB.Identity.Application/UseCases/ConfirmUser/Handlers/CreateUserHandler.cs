
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Domain.Models;
using Newtonsoft.Json;

namespace GVPB.Identity.Application.UseCases.ConfirmUser.Handlers;

public class CreateUserHandler : Handler<ConfirmUserRequest, ConfirmUserComunications>
{
    private readonly IUserRepository userRepository;

    public CreateUserHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    protected override void ProcessRequest(ConfirmUserRequest request, ConfirmUserComunications? comunications)
    {
       var user = JsonConvert.DeserializeObject<User>(comunications!.requestUser!.Body);
       userRepository.Add(user!);
       comunications.user = user;
       SetObjectsLog(user!);
    }
}

