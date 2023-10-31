
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Validator;

namespace GVPB.Identity.Domain.Models;

public class Log : Entity<Log, LogValidator>
{
    public required Guid Id { get; init; }
    public required string Message { get; init; }
    public required LogType Type { get; init; }
    public required DateTime LogDate { get; init; }

    public Log()
        : base(new LogValidator())
    {   
    }

    public static Log AddLog(string message, LogType type, DateTime logDate) => new Log() { Id = Guid.NewGuid(), Message = message, Type = type, LogDate = logDate };
}

