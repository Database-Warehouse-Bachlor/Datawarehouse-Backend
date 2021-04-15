using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Datawarehouse_Backend.Migrations
{
    public partial class CustumerUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    errorType = table.Column<string>(type: "text", nullable: false),
                    errorMessage = table.Column<string>(type: "text", nullable: false),
                    timeOfError = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tennants",
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
                    table.PrimaryKey("PK_Tennants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BalanceAndBudgets",
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
                    table.PrimaryKey("PK_BalanceAndBudgets", x => x.id);
                    table.ForeignKey(
                        name: "FK_BalanceAndBudgets_Tennants_tennantFK",
                        column: x => x.tennantFK,
                        principalTable: "Tennants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
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
                    table.PrimaryKey("PK_Customers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Customers_Tennants_tennantFK",
                        column: x => x.tennantFK,
                        principalTable: "Tennants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
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
                    table.PrimaryKey("PK_Employees", x => x.id);
                    table.ForeignKey(
                        name: "FK_Employees_Tennants_tennantFK",
                        column: x => x.tennantFK,
                        principalTable: "Tennants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceInbounds",
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
                    table.PrimaryKey("PK_InvoiceInbounds", x => x.id);
                    table.ForeignKey(
                        name: "FK_InvoiceInbounds_Tennants_tennantFK",
                        column: x => x.tennantFK,
                        principalTable: "Tennants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
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
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Tennants_tennantFK",
                        column: x => x.tennantFK,
                        principalTable: "Tennants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountsReceivables",
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
                    table.PrimaryKey("PK_AccountsReceivables", x => x.id);
                    table.ForeignKey(
                        name: "FK_AccountsReceivables_Customers_customerFK",
                        column: x => x.customerFK,
                        principalTable: "Customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
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
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_customerFK",
                        column: x => x.customerFK,
                        principalTable: "Customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Tennants_tennantFK",
                        column: x => x.tennantFK,
                        principalTable: "Tennants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbsenceRegisters",
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
                    table.PrimaryKey("PK_AbsenceRegisters", x => x.id);
                    table.ForeignKey(
                        name: "FK_AbsenceRegisters_Employees_employeeFK",
                        column: x => x.employeeFK,
                        principalTable: "Employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeRegisters",
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
                    table.PrimaryKey("PK_TimeRegisters", x => x.id);
                    table.ForeignKey(
                        name: "FK_TimeRegisters_Employees_employeeFK",
                        column: x => x.employeeFK,
                        principalTable: "Employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceOutbounds",
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
                    table.PrimaryKey("PK_InvoiceOutbounds", x => x.id);
                    table.ForeignKey(
                        name: "FK_InvoiceOutbounds_Customers_customerFK",
                        column: x => x.customerFK,
                        principalTable: "Customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceOutbounds_Orders_orderFK",
                        column: x => x.orderFK,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceRegisters_employeeFK",
                table: "AbsenceRegisters",
                column: "employeeFK");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivables_customerFK",
                table: "AccountsReceivables",
                column: "customerFK");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceAndBudgets_tennantFK",
                table: "BalanceAndBudgets",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_tennantFK",
                table: "Customers",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_tennantFK",
                table: "Employees",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceInbounds_tennantFK",
                table: "InvoiceInbounds",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOutbounds_customerFK",
                table: "InvoiceOutbounds",
                column: "customerFK");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOutbounds_orderFK",
                table: "InvoiceOutbounds",
                column: "orderFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_customerFK",
                table: "Orders",
                column: "customerFK");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_tennantFK",
                table: "Orders",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegisters_employeeFK",
                table: "TimeRegisters",
                column: "employeeFK");

            migrationBuilder.CreateIndex(
                name: "IX_User_tennantFK",
                table: "User",
                column: "tennantFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbsenceRegisters");

            migrationBuilder.DropTable(
                name: "AccountsReceivables");

            migrationBuilder.DropTable(
                name: "BalanceAndBudgets");

            migrationBuilder.DropTable(
                name: "ErrorLogs");

            migrationBuilder.DropTable(
                name: "InvoiceInbounds");

            migrationBuilder.DropTable(
                name: "InvoiceOutbounds");

            migrationBuilder.DropTable(
                name: "TimeRegisters");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Tennants");
        }
    }
}
