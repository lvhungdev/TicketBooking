namespace API;

public static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        WebApplication app = builder.Build();

        app.UseHttpsRedirection();

        // app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
