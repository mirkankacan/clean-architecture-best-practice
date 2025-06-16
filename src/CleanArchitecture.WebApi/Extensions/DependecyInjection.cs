using System.Text.Json.Serialization;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

namespace CleanArchitecture.WebApi.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration, IHostBuilder host)
        {
            services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                         .WithMethods("GET", "POST", "PUT", "DELETE")
                         .WithHeaders(
                             "Content-Type",
                             "Authorization",
                             "X-Requested-With",
                             "Accept",
                             "Origin"
                         )
                         .WithExposedHeaders("X-Pagination")
                         .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
                });
            });

            // Controller Configuration
            services.AddControllers(options =>
            {
                // Global filters can be added here
                // options.Filters.Add<GlobalExceptionFilter>();

                // Model validation configuration
                options.ModelValidatorProviders.Clear();
            });

            // JSON Configuration
            services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                options.SerializerOptions.WriteIndented = false;
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            // API Documentation
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(setup =>
            {
                var jwtSecuritySheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
            });

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "CleanArchitecture.WebApi")
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                .WriteTo.File("logs/application-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30,
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                .WriteTo.MSSqlServer(
                    connectionString: configuration.GetConnectionString("SqlConnection"),
                    sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        AutoCreateSqlTable = true
                    },
                    restrictedToMinimumLevel: LogEventLevel.Warning) // Only log warnings and errors to database
                .CreateLogger();
            host.UseSerilog();
            // Middleware Registration
            services.AddScoped<ExceptionMiddleware>();
            return services;
        }
    }
}