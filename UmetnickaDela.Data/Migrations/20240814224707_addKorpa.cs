using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmetnickaDela.Data.Migrations
{
    public partial class addKorpa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "korpe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DeloId = table.Column<int>(type: "int", nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_korpe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_korpe_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_korpe_umetnickaDela_DeloId",
                        column: x => x.DeloId,
                        principalTable: "umetnickaDela",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_korpe_DeloId",
                table: "korpe",
                column: "DeloId");

            migrationBuilder.CreateIndex(
                name: "IX_korpe_UserId",
                table: "korpe",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "korpe");
        }
    }
}
