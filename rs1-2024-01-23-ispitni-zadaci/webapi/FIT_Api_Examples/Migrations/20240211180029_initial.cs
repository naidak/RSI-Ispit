using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "defaultOpstinaId",
                table: "KorisnickiNalog",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KorisnickiNalog_defaultOpstinaId",
                table: "KorisnickiNalog",
                column: "defaultOpstinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnickiNalog_Opstina_defaultOpstinaId",
                table: "KorisnickiNalog",
                column: "defaultOpstinaId",
                principalTable: "Opstina",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KorisnickiNalog_Opstina_defaultOpstinaId",
                table: "KorisnickiNalog");

            migrationBuilder.DropIndex(
                name: "IX_KorisnickiNalog_defaultOpstinaId",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "defaultOpstinaId",
                table: "KorisnickiNalog");
        }
    }
}
