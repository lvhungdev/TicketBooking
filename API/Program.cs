using Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace API;

public static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddDependencies();

        WebApplication app = builder.Build();

        app.UseHttpsRedirection();

        // app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
