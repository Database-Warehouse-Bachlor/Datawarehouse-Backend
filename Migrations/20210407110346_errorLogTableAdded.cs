using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Datawarehouse_Backend.Migrations
{
    public partial class errorLogTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "Tennants");

            migrationBuilder.DropColumn(
                name: "zipcode",
                table: "Tennants");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Tennants",
                newName: "apiKey");

            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    errorMessage = table.Column<string>(type: "text", nullable: true),
                    timeOfError = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    tennantid = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.id);
                    table.ForeignKey(
                        name: "FK_ErrorLogs_Tennants_tennantid",
                        column: x => x.tennantid,
                        principalTable: "Tennants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_tennantid",
                table: "ErrorLogs",
                column: "tennantid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLogs");

            migrationBuilder.RenameColumn(
                name: "apiKey",
                table: "Tennants",
                newName: "city");

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Tennants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "zipcode",
                table: "Tennants",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
