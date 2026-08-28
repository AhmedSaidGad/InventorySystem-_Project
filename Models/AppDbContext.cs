using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Models
{
    public class AppDbContext : DbContext
    {
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)           
                .WithOne(p => p.Category)           
                .HasForeignKey(p => p.CategoryID)   
                .OnDelete(DeleteBehavior.Restrict);

          
            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.Purchases)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierID)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<Purchase>()
                .HasMany(p => p.PurchaseItems)
                .WithOne(pi => pi.Purchase)
                .HasForeignKey(pi => pi.PurchaseID)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Product>()
                .HasMany(p => p.PurchaseItems)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sale>()
                .HasMany(s => s.SaleItems)
                .WithOne(si => si.Sale)
                .HasForeignKey(si => si.SaleID)
                .OnDelete(DeleteBehavior.Cascade); 

           
            modelBuilder.Entity<Product>()
                .HasMany(p => p.SaleItems)
                .WithOne(si => si.Product)
                .HasForeignKey(si => si.ProductID)
                .OnDelete(DeleteBehavior.Restrict); 

            
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique();
        }
    }
}