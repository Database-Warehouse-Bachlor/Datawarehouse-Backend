using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Datawarehouse_Backend.Migrations.LoginDatabase
{
    public partial class UserRoleFieldNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tennant",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tennantName = table.Column<string>(type: "text", nullable: true),
                    businessId = table.Column<string>(type: "text", nullable: false),
                    apiKey = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tennant", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BalanceAndBudget",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BalanceAndBudgetId = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    account = table.Column<string>(type: "text", nullable: true),
                    periodDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    startBalance = table.Column<double>(type: "double precision", nullable: false),
                    periodBalance = table.Column<double>(type: "double precision", nullable: false),
                    endBalance = table.Column<double>(type: "double precision", nullable: false),
                    department = table.Column<string>(type: "text", nullable: true),
                    tennantFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceAndBudget", x => x.id);
                    table.ForeignKey(
                        name: "FK_BalanceAndBudget_Tennant_tennantFK",
                        column: x => x.tennantFK,
                        principalTable: "Tennant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    custommerId = table.Column<long>(type: "bigint", nullable: false),
                    customerName = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    zipcode = table.Column<int>(type: "integer", nullable: false),
                    city = table.Column<string>(type: "text", nullable: true),
                    isInactive = table.Column<bool>(type: "boolean", nullable: false),
                    tennantFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.id);
                    table.ForeignKey(
                        name: "FK_Customer_Tennant_tennantFK",
                        column: x => x.tennantFK,
                        principalTable: "Tennant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeId = table.Column<long>(type: "bigint", nullable: false),
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
                    employmentType = table.Column<string>(type: "text", nullable: true),
                    tennantFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.id);
                    table.ForeignKey(
                        name: "FK_Employee_Tennant_tennantFK",
                        column: x => x.tennantFK,
                        principalTable: "Tennant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceInbound",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    invoiceInboundId = table.Column<long>(type: "bigint", nullable: false),
                    invoiceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    jobId = table.Column<long>(type: "bigint", nullable: false),
                    supplierId = table.Column<long>(type: "bigint", nullable: false),
                    wholesalerId = table.Column<long>(type: "bigint", nullable: false),
                    amountTotal = table.Column<double>(type: "double precision", nullable: false),
                    specification = table.Column<string>(type: "text", nullable: true),
                    invoicePdf = table.Column<string>(type: "text", nullable: true),
                    tennantFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceInbound", x => x.id);
                    table.ForeignKey(
                        name: "FK_InvoiceInbound_Tennant_tennantFK",
                        column: x => x.tennantFK,
                        principalTable: "Tennant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    role = table.Column<string>(type: "text", nullable: true),
                    tennantFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Tennant_tennantFK",
                        column: x => x.tennantFK,
                        principalTable: "Tennant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountsReceivable",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountsReceivableId = table.Column<long>(type: "bigint", nullable: false),
                    recordDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    recordType = table.Column<string>(type: "text", nullable: true),
                    customerName = table.Column<string>(type: "text", nullable: true),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    amountDue = table.Column<double>(type: "double precision", nullable: false),
                    overdueNotice = table.Column<string>(type: "text", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    oderId = table.Column<long>(type: "bigint", nullable: false),
                    jobId = table.Column<long>(type: "bigint", nullable: false),
                    customerFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsReceivable", x => x.id);
                    table.ForeignKey(
                        name: "FK_AccountsReceivable_Customer_customerFK",
                        column: x => x.customerFK,
                        principalTable: "Customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    orderId = table.Column<long>(type: "bigint", nullable: false),
                    orderType = table.Column<string>(type: "text", nullable: true),
                    orderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    plannedDelivery = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    startedDelivery = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    endDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    confimedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    lastChanged = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    warrantyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    caseHandler = table.Column<string>(type: "text", nullable: true),
                    customerName = table.Column<string>(type: "text", nullable: true),
                    jobId = table.Column<long>(type: "bigint", nullable: false),
                    jobName = table.Column<string>(type: "text", nullable: true),
                    jobSiteId = table.Column<long>(type: "bigint", nullable: false),
                    isFixedPrice = table.Column<bool>(type: "boolean", nullable: false),
                    fixedPriceAmount = table.Column<double>(type: "double precision", nullable: false),
                    materials = table.Column<string>(type: "text", nullable: true),
                    hoursOfWork = table.Column<double>(type: "double precision", nullable: false),
                    hasWarranty = table.Column<bool>(type: "boolean", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    tennantFK = table.Column<long>(type: "bigint", nullable: false),
                    invoiceOutboundFK = table.Column<long>(type: "bigint", nullable: false),
                    customerFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_customerFK",
                        column: x => x.customerFK,
                        principalTable: "Customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Tennant_tennantFK",
                        column: x => x.tennantFK,
                        principalTable: "Tennant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbsenceRegister",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AbsenceRegisterId = table.Column<long>(type: "bigint", nullable: false),
                    fromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    toDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    duration = table.Column<double>(type: "double precision", nullable: false),
                    soleCaretaker = table.Column<bool>(type: "boolean", nullable: false),
                    abcenseType = table.Column<string>(type: "text", nullable: true),
                    abcenseTypeText = table.Column<string>(type: "text", nullable: true),
                    comment = table.Column<string>(type: "text", nullable: true),
                    degreeDisability = table.Column<string>(type: "text", nullable: true),
                    employeeFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsenceRegister", x => x.id);
                    table.ForeignKey(
                        name: "FK_AbsenceRegister_Employee_employeeFK",
                        column: x => x.employeeFK,
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
                    timeRegisterId = table.Column<long>(type: "bigint", nullable: false),
                    recordDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    isCaseworker = table.Column<bool>(type: "boolean", nullable: false),
                    personName = table.Column<string>(type: "text", nullable: true),
                    personDepartment = table.Column<string>(type: "text", nullable: true),
                    personDepartmentName = table.Column<string>(type: "text", nullable: true),
                    year = table.Column<int>(type: "integer", nullable: false),
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
                    summaryType = table.Column<string>(type: "text", nullable: true),
                    employeeFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRegister", x => x.id);
                    table.ForeignKey(
                        name: "FK_TimeRegister_Employee_employeeFK",
                        column: x => x.employeeFK,
                        principalTable: "Employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceOutbound",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    invoiceOutboundId = table.Column<long>(type: "bigint", nullable: false),
                    invoiceDue = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    jobId = table.Column<long>(type: "bigint", nullable: false),
                    invoiceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    invoiceExVat = table.Column<double>(type: "double precision", nullable: false),
                    invoiceIncVat = table.Column<double>(type: "double precision", nullable: false),
                    amountExVat = table.Column<double>(type: "double precision", nullable: false),
                    amountIncVat = table.Column<double>(type: "double precision", nullable: false),
                    amountTotal = table.Column<double>(type: "double precision", nullable: false),
                    orderFK = table.Column<long>(type: "bigint", nullable: false),
                    customerFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceOutbound", x => x.id);
                    table.ForeignKey(
                        name: "FK_InvoiceOutbound_Customer_customerFK",
                        column: x => x.customerFK,
                        principalTable: "Customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceOutbound_Order_orderFK",
                        column: x => x.orderFK,
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceRegister_employeeFK",
                table: "AbsenceRegister",
                column: "employeeFK");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivable_customerFK",
                table: "AccountsReceivable",
                column: "customerFK");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceAndBudget_tennantFK",
                table: "BalanceAndBudget",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_tennantFK",
                table: "Customer",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_tennantFK",
                table: "Employee",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceInbound_tennantFK",
                table: "InvoiceInbound",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOutbound_customerFK",
                table: "InvoiceOutbound",
                column: "customerFK");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOutbound_orderFK",
                table: "InvoiceOutbound",
                column: "orderFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_customerFK",
                table: "Order",
                column: "customerFK");

            migrationBuilder.CreateIndex(
                name: "IX_Order_tennantFK",
                table: "Order",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegister_employeeFK",
                table: "TimeRegister",
                column: "employeeFK");

            migrationBuilder.CreateIndex(
                name: "IX_Users_tennantFK",
                table: "Users",
                column: "tennantFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbsenceRegister");

            migrationBuilder.DropTable(
                name: "AccountsReceivable");

            migrationBuilder.DropTable(
                name: "BalanceAndBudget");

            migrationBuilder.DropTable(
                name: "InvoiceInbound");

            migrationBuilder.DropTable(
                name: "InvoiceOutbound");

            migrationBuilder.DropTable(
                name: "TimeRegister");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Tennant");
        }
    }
}
