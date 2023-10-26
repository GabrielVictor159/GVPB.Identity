
using GVPB.Identity.Domain.Validator;

namespace GVPB.Identity.Domain.Models;

public class RequestUser : Entity<RequestUser, RequestUserValidator>
{
    public required Guid Id { get; init; }
    public required string Body { get; init; }
    public required DateTime CreationDate { get; init; }

    public RequestUser(Guid id, string body, DateTime creationDate)
        : base(new())
    {
        Id = id;
        Body = body;
        CreationDate = creationDate;
    }
}

