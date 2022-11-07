using Microsoft.EntityFrameworkCore;
using SmartShopping.Models;

namespace SmartShopping.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<PriceRecord> PriceRecords { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity => {
                entity.HasIndex(e => e.Id).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });
            builder.Entity<Product>(entity => {
                entity.HasIndex(e => e.Id).IsUnique();
                entity.HasMany(e => e.PriceRecords).WithOne(e => e.Product);
                entity.HasMany(e => e.Tags).WithMany(e => e.Products);
                entity.HasMany(e => e.Shops).WithMany(e => e.Products);
            });
            builder.Entity<PriceRecord>(entity => {
                entity.HasIndex(e => e.Id).IsUnique();
                entity.HasOne(e => e.Shop).WithMany();

                entity.HasOne(e => e.Product).WithMany(e => e.PriceRecords);
            });
            builder.Entity<ProductTag>(entity => {
                entity.HasIndex(e => e.Id).IsUnique();

                entity.HasMany(e => e.Products).WithMany(e => e.Tags);
            });
            builder.Entity<Shop>(entity => {
                entity.HasIndex(e => e.Id).IsUnique();

                entity.HasMany(e => e.Products).WithMany(e => e.Shops);
            });
        }
    }
}
