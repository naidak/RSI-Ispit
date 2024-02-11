using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UpisanaGodina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datumUpisa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    akademskaGodinaId = table.Column<int>(type: "int", nullable: false),
                    cijenaSkolarine = table.Column<float>(type: "real", nullable: false),
                    obnova = table.Column<bool>(type: "bit", nullable: false),
                    datumOvjere = table.Column<DateTime>(type: "datetime2", nullable: false),
                    napomena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    evidentiraoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpisanaGodina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpisanaGodina_AkademskaGodina_akademskaGodinaId",
                        column: x => x.akademskaGodinaId,
                        principalTable: "AkademskaGodina",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpisanaGodina_KorisnickiNalog_evidentiraoId",
                        column: x => x.evidentiraoId,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpisanaGodina_Student_studentId",
                        column: x => x.studentId,
                        principalTable: "Student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UpisanaGodina_akademskaGodinaId",
                table: "UpisanaGodina",
                column: "akademskaGodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisanaGodina_evidentiraoId",
                table: "UpisanaGodina",
                column: "evidentiraoId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisanaGodina_studentId",
                table: "UpisanaGodina",
                column: "studentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpisanaGodina");
        }
    }
}
