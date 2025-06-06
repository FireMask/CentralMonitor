using CentralMonitor.Application.Dto;

namespace CentralMonitor.Application.Interfaces;

public interface ITotalSalesMonitorService
{
    Task<List<TotalSalesDto>> GetTotalUnitSalesByDateSPAsync(DateTime startDate, DateTime endDate);
    Task<List<TotalSalesDto>> GetTotalUnitSalesByDateEFAsync(DateTime startDate, DateTime endDate);
    Task<List<TotalSalesDto>> GetTotalUnitSalesByDateRawAsync(DateTime startDate, DateTime endDate);
}