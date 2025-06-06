using Microsoft.EntityFrameworkCore;
using CentralMonitor.Application.Dto;
using CentralMonitor.Application.Interfaces;
using CentralMonitor.Infrastructure.Interfaces;
using System.Data;
using CentralMonitor.Domain.Entities;

namespace CentralMonitor.Application.Services;

public class TotalSalesMonitorService : ITotalSalesMonitorService
{
    private readonly IDbContextResolver _dbContextResolver;

    public TotalSalesMonitorService(IDbContextResolver dbContextResolver)
    {
        _dbContextResolver = dbContextResolver;
    }

    public Task<List<TotalSalesDto>> GetTotalUnitSalesByDateSPAsync(DateTime startDate, DateTime endDate)
    {
        return GetTotalUnitSalesAsync(startDate, endDate, GetTotalUnitSalesByDateSP);
    }

    public Task<List<TotalSalesDto>> GetTotalUnitSalesByDateEFAsync(DateTime startDate, DateTime endDate)
    {
        return GetTotalUnitSalesAsync(startDate, endDate, GetTotalUnitSalesByDateEF);
    }

    public Task<List<TotalSalesDto>> GetTotalUnitSalesByDateRawAsync(DateTime startDate, DateTime endDate)
    {
        return GetTotalUnitSalesAsync(startDate, endDate, GetTotalUnitSalesByDateRAW);
    }

    private async Task<List<TotalSalesDto>> GetTotalUnitSalesAsync(DateTime startDate, DateTime endDate, Func<UnitConnectionInfo, DateTime, DateTime, Task<TotalSalesDto>> salesFunc)
    {
        var unitsInfo = await _dbContextResolver.GetActiveUnitsAsync();

        if (unitsInfo == null || !unitsInfo.Any())
        {
            return new List<TotalSalesDto>();
        }

        var tasks = unitsInfo.Select(unit => salesFunc(unit, startDate, endDate)).ToList();

        var results = await Task.WhenAll(tasks);

        return results.ToList();
    }

    private async Task<TotalSalesDto> GetTotalUnitSalesByDateSP(UnitConnectionInfo unit, DateTime startDate, DateTime endDate)
    {
        using var db = _dbContextResolver.GetUnitDbContextAsync(unit);

        List<TotalSalesDto> totalSalesDto = await db.ExecuteStoredProcedureAsync(
            StoredProcedures.GetTotalSalesByDate,
            new Dictionary<string, object>
            {
                { "@BeginDate", startDate },
                { "@EndDate", endDate }
            },
            reader => new TotalSalesDto
            {
                UnitNo = unit.UnitNo,
                TotalAmount = reader.GetDecimal("TotalAmount")
            }
        );

        return totalSalesDto.FirstOrDefault() ?? new TotalSalesDto
        {
            UnitNo = unit.UnitNo,
            TotalAmount = 0
        };
    }

    private async Task<TotalSalesDto> GetTotalUnitSalesByDateEF(UnitConnectionInfo unit, DateTime startDate, DateTime endDate)
    {
        using var db = _dbContextResolver.GetUnitDbContextAsync(unit);

        var total = await db.Sales
            .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
            .SumAsync(s => s.Amount);

        return new TotalSalesDto
        {
            UnitNo = unit.UnitNo,
            TotalAmount = total
        };
    }

    private async Task<TotalSalesDto> GetTotalUnitSalesByDateRAW(UnitConnectionInfo unit, DateTime startDate, DateTime endDate)
    {
        using var db = _dbContextResolver.GetUnitDbContextAsync(unit);

        var total = await db.Sales
            .FromSqlRaw("SELECT * FROM Sale WHERE SaleDate BETWEEN {0} AND {1}", startDate, endDate)
            .SumAsync(s => s.Amount);

        return new TotalSalesDto
        {
            UnitNo = unit.UnitNo,
            TotalAmount = total
        };
    }
}