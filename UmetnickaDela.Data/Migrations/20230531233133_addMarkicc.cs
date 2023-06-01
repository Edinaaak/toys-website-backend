using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmetnickaDela.Data.Migrations
{
    public partial class addMarkicc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userDela",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DeloId = table.Column<int>(type: "int", nullable: false),
                    Ocena = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userDela", x => new { x.UserId, x.DeloId });
                    table.ForeignKey(
                        name: "FK_userDela_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_userDela_umetnickaDela_DeloId",
                        column: x => x.DeloId,
                        principalTable: "umetnickaDela",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_userDela_DeloId",
                table: "userDela",
                column: "DeloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userDela");
        }
    }
}
