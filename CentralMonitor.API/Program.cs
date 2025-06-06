using Microsoft.EntityFrameworkCore;
using Serilog;
using CentralMonitor.Application.Interfaces;
using CentralMonitor.Application.Services;
using CentralMonitor.Infrastructure.DbContexts;
using CentralMonitor.Infrastructure.Interfaces;
using CentralMonitor.Infrastructure.Resolvers;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Env
Env.Load();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog(Log.Logger);

// Add DbContext
var centralConn = Environment.GetEnvironmentVariable("CENTRAL_DB");

builder.Services.AddDbContext<CentralMonitorDbContext>(options => options.UseSqlServer(centralConn));

// Add resolver & services
builder.Services.AddScoped<IDbContextResolver, DbContextResolver>();
builder.Services.AddScoped<ITotalSalesMonitorService, TotalSalesMonitorService>();

// Caching configuration
builder.Services.AddMemoryCache();

// Controller configuration
builder.Services.AddControllers();

// Add Swagger generator
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseSerilogRequestLogging();
app.MapControllers();

Console.WriteLine("Starting CentralMonitor.API...");

app.Run();