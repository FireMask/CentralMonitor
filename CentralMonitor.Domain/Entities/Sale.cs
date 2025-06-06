namespace CentralMonitor.Domain.Entities;

public class Sale
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime SaleDate { get; set; }
}