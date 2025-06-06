using Microsoft.AspNetCore.Mvc;
using CentralMonitor.Application.Interfaces;

namespace CentralMonitor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ITotalSalesMonitorService _centralMonitorService;

    public SalesController(ITotalSalesMonitorService centralMonitorService)
    {
        _centralMonitorService = centralMonitorService;
    }

    [HttpGet("total-sales-sp")]
    public async Task<IActionResult> GetTotalSalesPerUnitSP()
    {
        try
        {
            var result = await _centralMonitorService.GetTotalUnitSalesByDateSPAsync(DateTime.Now.AddDays(-30), DateTime.Now);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("total-sales-ef")]
    public async Task<IActionResult> GetTotalSalesPerUnitEF()
    {
        try
        {
            var result = await _centralMonitorService.GetTotalUnitSalesByDateEFAsync(DateTime.Now.AddDays(-30), DateTime.Now);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("total-sales-raw")]
    public async Task<IActionResult> GetTotalSalesPerUnitRaw()
    {
        try
        {
            var result = await _centralMonitorService.GetTotalUnitSalesByDateRawAsync(DateTime.Now.AddDays(-30), DateTime.Now);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}