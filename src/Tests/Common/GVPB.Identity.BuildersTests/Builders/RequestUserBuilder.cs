
using Bogus;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Models;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Newtonsoft.Json;

namespace GVPB.Identity.BuildersTests.Builders;

public class RequestUserBuilder
{
    private Guid Id { get; set; }
    private string Body { get; set; } = "";
    private DateTime CreationDate { get; set; }
    private RequestUserType RequestUserType { get; set; } = RequestUserType.Register;

    public static RequestUserBuilder New()
    {
        var faker = new Faker("pt_BR");
        return new RequestUserBuilder()
        {
            Id = Guid.NewGuid(),
            Body = JsonConvert.SerializeObject(UserBuilder.New().Build()),
            CreationDate = DateTime.UtcNow
        };
    }
    public RequestUser Build()
    {
        return new RequestUser() { Id = Id, Body = Body, CreationDate = CreationDate, RequestUserType = RequestUserType };
    }

    public RequestUserBuilder WithId(Guid value)
    {
        this.Id = value;
        return this;
    }

    public RequestUserBuilder WithBody(string value)
    {
        this.Body = value;
        return this;
    }

    public RequestUserBuilder WithCreationDate(DateTime value)
    {
        this.CreationDate = value;
        return this;
    }

    public RequestUserBuilder WithRequestUserType(RequestUserType requestUserType)
    {
        this.RequestUserType = requestUserType;
        return this;
    }


}

