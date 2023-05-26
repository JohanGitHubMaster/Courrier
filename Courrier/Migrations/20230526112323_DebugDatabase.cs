using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courrier.Migrations
{
    /// <inheritdoc />
    public partial class DebugDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //migrationBuilder.DropTable(
            //    name: "CourrierDestinataire");

            //migrationBuilder.DropTable(
            //    name: "MouvementCourrier");

            //migrationBuilder.DropTable(
            //    name: "Courrier");

            //migrationBuilder.DropTable(
            //    name: "Coursier");

            //migrationBuilder.DropTable(
            //    name: "Destinataire");

            //migrationBuilder.DropTable(
            //    name: "Flag");

            //migrationBuilder.DropTable(
            //    name: "Receptioniste");

            //migrationBuilder.DropTable(
            //    name: "Status");

            migrationBuilder.CreateTable(
                name: "Coursier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coursier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Destinataire",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinataire", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receptioniste",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptioniste", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courrier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Réferences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expediteur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Objet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoursierId = table.Column<int>(type: "int", nullable: false),
                    ReceptionisteId = table.Column<int>(type: "int", nullable: false),
                    FlagId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    DestinataireId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courrier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courrier_Coursier_CoursierId",
                        column: x => x.CoursierId,
                        principalTable: "Coursier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courrier_Destinataire_DestinataireId",
                        column: x => x.DestinataireId,
                        principalTable: "Destinataire",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Courrier_Flag_FlagId",
                        column: x => x.FlagId,
                        principalTable: "Flag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courrier_Receptioniste_ReceptionisteId",
                        column: x => x.ReceptionisteId,
                        principalTable: "Receptioniste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courrier_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MouvementCourrier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    DestinataireId = table.Column<int>(type: "int", nullable: true),
                    ReceptionisteId = table.Column<int>(type: "int", nullable: true),
                    DatedeMouvement = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MouvementCourrier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MouvementCourrier_Destinataire_DestinataireId",
                        column: x => x.DestinataireId,
                        principalTable: "Destinataire",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MouvementCourrier_Receptioniste_ReceptionisteId",
                        column: x => x.ReceptionisteId,
                        principalTable: "Receptioniste",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MouvementCourrier_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourrierDestinataire",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinataireId = table.Column<int>(type: "int", nullable: false),
                    CourrierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourrierDestinataire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourrierDestinataire_Courrier_CourrierId",
                        column: x => x.CourrierId,
                        principalTable: "Courrier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourrierDestinataire_Destinataire_DestinataireId",
                        column: x => x.DestinataireId,
                        principalTable: "Destinataire",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courrier_CoursierId",
                table: "Courrier",
                column: "CoursierId");

            migrationBuilder.CreateIndex(
                name: "IX_Courrier_DestinataireId",
                table: "Courrier",
                column: "DestinataireId");

            migrationBuilder.CreateIndex(
                name: "IX_Courrier_FlagId",
                table: "Courrier",
                column: "FlagId");

            migrationBuilder.CreateIndex(
                name: "IX_Courrier_ReceptionisteId",
                table: "Courrier",
                column: "ReceptionisteId");

            migrationBuilder.CreateIndex(
                name: "IX_Courrier_StatusId",
                table: "Courrier",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CourrierDestinataire_CourrierId",
                table: "CourrierDestinataire",
                column: "CourrierId");

            migrationBuilder.CreateIndex(
                name: "IX_CourrierDestinataire_DestinataireId",
                table: "CourrierDestinataire",
                column: "DestinataireId");

            migrationBuilder.CreateIndex(
                name: "IX_MouvementCourrier_DestinataireId",
                table: "MouvementCourrier",
                column: "DestinataireId");

            migrationBuilder.CreateIndex(
                name: "IX_MouvementCourrier_ReceptionisteId",
                table: "MouvementCourrier",
                column: "ReceptionisteId");

            migrationBuilder.CreateIndex(
                name: "IX_MouvementCourrier_StatusId",
                table: "MouvementCourrier",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourrierDestinataire");

            migrationBuilder.DropTable(
                name: "MouvementCourrier");

            migrationBuilder.DropTable(
                name: "Courrier");

            migrationBuilder.DropTable(
                name: "Coursier");

            migrationBuilder.DropTable(
                name: "Destinataire");

            migrationBuilder.DropTable(
                name: "Flag");

            migrationBuilder.DropTable(
                name: "Receptioniste");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
