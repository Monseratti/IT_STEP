using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAB17.Migrations
{
    public partial class SecM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "Clients",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Clients");
        }
    }
}
