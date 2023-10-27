
using GVPB.Identity.Domain.Models;
using GVPB.Identity.Infraestructure.Tests.Builders;

namespace GVPB.Identity.Infraestructure.Tests.Factory;

public class DomainFactory
{
    public static Object? CreateDomain(string domainType)
    {
        switch(domainType)
        {
            case nameof(User):
                return UserBuilder.New().Build();

            default:
                return null;
        }
    }
    public static List<Object> CreateDomainRange(string domainType, int quantity)
    {
        List<Object> list = new List<Object>();
        for(int i = 0; i < quantity; i++)
        {
            list.Add(CreateDomain(domainType)!);
        }
        return list;
    }

}

