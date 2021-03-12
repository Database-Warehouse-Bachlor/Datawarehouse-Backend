using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Datawarehouse_Backend.Migrations
{
    public partial class WarehouseInitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbsenceRegisters",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    absenceId = table.Column<long>(type: "bigint", nullable: false),
                    employeeId = table.Column<long>(type: "bigint", nullable: false),
                    fromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    toDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    duration = table.Column<double>(type: "double precision", nullable: false),
                    soleCaretaker = table.Column<bool>(type: "boolean", nullable: false),
                    abcenseType = table.Column<string>(type: "text", nullable: false),
                    degreeDisability = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsenceRegisters", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbsenceRegisters");
        }
    }
}
