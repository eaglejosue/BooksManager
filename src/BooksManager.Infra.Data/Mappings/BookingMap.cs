using BooksManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksManager.Infra.Data.Mappings
{
    public class BookingMap : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.OwnsOne(c => c.BookingPeriod, cb =>
            {
                cb.Property(c => c.Start)
                    .HasColumnName("StartTime")
                    .IsRequired();

                cb.Property(c => c.End)
                    .HasColumnName("EndTime")
                    .IsRequired();
            });

            builder.Property(c => c.Price)
                .IsRequired();

            builder.HasOne(e => e.Book)
                .WithMany(o => o.Bookings)
                .HasForeignKey(e => e.BookId);

            builder.ToTable("Bookings");
        }
    }
}