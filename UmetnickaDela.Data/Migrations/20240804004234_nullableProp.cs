using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmetnickaDela.Data.Migrations
{
    public partial class nullableProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_umetnickaDela_AspNetUsers_slikarId",
                table: "umetnickaDela");

            migrationBuilder.AlterColumn<int>(
                name: "slikarId",
                table: "umetnickaDela",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_umetnickaDela_AspNetUsers_slikarId",
                table: "umetnickaDela",
                column: "slikarId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_umetnickaDela_AspNetUsers_slikarId",
                table: "umetnickaDela");

            migrationBuilder.AlterColumn<int>(
                name: "slikarId",
                table: "umetnickaDela",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_umetnickaDela_AspNetUsers_slikarId",
                table: "umetnickaDela",
                column: "slikarId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
