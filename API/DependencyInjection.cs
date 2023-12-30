using System.Reflection;
using Domain.Common.Behaviors;
using Domain.Movies.Ports;
using Domain.Movies.UseCases;
using Domain.Theaters.Ports;
using Domain.Theaters.UseCases;
using Domain.Users.Ports;
using FluentValidation;
using Infrastructure.Hasher;
using Infrastructure.Storage.Repositories;
using MediatR;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        // TODO Make this dependency injection better
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            services.AddValidatorsFromAssembly(assembly);
        }

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IMovieUseCases, MovieUseCases>();
        services.AddScoped<IMovieRepository, MovieRepository>();

        services.AddScoped<ITheaterUseCases, TheaterUseCases>();
        services.AddScoped<ITheaterRepository, TheaterRepository>();

        return services;
    }
}
