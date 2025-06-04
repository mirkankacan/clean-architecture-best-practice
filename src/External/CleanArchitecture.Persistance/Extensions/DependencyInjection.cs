using System.Reflection;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Repositories;
using CleanArchitecture.Persistance.Services;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Persistance.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            // Database Configuration
            string connectionString = configuration.GetConnectionString("SqlConnection")!;

            services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlServer(connectionString));

            // Repository Registration
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<AppDbContext>());

            // Service Registration

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICarService, CarService>();

            // AutoMapper Configuration
            services.AddAutoMapper(assembly);
            return services;
        }
    }
}