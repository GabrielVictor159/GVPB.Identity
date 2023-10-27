using System.Linq.Expressions;

namespace GVPB.Identity.Application.Interfaces.Common;
public interface ICRUDRepositoryPattern<Domain>
{
    int Add(Domain domain);
    int AddRange(List<Domain> domains);
    Domain? GetOne(Guid id);
    List<Domain> GetByFilter(Expression<Func<Domain, bool>> expression);
    int Update(Domain domain);
    (int, List<Domain> EntitiesNotFound) UpdateRange(List<Domain> domains);
    int Delete(Domain domain);
    (int, List<Domain> EntitiesNotFound) DeleteRange(List<Domain> domains);
}
