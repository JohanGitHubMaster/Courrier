using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courrier.Data.TypeConfiguration
{
    public class FlagTypeConfiguration : IEntityTypeConfiguration<Courrier.Models.Flag>
    {
        public void Configure(EntityTypeBuilder<Courrier.Models.Flag> builder)
        {
            builder.ToTable("Flag");
            builder.HasKey("Id");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
