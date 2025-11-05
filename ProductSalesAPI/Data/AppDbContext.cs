using Microsoft.EntityFrameworkCore;
using ProductSalesAPI.Models;

namespace ProductSalesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Client)
                .WithMany()
                .HasForeignKey(s => s.ClientId);

            modelBuilder.Entity<SaleProduct>()
                .HasOne(sp => sp.Sale)
                .WithMany(s => s.SaleProducts)
                .HasForeignKey(sp => sp.SaleId);

            modelBuilder.Entity<SaleProduct>()
                .HasOne(sp => sp.Product)
                .WithMany()
                .HasForeignKey(sp => sp.ProductId);
        }
    }
}