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
            new Claim("userName", account.Name),
            new Claim("password", account.Password)
        };
        var JwtToken = new JwtSecurityToken(
            expires : DateTime.UtcNow.Add(options.Value.Expires),
            claims: claims,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(options.Value.SecretKey)),
                SecurityAlgorithms.HmacSha256
            )
        );        
        
        return new JwtSecurityTokenHandler().WriteToken(JwtToken);
    }
}
