using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Courrier.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata;
using Courrier.Data.TypeConfiguration;

namespace Courrier.Data
{
    public class CourrierContext : DbContext
    {
        public CourrierContext (DbContextOptions<CourrierContext> options)
            : base(options)
        {
        }

        public DbSet<Courriers> Courrier { get; set; } = default!;
        public DbSet<CourrierDestinataire> CourrierDestinataire { get; set; } = default!;
        public DbSet<Coursier> Coursier { get; set; } = default!;
        public DbSet<Destinataire> Destinataire { get; set; } = default!;
        public DbSet<MouvementCourrier> MouvementCourrier { get; set; } = default!;
        public DbSet<Receptioniste> Receptioniste { get; set; } = default!;
        public DbSet<Status> Status { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CourrierTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CoursierTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DestinataireTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FlagTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StatusTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReceptionisteTypeConfiguration());
        }

    }
}
