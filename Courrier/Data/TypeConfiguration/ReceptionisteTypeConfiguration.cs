using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Courrier.Models;

namespace Courrier.Data.TypeConfiguration
{
    public class ReceptionisteTypeConfiguration : IEntityTypeConfiguration<Receptioniste>
    {
        public void Configure(EntityTypeBuilder<Receptioniste> builder)
        {
            builder.ToTable("Receptioniste");
            builder.HasKey("Id");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
