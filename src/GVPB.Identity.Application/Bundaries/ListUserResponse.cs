using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.Bundaries;

public class ListUserResponse
{
    public required List<User> Users {get; init;}
}
