using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAB17.Migrations
{
    public partial class ThirdM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autos_Clients_ClientsId",
                table: "Autos");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Autos_AutosId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Servises_ServisesId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Workers_WorkersId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "WorkersId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ServisesId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AutosId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClientsId",
                table: "Autos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Autos_Clients_ClientsId",
                table: "Autos",
                column: "ClientsId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Autos_AutosId",
                table: "Orders",
                column: "AutosId",
                principalTable: "Autos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Servises_ServisesId",
                table: "Orders",
                column: "ServisesId",
                principalTable: "Servises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Workers_WorkersId",
                table: "Orders",
                column: "WorkersId",
                principalTable: "Workers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autos_Clients_ClientsId",
                table: "Autos");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Autos_AutosId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Servises_ServisesId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Workers_WorkersId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "WorkersId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServisesId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AutosId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Autos_AutosId",
                table: "Orders",
                column: "AutosId",
                principalTable: "Autos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Servises_ServisesId",
                table: "Orders",
                column: "ServisesId",
                principalTable: "Servises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Workers_WorkersId",
                table: "Orders",
                column: "WorkersId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
