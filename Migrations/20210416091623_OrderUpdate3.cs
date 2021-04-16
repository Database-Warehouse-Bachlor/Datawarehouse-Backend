using Microsoft.EntityFrameworkCore.Migrations;

namespace Datawarehouse_Backend.Migrations
{
    public partial class OrderUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "custommerId",
                table: "Customers",
                newName: "customerId");

            migrationBuilder.RenameColumn(
                name: "oderId",
                table: "AccountsReceivables",
                newName: "customerId");

            migrationBuilder.AddColumn<long>(
                name: "employeeId",
                table: "TimeRegisters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "customerId",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "customerId",
                table: "InvoiceOutbounds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "orderId",
                table: "InvoiceOutbounds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "employeeId",
                table: "AbsenceRegisters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "employeeId",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "customerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "customerId",
                table: "InvoiceOutbounds");

            migrationBuilder.DropColumn(
                name: "orderId",
                table: "InvoiceOutbounds");

            migrationBuilder.DropColumn(
                name: "employeeId",
                table: "AbsenceRegisters");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "Customers",
                newName: "custommerId");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "AccountsReceivables",
                newName: "oderId");
        }
    }
}
