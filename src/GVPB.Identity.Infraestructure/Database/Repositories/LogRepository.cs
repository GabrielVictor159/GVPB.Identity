
using AutoMapper;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Infraestructure.Database.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace GVPB.Identity.Infraestructure.Database.Repositories;

public class LogRepository
    : CRUDRepositoryPattern<Domain.Models.Log, Entities.Log>,
    ILogRepository
{
    public LogRepository(Context context, IMapper mapper) 
        : base(context, mapper)
    {
    }
}

