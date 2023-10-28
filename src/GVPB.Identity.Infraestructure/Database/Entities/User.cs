
using GVPB.Identity.Domain.Enum;

namespace GVPB.Identity.Infraestructure.Database.Entities;

public class User
{
    public required Guid Id { get; set; }
    public required string UserName { get; set; }
    public required int PasswordLength { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required Rules Rule { get; set; }
}

