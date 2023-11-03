using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application;

public class RequestUserRequest
{
    public required User NewUser {get; init;}

}

public class RequestUserComunications
{
    public RequestUser? requestUser {get; set;}
    public List<Log> Logs { get; private set; } = new();
    public void AddLog(LogType type, string message)
            => Logs.Add(Log.AddLog(message, type, DateTime.Now));
}
