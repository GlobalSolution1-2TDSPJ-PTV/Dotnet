using FloodWatch.Domain.Entities;
using FloodWatch.Infrastructure.Context;
using FloodWatch.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = builder.Configuration["Swagger:Title"],
        Description = builder.Configuration["Swagger:Description"] + DateTime.Now.Year,
        Contact = new OpenApiContact()
        {
            Email = builder.Configuration["Swagger:Email"],
            Name = builder.Configuration["Swagger:Name"]
        }
    });
});

builder.Services.AddDbContext<FloodWatchContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle"));
});

builder.Services.AddScoped<IRepository<Alert>, Repository<Alert>>();
builder.Services.AddScoped<IRepository<Sensor>, Repository<Sensor>>();
builder.Services.AddScoped<IRepository<SensorReading>, Repository<SensorReading>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapControllers();

app.Run();
