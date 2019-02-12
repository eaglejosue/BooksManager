using BooksManager.Domain.Entities;
using BooksManager.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BooksManager.Infra.Data.Context
{
    public class BooksManagerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public BooksManagerContext()
        {
            // Irá criar o banco e a estrutura de tabelas necessárias
            Database.EnsureCreated();
        }

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
    }
}
