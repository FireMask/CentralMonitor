using CentralMonitor.Domain.Entities;
using CentralMonitor.Infrastructure.DbContexts;

namespace CentralMonitor.Infrastructure.Interfaces;

public interface IDbContextResolver
{
    UnitDbContext GetUnitDbContextAsync(UnitConnectionInfo unitInfo);
    Task<List<UnitConnectionInfo>> GetActiveUnitsAsync();
}