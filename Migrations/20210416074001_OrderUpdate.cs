using Microsoft.EntityFrameworkCore.Migrations;

namespace Datawarehouse_Backend.Migrations
{
    public partial class OrderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvoiceOutbounds_orderFK",
                table: "InvoiceOutbounds");

            migrationBuilder.DropColumn(
                name: "invoiceOutboundFK",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOutbounds_orderFK",
                table: "InvoiceOutbounds",
                column: "orderFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvoiceOutbounds_orderFK",
                table: "InvoiceOutbounds");

            migrationBuilder.AddColumn<long>(
                name: "invoiceOutboundFK",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOutbounds_orderFK",
                table: "InvoiceOutbounds",
                column: "orderFK",
                unique: true);
        }
    }
}
