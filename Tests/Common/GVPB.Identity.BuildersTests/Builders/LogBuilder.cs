
using Bogus;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Models;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Newtonsoft.Json;

namespace GVPB.Identity.BuildersTests.Builders;

public class LogBuilder
{
    public Guid Id { get; private set; }
    public string Message { get; private set; } = "";
    public LogType Type { get; private set; }
    public DateTime LogDate { get; private set; }

    public static LogBuilder New()
    {
        var faker = new Faker("pt_BR");
        return new LogBuilder()
        {
            Id = Guid.NewGuid(),
            Message = "test",
            LogDate = DateTime.UtcNow,
            Type = LogType.Process
        };
    }

    public Log Build()
    {
        return new Log() { Id=Id, Message= Message, Type = Type, LogDate = LogDate };
    }

    public LogBuilder WithId(Guid value)
    {
        this.Id = value;
        return this;
    }

    public LogBuilder WithMessage(string value) 
    {
        this.Message = value;
        return this;
    }

    public LogBuilder WithLogDate(DateTime value)
    { 
        this.LogDate = value;
        return this;
    }

    public LogBuilder WithType(LogType value)
    {
        this.Type = value;
        return this;
    }
}

