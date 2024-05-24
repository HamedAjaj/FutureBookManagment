using FutureOFTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureOFTask.Repository.Config
{
    public class BookConfigurations : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(p => p.Author).IsRequired().HasMaxLength(100);
            builder.Property(p => p.ISBN).IsRequired();
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p.PublicationDate).IsRequired();
            builder.Property(p => p.Genre).IsRequired();
        }
    }
}
