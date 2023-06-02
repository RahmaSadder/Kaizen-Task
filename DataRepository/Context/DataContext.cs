using Microsoft.EntityFrameworkCore;
using CoreRepository.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
namespace DataRepository.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> context) : base(context)
        { }

        public DbSet<Item> Item { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {

            base.OnConfiguring(dbContextOptionsBuilder);
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot config = builder.Build();
            var conString = config.GetConnectionString("DefaultConnection");
            dbContextOptionsBuilder.UseSqlite(conString);
        }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Item>()
             .HasIndex(item => item.Name)
             .IsUnique();

            builder.Entity<Warehouse>()
            .HasIndex(warehouse => warehouse.Name)
            .IsUnique();

            builder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();
        }
    }
}
