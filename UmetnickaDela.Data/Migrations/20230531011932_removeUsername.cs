using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmetnickaDela.Data.Migrations
{
    public partial class removeUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_umetnickaDela_celine_celinaId",
                table: "umetnickaDela");

            migrationBuilder.AlterColumn<int>(
                name: "celinaId",
                table: "umetnickaDela",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_umetnickaDela_celine_celinaId",
                table: "umetnickaDela",
                column: "celinaId",
                principalTable: "celine",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_umetnickaDela_celine_celinaId",
                table: "umetnickaDela");

            migrationBuilder.AlterColumn<int>(
                name: "celinaId",
                table: "umetnickaDela",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_umetnickaDela_celine_celinaId",
                table: "umetnickaDela",
                column: "celinaId",
                principalTable: "celine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
