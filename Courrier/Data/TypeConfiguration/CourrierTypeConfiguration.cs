using Courrier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courrier.Data.TypeConfiguration
{
    public class CourrierTypeConfiguration : IEntityTypeConfiguration<Courriers>
    {
        public void Configure(EntityTypeBuilder<Courriers> builder)
        {
            builder.ToTable("Courrier");
            builder.HasKey("Id");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasMany(x => x.CourrierDestinataires).WithOne(x => x.Courrier);
            builder.HasMany(x => x.MouvementCourriers).WithOne(x => x.Courriers);

            builder.HasOne(x => x.Coursier).WithMany(x => x.Courriers).HasForeignKey(x => x.CoursierId);
            builder.HasOne(x => x.Receptioniste).WithMany(x => x.Courriers).HasForeignKey(x => x.ReceptionisteId);
            builder.HasOne(x => x.Status).WithMany(x => x.Courriers).HasForeignKey(x => x.StatusId);
            builder.HasOne(x => x.Flag).WithMany(x => x.Courriers).HasForeignKey(x => x.FlagId);


        }

    }
}
