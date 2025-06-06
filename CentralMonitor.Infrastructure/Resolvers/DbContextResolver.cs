using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CentralMonitor.Domain.Entities;
using CentralMonitor.Infrastructure.DbContexts;
using CentralMonitor.Infrastructure.Interfaces;

namespace CentralMonitor.Infrastructure.Resolvers;

public class DbContextResolver(CentralMonitorDbContext centralMonitorDbContext) : IDbContextResolver
{
    private readonly CentralMonitorDbContext _centralMonitorDbContext = centralMonitorDbContext;
    private readonly string _dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? throw new Exception("Missing DB_USER in .env");
    private readonly string _dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? throw new Exception("Missing DB_PASSWORD in .env");
    private readonly int _dbTimeout = int.TryParse(Environment.GetEnvironmentVariable("DB_TIMEOUT"), out var timeout) ? timeout : 30;

    public UnitDbContext GetUnitDbContextAsync(UnitConnectionInfo unitInfo)
    {
        if (unitInfo == null)
        {
            throw new InvalidOperationException($"Unit not found or is inactive.");
        }

        var connectionString = $"Server={unitInfo.DbServer};Database={unitInfo.DbName};User Id={_dbUser};Password={_dbPassword}; TrustServerCertificate=True;";

        var options = new DbContextOptionsBuilder<UnitDbContext>()
            .UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
                sqlOptions.CommandTimeout(_dbTimeout);
            })
            .Options;

        return new UnitDbContext(options);
    }

    public async Task<List<UnitConnectionInfo>> GetActiveUnitsAsync()
    {
        return await _centralMonitorDbContext.UnitConnections
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToListAsync();
    }
}