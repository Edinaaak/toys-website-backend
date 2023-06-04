using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmetnickaDela.Data.Migrations
{
    public partial class addCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userDela_AspNetUsers_UserId",
                table: "userDela");

            migrationBuilder.DropForeignKey(
                name: "FK_userDela_umetnickaDela_DeloId",
                table: "userDela");

            migrationBuilder.AddForeignKey(
                name: "FK_userDela_AspNetUsers_UserId",
                table: "userDela",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userDela_umetnickaDela_DeloId",
                table: "userDela",
                column: "DeloId",
                principalTable: "umetnickaDela",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userDela_AspNetUsers_UserId",
                table: "userDela");

            migrationBuilder.DropForeignKey(
                name: "FK_userDela_umetnickaDela_DeloId",
                table: "userDela");

            migrationBuilder.AddForeignKey(
                name: "FK_userDela_AspNetUsers_UserId",
                table: "userDela",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_userDela_umetnickaDela_DeloId",
                table: "userDela",
                column: "DeloId",
                principalTable: "umetnickaDela",
                principalColumn: "Id");
        }
    }
}
