using InsecureCode.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InsecureCode.Infrastructure.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Message).IsRequired().HasMaxLength(200);
            builder.HasOne(x => x.Contributor).WithMany(x => x.Posts).HasForeignKey(x => x.ContributorId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
