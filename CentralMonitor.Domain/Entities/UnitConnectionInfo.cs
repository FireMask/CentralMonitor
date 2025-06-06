namespace CentralMonitor.Domain.Entities;

public class UnitConnectionInfo
{
    public int Id { get; set; }
    public string UnitNo { get; set; } = string.Empty;
    public string DbServer { get; set; } = string.Empty;
    public string DbName { get; set; } = string.Empty;
    public DateTime LastUpdate { get; set; }
    public bool IsActive { get; set; }
    public string District { get; set; } = string.Empty;
}