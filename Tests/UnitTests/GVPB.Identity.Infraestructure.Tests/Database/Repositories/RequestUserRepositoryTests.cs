
using AutoMapper;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Infraestructure.Database;
using GVPB.Identity.Infraestructure.Database.Repositories.Common;
using GVPB.Identity.Infraestructure.Tests.Database.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Infraestructure.Tests.Database.Repositories;
[UseAutofacTestFramework]
public class RequestUserRepositoryTests
    : CRUDRepositoryPatternTests<Domain.Models.RequestUser, Infraestructure.Database.Entities.RequestUser>
{
    public RequestUserRepositoryTests
        (IRequestUserRepository repository, 
        Context dbContext, IMapper mapper) 
        : base((CRUDRepositoryPattern<Domain.Models.RequestUser, Infraestructure.Database.Entities.RequestUser>)repository, dbContext, mapper)
    {
    }
}

