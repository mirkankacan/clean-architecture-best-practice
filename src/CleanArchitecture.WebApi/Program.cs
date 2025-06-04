using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Infrastructure.Extensions;
using CleanArchitecture.Persistance.Extensions;
using CleanArchitecture.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container using extension methods
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseWebApiMiddlewares();

app.Run();