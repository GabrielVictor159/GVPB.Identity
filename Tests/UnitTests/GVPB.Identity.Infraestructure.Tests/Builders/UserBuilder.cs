
using Bogus;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Infraestructure.Tests.Builders;

public class UserBuilder
{
    private Guid Id { get; set; }
    private string UserName { get; set; } = "";
    private string Password { get; set; } = "";
    private string Email { get; set; } = "";
    private Rules Rule { get; set; } = Rules.USER;

    public static UserBuilder New()
    {
        var faker = new Faker("pt_BR");
        return new UserBuilder()
        {
            Id = Guid.NewGuid(),
            UserName = faker.Random.String2(faker.Random.Int(5,8)),
            Password = faker.Random.String2(faker.Random.Int(5, 8)),
            Email = faker.Random.String2(faker.Random.Int(5, 8))

        };
    }
    public User Build()
    {
        return new User() {Id =  Id, UserName = UserName, Password = Password, Email = Email , Rule = Rule };
    }

    public UserBuilder WithId(Guid value)
    {
        this.Id = value;
        return this;
    }
    public UserBuilder WithUserName(string value)
    {
        this.UserName = value;
        return this;
    }
    public UserBuilder WithPassword(string value)
    {
        this.Password = value;
        return this;
    }
    public UserBuilder WithRule(Rules value)
    {
        this.Rule = value;
        return this;
    }
    public UserBuilder WithEmail(string value)
    {
        this.Email = value;
        return this;
    }

}

