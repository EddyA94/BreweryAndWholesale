using BreweryWholesale.Domain.Models.DBO;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesale.Infrastructure
{
    public class BrewerWholesaleDBContext : DbContext
    {
        public BrewerWholesaleDBContext(DbContextOptions<BrewerWholesaleDBContext> options) : base(options)
        {
        }

        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<Wholesaler> Wholesaler { get; set; }
        public DbSet<WholesalerStock> WholesalerStock { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>().HasKey(x => x.BeerID);
            modelBuilder.Entity<Brewery>().HasKey(x => x.BrewerID);
            modelBuilder.Entity<Wholesaler>().HasKey(x => x.WholesalerID);
            modelBuilder.Entity<Sale>().HasKey(x => x.SaleID);
            modelBuilder.Entity<WholesalerStock>().HasKey(x => x.WholesalerStockID);
            base.OnModelCreating(modelBuilder);
        }
    }
}
