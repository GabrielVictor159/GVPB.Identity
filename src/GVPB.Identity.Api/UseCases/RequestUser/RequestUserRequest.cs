using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Helpers;

namespace GVPB.Identity.Api.UseCases.RequestUser;

public class RequestUserRequest
{
    public required string UserName { get; set; }
    public required string Password{ get; set; }
    public required string Email { get; set; }
}
