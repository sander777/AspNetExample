using System.Security.Cryptography.X509Certificates;
using Sander.TestTask.Persistance.Mssql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Sander.TestTask.Persistance.Mssql;

public class MarketDbContext : DbContext
{
    public MarketDbContext(DbContextOptions<MarketDbContext> options) : base(options)
    {
    }

    public DbSet<MarketItemEntity> MarketItems { get; set; }
    public DbSet<AuctionEntity> Auctions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MarketItemEntity>();
        modelBuilder.Entity<AuctionEntity>();
    }
}
