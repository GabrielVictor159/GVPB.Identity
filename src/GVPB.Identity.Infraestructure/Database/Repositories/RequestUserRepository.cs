
using AutoMapper;
using GVPB.Identity.Application.Interfaces;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Infraestructure.Database.Repositories.Common;

namespace GVPB.Identity.Infraestructure.Database.Repositories;

public class RequestUserRepository 
    : CRUDRepositoryPattern<Domain.Models.RequestUser, Entities.RequestUser>,
    IRequestUserRepository
{
    public RequestUserRepository(Context context, IMapper mapper)
        : base(context, mapper)
    {
    }
}

