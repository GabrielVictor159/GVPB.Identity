
using GVPB.Identity.BuildersTests.Builders;
using GVPB.Identity.Domain.Models;
using GVPB.Identity.Infraestructure.Tests.Builders;

namespace GVPB.Identity.Infraestructure.Tests.Factory;

public class DomainFactory
{
    public static dynamic? CreateDomain(string domainType, Guid? Id = null)
    {
        switch(domainType)
        {
            case nameof(User):
                return UserBuilder.New().WithId(Id ?? Guid.NewGuid()).Build();

            case nameof(RequestUser):
                return RequestUserBuilder.New().WithId(Id ?? Guid.NewGuid()).Build();

            default:
                return null;
        }
    }

}

