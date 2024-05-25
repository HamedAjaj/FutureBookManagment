using FutureOFTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureOFTask.Repository.Config
{
    public class RatingConfigurations : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasOne(r => r.User)
                 .WithMany(u => u.Ratings).HasForeignKey(r => r.UserId);

            builder.HasOne(r => r.Book)
                .WithMany(b=>b.Ratings).HasForeignKey(r => r.BookId);
        }
    }
}
