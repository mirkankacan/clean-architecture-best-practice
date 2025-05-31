using CleanArchitecture.Application.Services;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("SqlConnection")!;
builder.Services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(connectionString));
builder.Services.AddMediatR(cfr =>
{
    cfr.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly);
});
builder.Services.AddControllers()
    .AddApplicationPart(assembly: typeof(CleanArchitecture.Presentation.AssemblyReference).Assembly);

builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddAutoMapper(typeof(CleanArchitecture.Application.AssemblyReference).Assembly);
// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer(); // Gerekli
builder.Services.AddSwaggerGen();           // Swagger yapýlandýrmasý

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();                        // Swagger JSON endpoint
    app.UseSwaggerUI();                      // Swagger UI
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();