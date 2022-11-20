using Microsoft.EntityFrameworkCore;
using SmartShopping.Models;

namespace SmartShopping.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>(entity => {
                    entity.HasIndex(entity => entity.Id).IsUnique();
                    entity.HasIndex(e => e.Email).IsUnique();
                    entity.Property(e => e.Name).IsRequired();
                    entity.Property(e => e.PasswordHash).IsRequired();
                })
                .Entity<Product>(entity => {
                    entity.HasIndex(entity => entity.Id).IsUnique();
                    entity.HasMany(e => e.PriceRecords).WithOne(e => e.Product);
                    entity.HasMany(e => e.Tags).WithMany(e => e.Products);
                    entity.HasMany(e => e.Shops).WithMany(e => e.Products);


                    entity.Property(e => e.SimplifiedName).IsRequired();
                    entity.Property(e => e.DisplayName).IsRequired();
                })
                .Entity<PriceRecord>(entity => {
                    entity.HasIndex(entity => entity.Id).IsUnique();
                    entity.HasOne(e => e.Shop).WithMany();
                    entity.HasOne(e => e.Product).WithMany(e => e.PriceRecords);

                    entity.Property(e => e.Price).IsRequired();
                })
                .Entity<ProductTag>(entity => {
                    entity.HasIndex(entity => entity.Id).IsUnique();
                    entity.HasMany(e => e.Products).WithMany(e => e.Tags);

                    entity.Property(e => e.SimplifiedName).IsRequired();
                    entity.Property(e => e.DisplayName).IsRequired();
                })
                .Entity<Shop>(entity => {
                    entity.HasIndex(entity => entity.Id).IsUnique();
                    entity.HasMany(e => e.Products).WithMany(e => e.Shops);

                    entity.Property(e => e.Name).IsRequired();
                });
        }
    }
}
