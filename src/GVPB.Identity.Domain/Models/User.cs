
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Helpers;
using GVPB.Identity.Domain.Validator;

namespace GVPB.Identity.Domain.Models;

public class User : Entity<User, UserValidator>
{
    public required Guid Id { get; init; }
    public required string UserName { get; init; }
    public required string Password { get; init; }
    public required string Email { get; init; }
    public required Rules Rule { get; init; }

    public User
        (Guid id, string userName, string password, string email, Rules rule)
        : base (new UserValidator())
    {
        Id = id;
        UserName = userName;
        Password = password.md5Hash();
        Email = email;
        Rule = rule;
    }
}

