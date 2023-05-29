using Courrier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courrier.Data.TypeConfiguration
{
    public class StatusTypeConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Status");
            builder.HasKey("Id");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasMany(x => x.MouvementCourriers).WithOne(x => x.Status);
        }
    }
}
