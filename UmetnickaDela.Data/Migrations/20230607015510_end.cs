using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmetnickaDela.Data.Migrations
{
    public partial class end : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userDela",
                table: "userDela");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "userDela",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userDela",
                table: "userDela",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_userDela_UserId",
                table: "userDela",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userDela",
                table: "userDela");

            migrationBuilder.DropIndex(
                name: "IX_userDela_UserId",
                table: "userDela");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "userDela");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userDela",
                table: "userDela",
                columns: new[] { "UserId", "DeloId" });
        }
    }
}
