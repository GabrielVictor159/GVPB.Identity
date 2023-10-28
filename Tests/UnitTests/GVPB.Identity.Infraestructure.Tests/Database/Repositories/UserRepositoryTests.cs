
using AutoMapper;
using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Infraestructure.Database;
using GVPB.Identity.Infraestructure.Database.Repositories.Common;
using GVPB.Identity.Infraestructure.Tests.Database.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Infraestructure.Tests.Database.Repositories;
[UseAutofacTestFramework]
public class UserRepositoryTests
    : CRUDRepositoryPatternTests<Domain.Models.User, Infraestructure.Database.Entities.User>

{
    public UserRepositoryTests
        (IUserRepository userRepository, 
        Context dbContext, IMapper mapper) 
        : base((CRUDRepositoryPattern<Domain.Models.User, Infraestructure.Database.Entities.User>)userRepository, dbContext, mapper)
    {
    }
}

