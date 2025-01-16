using IdentityPersistance.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityUtility.JWT;

public class JWTGenerator(IOptions<JWTSettings> options)
{
    public string GenerateToken(Account account)
    {   
        var claims = new List<Claim>{
            new Claim("Id", account.Id.ToString()),
            new Claim(ClaimsIdentity.DefaultNameClaimType, account.Name),
        };
        foreach (var role in account.Roles)
        {
            claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name));
        }

        var signingCred = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)),SecurityAlgorithms.HmacSha256);
        var expireTime = DateTime.UtcNow.Add(options.Value.Expires);

        var JwtToken = new JwtSecurityToken(
            expires : expireTime,
            claims: claims,
            signingCredentials: signingCred
        );        
        
        return new JwtSecurityTokenHandler().WriteToken(JwtToken);
    }
}
