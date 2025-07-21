using Microsoft.EntityFrameworkCore;
using TestWebApi.Data.Configurations;
using TestWebApi.Models;

namespace TestWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Offer> Offers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new OfferConfiguration());
        }
    }       
    
}
