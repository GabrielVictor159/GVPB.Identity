
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.Bundaries;

public class ListRequestUserResponse
{
    public required List<RequestUser> RequestUsers { get; init; }
}

