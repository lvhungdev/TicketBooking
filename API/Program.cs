using System.Text;
using API.Authorization;
using API.Configurations;
using Infrastructure.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API;

public static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder
            .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(m =>
            {
                JwtSettings jwtSettings = builder.Configuration.GetRequiredSection("JwtSettings").Get<JwtSettings>()!;

                m.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
            });

        builder.Services.AddSingleton(builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()!);

        builder.Services.AddDependencies();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        WebApplication app = builder.Build();

        app.UseHttpsRedirection();
        app.UseExceptionHandler("/api/error");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAuthorizationService();

        app.MapControllers();

        app.Run();
    }
}
