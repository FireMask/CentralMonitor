using Microsoft.EntityFrameworkCore;
using CentralMonitor.Domain.Entities;

namespace CentralMonitor.Infrastructure.DbContexts;

public class UnitDbContext : DbContext
{
    public UnitDbContext(DbContextOptions<UnitDbContext> options) : base(options) { }

    public DbSet<Sale> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Sale>()
            .ToTable("Sale")
            .HasKey(s => s.Id);
    }
}