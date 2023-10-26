
namespace GVPB.Identity.Infraestructure.Database.Entities;

public class RequestUser
{
    public required Guid Id { get; init; }
    public required string Body { get; init; }
    public required DateTime CreationDate { get; init; }
}

