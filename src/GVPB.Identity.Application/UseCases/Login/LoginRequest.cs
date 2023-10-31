
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.UseCases.Login;

public class LoginRequest
{
    public required string UserName { get; init;}
    public required string Password { get; init;}
    public string Token { get; set; } = "";
    public List<Log> Logs { get; private set; } = new();
    public User? User { get; set; }
    public void AddLog(LogType type, string message)
            => Logs.Add(Log.AddLog(message, type, DateTime.Now));

}

