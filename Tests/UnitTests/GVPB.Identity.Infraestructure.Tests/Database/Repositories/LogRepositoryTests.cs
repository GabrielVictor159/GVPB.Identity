
using AutoMapper;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Infraestructure.Database;
using GVPB.Identity.Infraestructure.Database.Repositories.Common;
using GVPB.Identity.Infraestructure.Tests.Database.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Infraestructure.Tests.Database.Repositories;
[UseAutofacTestFramework]
public class LogRepositoryTests
    : CRUDRepositoryPatternTests<Domain.Models.Log, Infraestructure.Database.Entities.Log>
{
    public LogRepositoryTests
        (ILogRepository logRepository, 
        Context dbContext, IMapper mapper) 
        : base((CRUDRepositoryPattern<Domain.Models.Log, Infraestructure.Database.Entities.Log>)logRepository, dbContext, mapper)
    {
    }
}

