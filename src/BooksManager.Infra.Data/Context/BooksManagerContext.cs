using BooksManager.Domain.Entities;
using BooksManager.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Linq;

//// Descomentar para usar o Migrations
//using System.IO;
//using Microsoft.Extensions.Configuration;

namespace BooksManager.Infra.Data.Context
{
    public sealed class BooksManagerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public BooksManagerContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new BookMap());
            modelBuilder.ApplyConfiguration(new BookingMap());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        //// Descomentar para usar o Migrations
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // get the configuration from the app settings
        //    var config = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    // define the database to use
        //    optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        //}
    }
}
