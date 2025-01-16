using System.Security;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace IdentityUtility.JWT;

public static class AuthExtension
{
    public static IServiceCollection AddAuth(this IServiceCollection serviceCollection, IConfiguration Configuration)
    {   
        var authSettings = Configuration.GetSection(nameof(JWTSettings)).Get<JWTSettings>();   
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            { 
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings!.SecretKey))

                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context => {
                        context.Token = context.Request.Cookies["MyCookie"];

                        return Task.CompletedTask;
                    }    
                };
            });
        
        serviceCollection.AddAuthorization();
           

        return serviceCollection;    
    }
}