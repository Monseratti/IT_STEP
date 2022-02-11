using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAB17.Migrations
{
    public partial class SixM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Servises",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Ended",
                table: "Servises",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Servises");

            migrationBuilder.DropColumn(
                name: "Ended",
                table: "Servises");
        }
    }
}
