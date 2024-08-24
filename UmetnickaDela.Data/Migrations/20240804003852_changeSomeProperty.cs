using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmetnickaDela.Data.Migrations
{
    public partial class changeSomeProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sirina",
                table: "umetnickaDela");

            migrationBuilder.DropColumn(
                name: "Visina",
                table: "umetnickaDela");

            migrationBuilder.AddColumn<decimal>(
                name: "Cena",
                table: "umetnickaDela",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Opis",
                table: "umetnickaDela",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cena",
                table: "umetnickaDela");

            migrationBuilder.DropColumn(
                name: "Opis",
                table: "umetnickaDela");

            migrationBuilder.AddColumn<float>(
                name: "Sirina",
                table: "umetnickaDela",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Visina",
                table: "umetnickaDela",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
