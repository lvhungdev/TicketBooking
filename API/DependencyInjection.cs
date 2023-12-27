using Domain.Movies.Ports;
using Domain.Movies.UseCases;
using Infrastructure.Storage.Repositories;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IMovieUseCases, MovieUseCases>();
        services.AddScoped<IMovieRepository, MovieRepository>();

        return services;
    }
}
