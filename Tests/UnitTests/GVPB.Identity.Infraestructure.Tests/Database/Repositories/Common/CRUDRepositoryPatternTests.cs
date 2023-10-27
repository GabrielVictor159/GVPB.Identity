
using AutoMapper;
using FluentAssertions;
using GVPB.Identity.Infraestructure.Database.Repositories.Common;
using GVPB.Identity.Infraestructure.Tests.Factory;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GVPB.Identity.Infraestructure.Tests.Database.Repositories.Common;

public abstract class CRUDRepositoryPatternTests<Domain,Entity> where Entity : class
{
    private readonly CRUDRepositoryPattern<Domain,Entity> cRUDRepositoryPattern;
    private readonly DbContext dbContext;
    private readonly IMapper mapper;

    protected CRUDRepositoryPatternTests
        (CRUDRepositoryPattern<Domain, Entity> cRUDRepositoryPattern,
        DbContext dbContext,
        IMapper mapper)
    {
        this.cRUDRepositoryPattern = cRUDRepositoryPattern;
        this.dbContext = dbContext; 
        this.mapper = mapper;
    }

    [Fact]
    public void Should_Add_Sucess()
    {
        var entity = DomainFactory.CreateDomain(nameof(Domain));
        entity.Should().BeOfType<Domain>();
        if (entity is Domain domain)
        {
            cRUDRepositoryPattern.Add(domain);
            var result = dbContext.Set<Entity>().Find(mapper.Map<Entity>(domain));
            result.Should().NotBeNull();
        }
    }

    [Fact]
    public void Should_AddRange_Sucess()
    {
        var entity = DomainFactory.CreateDomainRange(nameof(Domain),4);
        entity.Should().BeOfType<Domain>();
        if (entity is Domain domain)
        {
            cRUDRepositoryPattern.Add(domain);
            var result = dbContext.Set<Entity>().Find(mapper.Map<Entity>(domain));
            result.Should().NotBeNull();
        }
    }
}

