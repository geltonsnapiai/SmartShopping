using Microsoft.EntityFrameworkCore;
using SmartShopping.Models;

namespace SmartShopping.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<User>(entity => entity.HasIndex(e => e.Id).IsUnique());
            builder.Entity<User>(entity => entity.HasIndex(e => e.Email).IsUnique());
            builder.Entity<User>(entity => entity.HasIndex(e => e.Name).IsUnique());
        }
    }
}
