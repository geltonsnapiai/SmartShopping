using Microsoft.EntityFrameworkCore;
using SmartShopping.Models;

namespace SmartShopping.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IEntity>(entity => {
                    entity.HasIndex(entity => entity.Id).IsUnique();
                })
                .Entity<INamedEntity>(entity => {
                    entity.Property(e => e.SimplifiedName).IsRequired();
                    entity.Property(e => e.DisplayName).IsRequired();
                }) 
                .Entity<User>(entity => {
                    entity.HasIndex(e => e.Email).IsUnique();
                    entity.Property(e => e.Name).IsRequired();
                    entity.Property(e => e.PasswordHash).IsRequired();
                })
                .Entity<Product>(entity => {
                    entity.HasMany(e => e.PriceRecords).WithOne(e => e.Product);
                    entity.HasMany(e => e.Tags).WithMany(e => e.Products);
                    entity.HasMany(e => e.Shops).WithMany(e => e.Products);
                })
                .Entity<PriceRecord>(entity => {
                    entity.HasOne(e => e.Shop).WithMany();
                    entity.HasOne(e => e.Product).WithMany(e => e.PriceRecords);
                })
                .Entity<ProductTag>(entity => {
                    entity.HasMany(e => e.Products).WithMany(e => e.Tags);
                })
                .Entity<Shop>(entity => {
                    entity.HasMany(e => e.Products).WithMany(e => e.Shops);
                });
        }
    }
}
