using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Datawarehouse_Backend.Migrations
{
    public partial class AddWarehouseTablesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accountsreceivables",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    oderId = table.Column<long>(type: "bigint", nullable: false),
                    recordDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    recordType = table.Column<string>(type: "text", nullable: true),
                    customerName = table.Column<string>(type: "text", nullable: true),
                    customerId = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    amountDue = table.Column<double>(type: "double precision", nullable: false),
                    overdueNotice = table.Column<string>(type: "text", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    jobId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accountsreceivables", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BalanceAndBudgets",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    account = table.Column<string>(type: "text", nullable: true),
                    tennantId = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    periodDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    startBalance = table.Column<double>(type: "double precision", nullable: false),
                    periodBalance = table.Column<double>(type: "double precision", nullable: false),
                    endBalance = table.Column<double>(type: "double precision", nullable: false),
                    department = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceAndBudgets", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accountsreceivables");

            migrationBuilder.DropTable(
                name: "BalanceAndBudgets");
        }
    }
}
