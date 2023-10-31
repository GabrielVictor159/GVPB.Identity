using GVPB.Identity.Application.Interfaces.Database.Common;
using GVPB.Identity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVPB.Identity.Application.Interfaces.Database;
public interface ILogRepository : ICRUDRepositoryPattern<Log>
{
}
