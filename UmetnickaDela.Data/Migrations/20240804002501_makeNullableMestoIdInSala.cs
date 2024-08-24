using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmetnickaDela.Data.Migrations
{
    public partial class makeNullableMestoIdInSala : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sale_mesta_MestoId",
                table: "sale");

            migrationBuilder.AlterColumn<int>(
                name: "MestoId",
                table: "sale",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_sale_mesta_MestoId",
                table: "sale",
                column: "MestoId",
                principalTable: "mesta",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sale_mesta_MestoId",
                table: "sale");

            migrationBuilder.AlterColumn<int>(
                name: "MestoId",
                table: "sale",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_sale_mesta_MestoId",
                table: "sale",
                column: "MestoId",
                principalTable: "mesta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
