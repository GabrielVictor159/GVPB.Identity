
using System.Linq.Expressions;

namespace GVPB.Identity.Application.UseCases.ListRequest;

public class ListRequestUserRequest
{
    public required Func<Domain.Models.RequestUser, bool> Expression { get; init; }
}

public class ListRequestUserComunications : IComunications
{
    public List<Domain.Models.RequestUser> RequestUsers { get; set; } = new();
}

