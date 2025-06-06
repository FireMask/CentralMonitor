using Microsoft.EntityFrameworkCore;
using CentralMonitor.Domain.Entities;

namespace CentralMonitor.Infrastructure.DbContexts;

public class CentralMonitorDbContext : DbContext
{
    public CentralMonitorDbContext(DbContextOptions<CentralMonitorDbContext> options) : base(options) { }

    public DbSet<UnitConnectionInfo> UnitConnections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UnitConnectionInfo>()
            .ToTable("UnitConnections")
            .HasKey(s => s.Id);
    }

}