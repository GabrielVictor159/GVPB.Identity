using GVPB.Identity.Application.Interfaces.Common;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.Interfaces
{
    public interface IUserRepository : ICRUDRepositoryPattern<User>
    {
    }
}
