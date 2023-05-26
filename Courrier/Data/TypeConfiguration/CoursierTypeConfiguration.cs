using Courrier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courrier.Data.TypeConfiguration
{
    public class CoursierTypeConfiguration : IEntityTypeConfiguration<Coursier>
    {
        public void Configure(EntityTypeBuilder<Coursier> builder)
        {
            builder.ToTable("Coursier");
            builder.HasKey("Id");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();                      
        }
    }
}
