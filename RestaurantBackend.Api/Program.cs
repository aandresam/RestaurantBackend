using Microsoft.EntityFrameworkCore;
using RestaurantBackend.Application.Persistence;
using RestaurantBackend.Application.Repositories;
using RestaurantBackend.Application.Services.Implementations;
using RestaurantBackend.Application.Services.Interfaces;
using RestaurantBackend.Infrastructure.Persistence;
using RestaurantBackend.Infrastructure.Persistence.Entities;
using RestaurantBackend.Infrastructure.Repositories;
using RestaurantBackend.Api.Middlewares;
using RestaurantBackend.Infrastructure.Persistence.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Falta `ConnectionStrings:DefaultConnection` en la configuración.");

builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseOracle(connectionString));

builder.Services.AddScoped<IRepository<Factura, int>, FacturaRepository>();
builder.Services.AddScoped<IRepository<DetalleFactura, int>, DetalleFacturaRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddScoped<IFacturaPersistence, FacturaPersistence>();
builder.Services.AddScoped<IFacturaService, FacturaService>();

builder.Services.AddScoped<IClientePersistence, ClientePersistence>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<IMeseroPersistence, MeseroPersistence>();
builder.Services.AddScoped<IMeseroService, MeseroService>();

builder.Services.AddScoped<IMesaPersistence, MesaPersistence>();
builder.Services.AddScoped<IMesaService, MesaService>();

builder.Services.AddScoped<ISupervisorPersistence, SupervisorPersistence>();
builder.Services.AddScoped<ISupervisorService, SupervisorService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins",
        policy =>
        {
            if (builder.Environment.IsDevelopment())
            {
                policy.WithOrigins("http://localhost:4200", "http://localhost:4500")
                      .AllowCredentials()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            }
            else
            {
                policy.WithOrigins(
                    "http://restaurant-app.aguirrelabs.dev",
                    "https://restaurant-app.aguirrelabs.dev"
                )
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader();

            }
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandling();

app.UseHttpsRedirection();
app.UseCors("AllowedOrigins");
app.UseAuthorization();
app.MapControllers();
app.Run();
