using Microsoft.EntityFrameworkCore.Migrations;

namespace Datawarehouse_Backend.Migrations
{
    public partial class modelRelationCleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "Tennants");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Tennants");

            migrationBuilder.DropColumn(
                name: "zipcode",
                table: "Tennants");

            migrationBuilder.DropColumn(
                name: "businessId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "employeeId",
                table: "timeRegisters",
                newName: "employeeid");

            migrationBuilder.RenameColumn(
                name: "tennantId",
                table: "Orders",
                newName: "tennantid");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "Orders",
                newName: "customerid");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "InvoiceOutbounds",
                newName: "customerid");

            migrationBuilder.RenameColumn(
                name: "tennantId",
                table: "InvoiceInbounds",
                newName: "tennantid");

            migrationBuilder.RenameColumn(
                name: "tennantId",
                table: "Employees",
                newName: "tennantid");

            migrationBuilder.RenameColumn(
                name: "tennantId",
                table: "BalanceAndBudgets",
                newName: "tennantid");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "Accountsreceivables",
                newName: "customerid");

            migrationBuilder.RenameColumn(
                name: "employeeId",
                table: "AbsenceRegisters",
                newName: "employeeid");

            migrationBuilder.AlterColumn<long>(
                name: "employeeid",
                table: "timeRegisters",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "businessId",
                table: "Tennants",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "apiKey",
                table: "Tennants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "tennantid",
                table: "Orders",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "customerid",
                table: "Orders",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "customerid",
                table: "InvoiceOutbounds",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "tennantid",
                table: "InvoiceInbounds",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "tennantid",
                table: "Employees",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "tennantid",
                table: "Customers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "tennantid",
                table: "BalanceAndBudgets",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "customerid",
                table: "Accountsreceivables",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_timeRegisters_employeeid",
                table: "timeRegisters",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_customerid",
                table: "Orders",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_tennantid",
                table: "Orders",
                column: "tennantid");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOutbounds_customerid",
                table: "InvoiceOutbounds",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOutbounds_orderId",
                table: "InvoiceOutbounds",
                column: "orderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceInbounds_tennantid",
                table: "InvoiceInbounds",
                column: "tennantid");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_tennantid",
                table: "Employees",
                column: "tennantid");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_tennantid",
                table: "Customers",
                column: "tennantid");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceAndBudgets_tennantid",
                table: "BalanceAndBudgets",
                column: "tennantid");

            migrationBuilder.CreateIndex(
                name: "IX_Accountsreceivables_customerid",
                table: "Accountsreceivables",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceRegisters_employeeid",
                table: "AbsenceRegisters",
                column: "employeeid");

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceRegisters_Employees_employeeid",
                table: "AbsenceRegisters",
                column: "employeeid",
                principalTable: "Employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accountsreceivables_Customers_customerid",
                table: "Accountsreceivables",
                column: "customerid",
                principalTable: "Customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceAndBudgets_Tennants_tennantid",
                table: "BalanceAndBudgets",
                column: "tennantid",
                principalTable: "Tennants",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Tennants_tennantid",
                table: "Customers",
                column: "tennantid",
                principalTable: "Tennants",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Tennants_tennantid",
                table: "Employees",
                column: "tennantid",
                principalTable: "Tennants",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceInbounds_Tennants_tennantid",
                table: "InvoiceInbounds",
                column: "tennantid",
                principalTable: "Tennants",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceOutbounds_Customers_customerid",
                table: "InvoiceOutbounds",
                column: "customerid",
                principalTable: "Customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceOutbounds_Orders_orderId",
                table: "InvoiceOutbounds",
                column: "orderId",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_customerid",
                table: "Orders",
                column: "customerid",
                principalTable: "Customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Tennants_tennantid",
                table: "Orders",
                column: "tennantid",
                principalTable: "Tennants",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_timeRegisters_Employees_employeeid",
                table: "timeRegisters",
                column: "employeeid",
                principalTable: "Employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceRegisters_Employees_employeeid",
                table: "AbsenceRegisters");

            migrationBuilder.DropForeignKey(
                name: "FK_Accountsreceivables_Customers_customerid",
                table: "Accountsreceivables");

            migrationBuilder.DropForeignKey(
                name: "FK_BalanceAndBudgets_Tennants_tennantid",
                table: "BalanceAndBudgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Tennants_tennantid",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Tennants_tennantid",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceInbounds_Tennants_tennantid",
                table: "InvoiceInbounds");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceOutbounds_Customers_customerid",
                table: "InvoiceOutbounds");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceOutbounds_Orders_orderId",
                table: "InvoiceOutbounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_customerid",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Tennants_tennantid",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_timeRegisters_Employees_employeeid",
                table: "timeRegisters");

            migrationBuilder.DropIndex(
                name: "IX_timeRegisters_employeeid",
                table: "timeRegisters");

            migrationBuilder.DropIndex(
                name: "IX_Orders_customerid",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_tennantid",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceOutbounds_customerid",
                table: "InvoiceOutbounds");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceOutbounds_orderId",
                table: "InvoiceOutbounds");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceInbounds_tennantid",
                table: "InvoiceInbounds");

            migrationBuilder.DropIndex(
                name: "IX_Employees_tennantid",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Customers_tennantid",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_BalanceAndBudgets_tennantid",
                table: "BalanceAndBudgets");

            migrationBuilder.DropIndex(
                name: "IX_Accountsreceivables_customerid",
                table: "Accountsreceivables");

            migrationBuilder.DropIndex(
                name: "IX_AbsenceRegisters_employeeid",
                table: "AbsenceRegisters");

            migrationBuilder.DropColumn(
                name: "apiKey",
                table: "Tennants");

            migrationBuilder.DropColumn(
                name: "tennantid",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "employeeid",
                table: "timeRegisters",
                newName: "employeeId");

            migrationBuilder.RenameColumn(
                name: "tennantid",
                table: "Orders",
                newName: "tennantId");

            migrationBuilder.RenameColumn(
                name: "customerid",
                table: "Orders",
                newName: "customerId");

            migrationBuilder.RenameColumn(
                name: "customerid",
                table: "InvoiceOutbounds",
                newName: "customerId");

            migrationBuilder.RenameColumn(
                name: "tennantid",
                table: "InvoiceInbounds",
                newName: "tennantId");

            migrationBuilder.RenameColumn(
                name: "tennantid",
                table: "Employees",
                newName: "tennantId");

            migrationBuilder.RenameColumn(
                name: "tennantid",
                table: "BalanceAndBudgets",
                newName: "tennantId");

            migrationBuilder.RenameColumn(
                name: "customerid",
                table: "Accountsreceivables",
                newName: "customerId");

            migrationBuilder.RenameColumn(
                name: "employeeid",
                table: "AbsenceRegisters",
                newName: "employeeId");

            migrationBuilder.AlterColumn<long>(
                name: "employeeId",
                table: "timeRegisters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "businessId",
                table: "Tennants",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Tennants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Tennants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "zipcode",
                table: "Tennants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "tennantId",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "customerId",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "customerId",
                table: "InvoiceOutbounds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "tennantId",
                table: "InvoiceInbounds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "tennantId",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "businessId",
                table: "Customers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "tennantId",
                table: "BalanceAndBudgets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "customerId",
                table: "Accountsreceivables",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
