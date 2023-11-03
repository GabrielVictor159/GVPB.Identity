
using System.Reflection;
using System.Runtime.CompilerServices;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Helpers;
using GVPB.Identity.Domain.Validator;
using Microsoft.Extensions.Localization;
namespace GVPB.Identity.Domain.Models;

public class User : Entity<User, UserValidator>
{
    public required Guid Id { get; init; }
    public required string UserName { get; init; }
    private string _password { get; set; } = "";
    public int PasswordLength { get; private set; }
    public required string Password 
    { 
        get 
        { 
            return _password; 
        } 
        init 
        { 
            _password = value.md5Hash(); 
            PasswordLength = value.Length;
        } 
    }
    public required string Email { get; init; }
    public required Rules Rule { get; init; }

    public User
        (ILanguageManager? Localizer = null)
        : base(new UserValidator(Localizer))
    {
    }
}

