using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application;

public class ConfirmUserResponse
{
    public required User User {get; init;}
}
