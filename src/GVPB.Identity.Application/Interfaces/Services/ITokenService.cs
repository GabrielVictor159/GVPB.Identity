using GVPB.Identity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVPB.Identity.Application.Interfaces.Services;
public interface ITokenService
{
    string GenerateToken(User user);
}
