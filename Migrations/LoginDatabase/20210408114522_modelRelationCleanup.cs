using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Datawarehouse_Backend.Migrations.LoginDatabase
{
    public partial class modelRelationCleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "Tennant");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Tennant");

            migrationBuilder.DropColumn(
                name: "zipcode",
                table: "Tennant");

            migrationBuilder.AlterColumn<string>(
                name: "businessId",
                table: "Tennant",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "apiKey",
                table: "Tennant",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BalanceAndBudget",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    account = table.Column<string>(type: "text", nullable: true),
                    tennantid = table.Column<long>(type: "bigint", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    periodDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    startBalance = table.Column<double>(type: "double precision", nullable: false),
                    periodBalance = table.Column<double>(type: "double precision", nullable: false),
                    endBalance = table.Column<double>(type: "double precision", nullable: false),
                    department = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceAndBudget", x => x.id);
                    table.ForeignKey(
                        name: "FK_BalanceAndBudget_Tennant_tennantid",
                        column: x => x.tennantid,
                        principalTable: "Tennant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customerId = table.Column<long>(type: "bigint", nullable: false),
                    tennantid = table.Column<long>(type: "bigint", nullable: true),
                    customerName = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    zipcode = table.Column<int>(type: "integer", nullable: false),
                    city = table.Column<string>(type: "text", nullable: true),
                    isInactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.id);
                    table.ForeignKey(
                        name: "FK_Customer_Tennant_tennantid",
                        column: x => x.tennantid,
                        principalTable: "Tennant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeId = table.Column<long>(type: "bigint", nullable: false),
                    tennantid = table.Column<long>(type: "bigint", nullable: true),
                    employeeName = table.Column<string>(type: "text", nullable: true),
                    birthdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    posistionCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    employmentRate = table.Column<int>(type: "integer", nullable: false),
                    startDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    leaveDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: true),
                    ssbPositionCode = table.Column<string>(type: "text", nullable: true),
                    ssbPositionText = table.Column<string>(type: "text", nullable: true),
                    ssbPayType = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    statusText = table.Column<string>(type: "text", nullable: true),
                    isCaseworker = table.Column<bool>(type: "boolean", nullable: false),
                    employmentType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.id);
                    table.ForeignKey(
                        name: "FK_Employee_Tennant_tennantid",
                        column: x => x.tennantid,
                        principalTable: "Tennant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceInbound",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    invoiceId = table.Column<long>(type: "bigint", nullable: false),
                    tennantid = table.Column<long>(type: "bigint", nullable: true),
                    jobId = table.Column<long>(type: "bigint", nullable: false),
                    supplierId = table.Column<long>(type: "bigint", nullable: false),
                    wholesalerId = table.Column<long>(type: "bigint", nullable: false),
                    invoiceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    amountTotal = table.Column<double>(type: "double precision", nullable: false),
                    specification = table.Column<string>(type: "text", nullable: true),
                    invoicePdf = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceInbound", x => x.id);
                    table.ForeignKey(
                        name: "FK_InvoiceInbound_Tennant_tennantid",
                        column: x => x.tennantid,
                        principalTable: "Tennant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accountsreceivable",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    oderId = table.Column<long>(type: "bigint", nullable: false),
                    recordDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    recordType = table.Column<string>(type: "text", nullable: true),
                    customerid = table.Column<long>(type: "bigint", nullable: true),
                    customerName = table.Column<string>(type: "text", nullable: true),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    amountDue = table.Column<double>(type: "double precision", nullable: false),
                    overdueNotice = table.Column<string>(type: "text", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    jobId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accountsreceivable", x => x.id);
                    table.ForeignKey(
                        name: "FK_Accountsreceivable_Customer_customerid",
                        column: x => x.customerid,
                        principalTable: "Customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    orderId = table.Column<long>(type: "bigint", nullable: false),
                    tennantid = table.Column<long>(type: "bigint", nullable: true),
                    customerid = table.Column<long>(type: "bigint", nullable: true),
                    orderType = table.Column<string>(type: "text", nullable: true),
                    orderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    caseHandler = table.Column<string>(type: "text", nullable: true),
                    customerName = table.Column<string>(type: "text", nullable: true),
                    jobId = table.Column<long>(type: "bigint", nullable: false),
                    jobName = table.Column<string>(type: "text", nullable: true),
                    jobSiteId = table.Column<long>(type: "bigint", nullable: false),
                    plannedDelivery = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    startedDelivery = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    endDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    confimedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    lastChanged = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    isFixedPrice = table.Column<bool>(type: "boolean", nullable: false),
                    fixedPriceAmount = table.Column<double>(type: "double precision", nullable: false),
                    materials = table.Column<string>(type: "text", nullable: true),
                    hoursOfWork = table.Column<double>(type: "double precision", nullable: false),
                    hasWarranty = table.Column<bool>(type: "boolean", nullable: false),
                    warrantyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_customerid",
                        column: x => x.customerid,
                        principalTable: "Customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Tennant_tennantid",
                        column: x => x.tennantid,
                        principalTable: "Tennant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AbsenceRegister",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    absenceId = table.Column<long>(type: "bigint", nullable: false),
                    employeeid = table.Column<long>(type: "bigint", nullable: false),
                    fromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    toDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    duration = table.Column<double>(type: "double precision", nullable: false),
                    soleCaretaker = table.Column<bool>(type: "boolean", nullable: false),
                    abcenseType = table.Column<string>(type: "text", nullable: false),
                    degreeDisability = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsenceRegister", x => x.id);
                    table.ForeignKey(
                        name: "FK_AbsenceRegister_Employee_employeeid",
                        column: x => x.employeeid,
                        principalTable: "Employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeRegister",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeid = table.Column<long>(type: "bigint", nullable: true),
                    isCaseworker = table.Column<bool>(type: "boolean", nullable: false),
                    personName = table.Column<string>(type: "text", nullable: true),
                    personDepartment = table.Column<string>(type: "text", nullable: true),
                    personDepartmentName = table.Column<string>(type: "text", nullable: true),
                    year = table.Column<int>(type: "integer", nullable: false),
                    recordDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    recordDepartment = table.Column<string>(type: "text", nullable: true),
                    recordDepartmentName = table.Column<string>(type: "text", nullable: true),
                    payType = table.Column<string>(type: "text", nullable: true),
                    payTypeName = table.Column<string>(type: "text", nullable: true),
                    qyt = table.Column<string>(type: "text", nullable: true),
                    rate = table.Column<double>(type: "double precision", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    invoiceRate = table.Column<string>(type: "text", nullable: true),
                    orderId = table.Column<long>(type: "bigint", nullable: false),
                    workplace = table.Column<string>(type: "text", nullable: true),
                    account = table.Column<string>(type: "text", nullable: true),
                    workComment = table.Column<string>(type: "text", nullable: true),
                    recordType = table.Column<string>(type: "text", nullable: true),
                    recordTypeName = table.Column<string>(type: "text", nullable: true),
                    processingCode = table.Column<string>(type: "text", nullable: true),
                    viaType = table.Column<string>(type: "text", nullable: true),
                    summaryType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRegister", x => x.id);
                    table.ForeignKey(
                        name: "FK_TimeRegister_Employee_employeeid",
                        column: x => x.employeeid,
                        principalTable: "Employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceOutbound",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    invoiceId = table.Column<long>(type: "bigint", nullable: false),
                    customerid = table.Column<long>(type: "bigint", nullable: true),
                    orderId = table.Column<long>(type: "bigint", nullable: false),
                    jobId = table.Column<long>(type: "bigint", nullable: false),
                    invoiceDue = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    invoiceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    invoiceExVat = table.Column<double>(type: "double precision", nullable: false),
                    invoiceIncVat = table.Column<double>(type: "double precision", nullable: false),
                    amountExVat = table.Column<double>(type: "double precision", nullable: false),
                    amountIncVat = table.Column<double>(type: "double precision", nullable: false),
                    amountTotal = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceOutbound", x => x.id);
                    table.ForeignKey(
                        name: "FK_InvoiceOutbound_Customer_customerid",
                        column: x => x.customerid,
                        principalTable: "Customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceOutbound_Order_orderId",
                        column: x => x.orderId,
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceRegister_employeeid",
                table: "AbsenceRegister",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_Accountsreceivable_customerid",
                table: "Accountsreceivable",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceAndBudget_tennantid",
                table: "BalanceAndBudget",
                column: "tennantid");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_tennantid",
                table: "Customer",
                column: "tennantid");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_tennantid",
                table: "Employee",
                column: "tennantid");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceInbound_tennantid",
                table: "InvoiceInbound",
                column: "tennantid");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOutbound_customerid",
                table: "InvoiceOutbound",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOutbound_orderId",
                table: "InvoiceOutbound",
                column: "orderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_customerid",
                table: "Order",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_Order_tennantid",
                table: "Order",
                column: "tennantid");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegister_employeeid",
                table: "TimeRegister",
                column: "employeeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbsenceRegister");

            migrationBuilder.DropTable(
                name: "Accountsreceivable");

            migrationBuilder.DropTable(
                name: "BalanceAndBudget");

            migrationBuilder.DropTable(
                name: "InvoiceInbound");

            migrationBuilder.DropTable(
                name: "InvoiceOutbound");

            migrationBuilder.DropTable(
                name: "TimeRegister");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropColumn(
                name: "apiKey",
                table: "Tennant");

            migrationBuilder.AlterColumn<string>(
                name: "businessId",
                table: "Tennant",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Tennant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Tennant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "zipcode",
                table: "Tennant",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
