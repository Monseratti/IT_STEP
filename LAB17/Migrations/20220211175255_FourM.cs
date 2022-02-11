using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAB17.Migrations
{
    public partial class FourM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autos_Clients_ClientsId",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Autos");

            migrationBuilder.AlterColumn<int>(
                name: "ClientsId",
                table: "Autos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Autos_Clients_ClientsId",
                table: "Autos",
                column: "ClientsId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autos_Clients_ClientsId",
                table: "Autos");

            migrationBuilder.AlterColumn<int>(
                name: "ClientsId",
                table: "Autos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Autos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Autos_Clients_ClientsId",
                table: "Autos",
                column: "ClientsId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
