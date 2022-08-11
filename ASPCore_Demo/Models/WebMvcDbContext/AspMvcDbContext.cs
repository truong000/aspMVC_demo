using ASPCore_Demo.Models.EF;
using ASPCore_Demo.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASPCore_Demo.Models.WebMvcDbContext
{
    public class AspMvcDbContext : IdentityDbContext<AppUser>
    {
        public AspMvcDbContext(DbContextOptions<AspMvcDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring (DbContextOptionsBuilder builder)
        {
            base.OnConfiguring (builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }

        public DbSet<Product> ProductModels { get; set; }
    }
}
