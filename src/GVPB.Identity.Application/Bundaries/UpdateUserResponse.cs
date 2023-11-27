using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.Bundaries;

public class UpdateUserResponse
{
    public required User NewAttributes {get; init;}
}
