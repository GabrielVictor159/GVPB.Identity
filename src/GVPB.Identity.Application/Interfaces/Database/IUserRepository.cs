using GVPB.Identity.Application.Interfaces.Database.Common;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.Interfaces.Database;

public interface IUserRepository : ICRUDRepositoryPattern<User>
{
}
