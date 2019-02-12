using BooksManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksManager.Infra.Data.Mappings
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Title)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasColumnType("varchar(500)")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(c => c.Price)
                .IsRequired();

            builder.Property(c => c.Author)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Year)
                .IsRequired();

            builder.Property(c => c.Publisher)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(c => c.Edition)
                .IsRequired();

            builder.Property(c => c.Tag)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(c => c.Summary)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            //builder.Property(l => l.Image);


            builder.ToTable("Books");
        }
    }
}