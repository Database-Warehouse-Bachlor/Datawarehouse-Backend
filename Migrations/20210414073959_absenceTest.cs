using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Datawarehouse_Backend.Migrations
{
    public partial class absenceTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "recordDate",
                table: "TimeRegisters",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "duration",
                table: "AbsenceRegisters",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "recordDate",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "duration",
                table: "AbsenceRegisters");
        }
    }
}
