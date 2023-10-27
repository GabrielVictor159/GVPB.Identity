
using AutoMapper;
using GVPB.Identity.Application.Interfaces;
using GVPB.Identity.Infraestructure.Database.Repositories.Common;

namespace GVPB.Identity.Infraestructure.Database.Repositories;

public class UserRepository
    : CRUDRepositoryPattern<Domain.Models.User, Entities.User>,
    IUserRepository
{
    public UserRepository(Context context, IMapper mapper) 
        : base(context, mapper)
    {
    }
}
