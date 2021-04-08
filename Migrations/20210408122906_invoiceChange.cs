using Microsoft.EntityFrameworkCore.Migrations;

namespace Datawarehouse_Backend.Migrations
{
    public partial class invoiceChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceInbounds_Tennants_tennantid",
                table: "InvoiceInbounds");

            migrationBuilder.RenameColumn(
                name: "tennantid",
                table: "InvoiceInbounds",
                newName: "tennantId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceInbounds_tennantid",
                table: "InvoiceInbounds",
                newName: "IX_InvoiceInbounds_tennantId");

            migrationBuilder.AlterColumn<long>(
                name: "tennantId",
                table: "InvoiceInbounds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceInbounds_Tennants_tennantId",
                table: "InvoiceInbounds",
                column: "tennantId",
                principalTable: "Tennants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceInbounds_Tennants_tennantId",
                table: "InvoiceInbounds");

            migrationBuilder.RenameColumn(
                name: "tennantId",
                table: "InvoiceInbounds",
                newName: "tennantid");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceInbounds_tennantId",
                table: "InvoiceInbounds",
                newName: "IX_InvoiceInbounds_tennantid");

            migrationBuilder.AlterColumn<long>(
                name: "tennantid",
                table: "InvoiceInbounds",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceInbounds_Tennants_tennantid",
                table: "InvoiceInbounds",
                column: "tennantid",
                principalTable: "Tennants",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
