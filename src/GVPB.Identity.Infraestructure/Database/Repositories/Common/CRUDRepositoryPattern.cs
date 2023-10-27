using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GVPB.Identity.Infraestructure.Database.Repositories.Common;

public abstract class CRUDRepositoryPattern<Domain, Entity> where Entity : class
{
    private readonly DbContext context;
    private readonly IMapper mapper;

    protected CRUDRepositoryPattern(DbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public int Add(Domain domain)
    {
        var entity = mapper.Map<Entity>(domain);
        context.Set<Entity>().Add(entity);
        return context.SaveChanges();
    }

    public int AddRange(List<Domain> domains)
    {
        var entities = mapper.Map<List<Entity>>(domains);
        context.Set<Entity>().AddRange(entities);
        return context.SaveChanges();
    }

    public Domain? GetOne(Guid id)
    {
        return mapper.Map<Domain>(context.Set<Entity>().Find(id));
    }

    public List<Domain> GetByFilter(Expression<Func<Domain, bool>> expression)
    {
        var predicate = mapper.Map<Expression<Func<Entity, bool>>>(expression);
        return mapper.Map<List<Domain>>(context.Set<Entity>().Where(predicate).ToList());
    }

    public int Update(Domain domain)
    {
        var entity = mapper.Map<Entity>(domain);
        var existingEntity = context.Set<Entity>().Find(entity);
        if (existingEntity != null)
        {
            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            return context.SaveChanges();
        }
        return 0;
    }

    public (int, List<Domain> EntitiesNotFound) UpdateRange(List<Domain> domains)
    {
        var entities = mapper.Map<List<Entity>>(domains);
        int result = 0;
        List<Entity> entitiesNotFound = new List<Entity>();
        foreach (var entity in entities)
        {
            var existingEntity = context.Set<Entity>().Find(entity);
            if (existingEntity != null)
            {
                context.Entry(existingEntity).CurrentValues.SetValues(entity);
                result += context.SaveChanges();
            }
            else
            {
                entitiesNotFound.Add(entity);
            }
        }
        return (result, mapper.Map<List<Domain>>(entitiesNotFound));
    }

    public int Delete(Domain domain)
    {
        var entity = context.Set<Entity>().Find(mapper.Map<Entity>(domain));
        if (entity != null)
        {
            context.Set<Entity>().Remove(entity);
            return context.SaveChanges();
        }
        return 0;
    }

    public (int, List<Domain> EntitiesNotFound) DeleteRange(List<Domain> domains)
    {
        var entities = mapper.Map<List<Entity>>(domains);
        int result = 0;
        List<Entity> entitiesNotFound = new List<Entity>();
        foreach (var entity in entities)
        {
            var entitySearch = context.Set<Entity>().Find(entity);
            if (entitySearch != null)
            {
                context.Set<Entity>().Remove(entity);
                result += context.SaveChanges();
            }
            else
            {
                entitiesNotFound.Add(entity);
            }
        }
        return (result, mapper.Map<List<Domain>>(entitiesNotFound));
    }
}

