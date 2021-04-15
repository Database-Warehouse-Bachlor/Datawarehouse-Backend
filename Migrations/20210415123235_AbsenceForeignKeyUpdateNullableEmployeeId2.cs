using Microsoft.EntityFrameworkCore.Migrations;

namespace Datawarehouse_Backend.Migrations
{
    public partial class AbsenceForeignKeyUpdateNullableEmployeeId2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "amountTotal",
                table: "InvoiceInbounds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "invoicePdf",
                table: "InvoiceInbounds",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "jobId",
                table: "InvoiceInbounds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "specification",
                table: "InvoiceInbounds",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "supplierId",
                table: "InvoiceInbounds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "wholesalerId",
                table: "InvoiceInbounds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "customerName",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isInactive",
                table: "Customers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "tennantFK",
                table: "Customers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "zipcode",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amountTotal",
                table: "InvoiceInbounds");

            migrationBuilder.DropColumn(
                name: "invoicePdf",
                table: "InvoiceInbounds");

            migrationBuilder.DropColumn(
                name: "jobId",
                table: "InvoiceInbounds");

            migrationBuilder.DropColumn(
                name: "specification",
                table: "InvoiceInbounds");

            migrationBuilder.DropColumn(
                name: "supplierId",
                table: "InvoiceInbounds");

            migrationBuilder.DropColumn(
                name: "wholesalerId",
                table: "InvoiceInbounds");

            migrationBuilder.DropColumn(
                name: "address",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "customerName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "isInactive",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "tennantFK",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "zipcode",
                table: "Customers");
        }
    }
}
