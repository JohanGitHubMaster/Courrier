﻿// <auto-generated />
using System;
using Courrier.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Courrier.Migrations
{
    [DbContext(typeof(CourrierContext))]
    [Migration("20230526112323_DebugDatabase")]
    partial class DebugDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Courrier.Models.CourrierDestinataire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourrierId")
                        .HasColumnType("int");

                    b.Property<int>("DestinataireId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourrierId");

                    b.HasIndex("DestinataireId");

                    b.ToTable("CourrierDestinataire");
                });

            modelBuilder.Entity("Courrier.Models.Courriers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Commentaire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CoursierId")
                        .HasColumnType("int");

                    b.Property<int?>("DestinataireId")
                        .HasColumnType("int");

                    b.Property<string>("Expediteur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlagId")
                        .HasColumnType("int");

                    b.Property<string>("Objet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReceptionisteId")
                        .HasColumnType("int");

                    b.Property<string>("Réferences")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoursierId");

                    b.HasIndex("DestinataireId");

                    b.HasIndex("FlagId");

                    b.HasIndex("ReceptionisteId");

                    b.HasIndex("StatusId");

                    b.ToTable("Courrier", (string)null);
                });

            modelBuilder.Entity("Courrier.Models.Coursier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Coursier", (string)null);
                });

            modelBuilder.Entity("Courrier.Models.Destinataire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Destinataire", (string)null);
                });

            modelBuilder.Entity("Courrier.Models.Flag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Flag", (string)null);
                });

            modelBuilder.Entity("Courrier.Models.MouvementCourrier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DatedeMouvement")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DestinataireId")
                        .HasColumnType("int");

                    b.Property<int?>("ReceptionisteId")
                        .HasColumnType("int");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DestinataireId");

                    b.HasIndex("ReceptionisteId");

                    b.HasIndex("StatusId");

                    b.ToTable("MouvementCourrier");
                });

            modelBuilder.Entity("Courrier.Models.Receptioniste", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Receptioniste", (string)null);
                });

            modelBuilder.Entity("Courrier.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Status", (string)null);
                });

            modelBuilder.Entity("Courrier.Models.CourrierDestinataire", b =>
                {
                    b.HasOne("Courrier.Models.Courriers", "Courrier")
                        .WithMany("CourrierDestinataires")
                        .HasForeignKey("CourrierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Courrier.Models.Destinataire", "Destinataire")
                        .WithMany()
                        .HasForeignKey("DestinataireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courrier");

                    b.Navigation("Destinataire");
                });

            modelBuilder.Entity("Courrier.Models.Courriers", b =>
                {
                    b.HasOne("Courrier.Models.Coursier", "Coursier")
                        .WithMany("Courriers")
                        .HasForeignKey("CoursierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Courrier.Models.Destinataire", null)
                        .WithMany("Courriers")
                        .HasForeignKey("DestinataireId");

                    b.HasOne("Courrier.Models.Flag", "Flag")
                        .WithMany("Courriers")
                        .HasForeignKey("FlagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Courrier.Models.Receptioniste", "Receptioniste")
                        .WithMany("Courriers")
                        .HasForeignKey("ReceptionisteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Courrier.Models.Status", "Status")
                        .WithMany("Courriers")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coursier");

                    b.Navigation("Flag");

                    b.Navigation("Receptioniste");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Courrier.Models.MouvementCourrier", b =>
                {
                    b.HasOne("Courrier.Models.Destinataire", "Destinataire")
                        .WithMany()
                        .HasForeignKey("DestinataireId");

                    b.HasOne("Courrier.Models.Receptioniste", "Receptioniste")
                        .WithMany()
                        .HasForeignKey("ReceptionisteId");

                    b.HasOne("Courrier.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.Navigation("Destinataire");

                    b.Navigation("Receptioniste");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Courrier.Models.Courriers", b =>
                {
                    b.Navigation("CourrierDestinataires");
                });

            modelBuilder.Entity("Courrier.Models.Coursier", b =>
                {
                    b.Navigation("Courriers");
                });

            modelBuilder.Entity("Courrier.Models.Destinataire", b =>
                {
                    b.Navigation("Courriers");
                });

            modelBuilder.Entity("Courrier.Models.Flag", b =>
                {
                    b.Navigation("Courriers");
                });

            modelBuilder.Entity("Courrier.Models.Receptioniste", b =>
                {
                    b.Navigation("Courriers");
                });

            modelBuilder.Entity("Courrier.Models.Status", b =>
                {
                    b.Navigation("Courriers");
                });
#pragma warning restore 612, 618
        }
    }
}
