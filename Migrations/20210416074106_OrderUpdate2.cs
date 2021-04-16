using Microsoft.EntityFrameworkCore.Migrations;

namespace Datawarehouse_Backend.Migrations
{
    public partial class OrderUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvoiceOutbounds_orderFK",
                table: "InvoiceOutbounds");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOutbounds_orderFK",
                table: "InvoiceOutbounds",
                column: "orderFK",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvoiceOutbounds_orderFK",
                table: "InvoiceOutbounds");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOutbounds_orderFK",
                table: "InvoiceOutbounds",
                column: "orderFK");
        }
    }
}
