
using GVPB.Identity.Domain.Enum;

namespace GVPB.Identity.Infraestructure.Database.Entities;

public class Log
{
    public Guid Id { get; set; }
    public required string Message { get; set; }
    public LogType Type { get; set; }
    public DateTime LogDate { get; set; }
}

