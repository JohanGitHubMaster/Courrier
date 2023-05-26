using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Courrier.Models;

namespace Courrier.Data.TypeConfiguration
{
    public class DestinataireTypeConfiguration : IEntityTypeConfiguration<Destinataire>
    {
        public void Configure(EntityTypeBuilder<Destinataire> builder)
        {
            builder.ToTable("Destinataire");
            builder.HasKey("Id");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
