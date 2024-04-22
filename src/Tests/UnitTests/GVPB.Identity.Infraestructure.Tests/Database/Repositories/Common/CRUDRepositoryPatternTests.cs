
using AutoMapper;
using FluentAssertions;
using GVPB.Identity.Infraestructure.Database.Repositories.Common;
using GVPB.Identity.Infraestructure.Tests.Factory;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Frameworks.Autofac;

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
        var entity = DomainFactory.CreateDomain(typeof(Domain).Name);
        if (entity is Domain domain)
        {
            cRUDRepositoryPattern.Add(domain);
            var result = dbContext.Set<Entity>().Find(entity.Id);
            Assert.NotEqual(null, result);
        }
        else
        {
            Assert.Fail($"Entity not {typeof(Domain).Name}");
        }
    }
    [Fact]
    public void Should_AddRange_Sucess()
    {
        var list = new List<Domain>();
        var listDynamic = new List<dynamic>();
        for (int i=0; i < 4; i++)
        {
            var entity = DomainFactory.CreateDomain(typeof(Domain).Name);
            if(entity is Domain d)
            {
                list.Add(d);
                listDynamic.Add(d);
            }
            else
            {
                Assert.Fail($"Entity not {typeof(Domain).Name}");
            }
        }
        cRUDRepositoryPattern.AddRange(list).Should().Be(list.Count);
        listDynamic.ForEach(e => 
        {
            var result = dbContext.Set<Entity>().Find(e.Id);
            Assert.NotNull(result);
        });
    }
    [Fact]
    public  void Should_GetOne_Sucess()
    {
        var entity = DomainFactory.CreateDomain(typeof(Domain).Name);
        if(entity is Domain domain)
        {
            dbContext.Add(mapper.Map<Entity>(domain));
            dbContext.SaveChanges();
            var result = cRUDRepositoryPattern.GetOne(entity.Id);
            Assert.NotNull(result);
        }
        else
        {
            Assert.Fail($"Entity not {typeof(Domain).Name}");
        }
    }
    [Fact]
    public void Should_GetByFilter_Sucess()
    {
        var entity = DomainFactory.CreateDomain(typeof(Domain).Name);
        if (entity is Domain domain)
        {
            dbContext.Add(mapper.Map<Entity>(domain));
            dbContext.SaveChanges();
            var result = cRUDRepositoryPattern.GetByFilter(e=>true);
            Assert.NotNull(result);
        }
        else
        {
            Assert.Fail($"Entity not {typeof(Domain).Name}");
        }
    }
    [Fact]
    public  void Should_Update_Sucess()
    {
        var entity = DomainFactory.CreateDomain(typeof(Domain).Name);
        var newAttributes = DomainFactory.CreateDomain(typeof(Domain).Name,entity!.Id);

        if (entity is Domain domain)
        {
            dbContext.Add(mapper.Map<Entity>(domain));
            dbContext.SaveChanges();
            var result = cRUDRepositoryPattern.Update(newAttributes);
            Assert.Equal(1, result);
        }
        else
        {
            Assert.Fail($"Entity not {typeof(Domain).Name}");
        }
    }
    [Fact]
    public  void Should_UpdateRange_Sucess()
    {
        var entities = new List<Domain>();
        var newAttributes = new List<Domain>();
        for(int i = 0; i < 4; i++)
        {
            var a = DomainFactory.CreateDomain(typeof(Domain).Name);
            entities.Add(a);
            newAttributes.Add(DomainFactory.CreateDomain(typeof(Domain).Name,a!.Id));
        }
        dbContext.AddRange(mapper.Map<List<Entity>>(entities));
        dbContext.SaveChanges();
        var result = cRUDRepositoryPattern.UpdateRange(newAttributes);
        Assert.Equal(entities.Count,result.numberLinesModify);
    }
    [Fact]
    public void Should_Delete_Sucess()
    {
        var entity = DomainFactory.CreateDomain(typeof(Domain).Name);
        dbContext.Add(mapper.Map<Entity>(entity));
        dbContext.SaveChanges();
        var result = cRUDRepositoryPattern.Delete(entity);
        Assert.Equal(1, result);
    }
    [Fact]
    public  void Should_DeleteRange_Sucess()
    {
        var entities = new List<Domain>();
        for (int i= 0; i < 4; i++)
        {
            entities.Add(DomainFactory.CreateDomain(typeof(Domain).Name));
        }
        dbContext.AddRange(mapper.Map<List<Entity>>(entities));
        dbContext.SaveChanges();
        var result = cRUDRepositoryPattern.DeleteRange(entities);
        Assert.Equal(entities.Count, result.numberLinesModify);
    }

}

