using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GVPB.Identity.Application.Interfaces.Services;
using GVPB.Identity.Domain.Models;
using Microsoft.IdentityModel.Tokens;
namespace GVPB.Identity.Infraestructure.Services;

public class TokenService : ITokenService
{
    public string GenerateToken(User user)
    {
        var Secret = Environment.GetEnvironmentVariable("SECRET");
        if (Secret == null)
        {
            throw new InvalidOperationException("SECRET environment variable not deified");
        }
        var tokenExpire = Environment.GetEnvironmentVariable("TOKEN_EXPIRES");
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Rule.ToString()),
                        new Claim("User_Id", user.Id.ToString()),
                        new Claim("User_Email", user.Email.ToString()),
                }),
            Expires = DateTime.UtcNow.AddHours(tokenExpire != null ? int.Parse(tokenExpire) : 8),
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenFinal = tokenHandler.WriteToken(token);
        return tokenFinal;
    }

}

