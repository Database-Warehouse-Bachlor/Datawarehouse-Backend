using Microsoft.EntityFrameworkCore.Migrations;

namespace Datawarehouse_Backend.Migrations
{
    public partial class errorLogsTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_Tennants_tennantid",
                table: "ErrorLogs");

            migrationBuilder.DropIndex(
                name: "IX_ErrorLogs_tennantid",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "tennantid",
                table: "ErrorLogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "tennantid",
                table: "ErrorLogs",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_tennantid",
                table: "ErrorLogs",
                column: "tennantid");

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLogs_Tennants_tennantid",
                table: "ErrorLogs",
                column: "tennantid",
                principalTable: "Tennants",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
