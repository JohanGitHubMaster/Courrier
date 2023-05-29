using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courrier.Data.TypeConfiguration
{
    public class MouvementCourrierTypeConfiguration : IEntityTypeConfiguration<Courrier.Models.MouvementCourrier>
    {
        public void Configure(EntityTypeBuilder<Courrier.Models.MouvementCourrier> builder)
        {
            builder.ToTable("MouvementCourrier");
            builder.HasKey("Id");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //builder.HasOne(x => x.Coursier).WithMany(x => x.MouvementCourriers).HasForeignKey(x => x.CoursierId);
            //builder.HasOne(x => x.Receptioniste).WithMany(x => x.MouvementCourriers).HasForeignKey(x => x.ReceptionisteId);
            //builder.HasOne(x => x.Status).WithMany(x => x.MouvementCourriers).HasForeignKey(x => x.StatusId);
            //builder.HasOne(x => x.Courriers).WithMany(x => x.MouvementCourriers).HasForeignKey(x => x.CourrierId);
        }
    }
}
