using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Datawarehouse_Backend.Migrations
{
    public partial class ModelUpdate : Migration
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
                    businessId = table.Column<string>(type: "text", nullable: true),
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
                    startBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    periodBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    endBalance = table.Column<decimal>(type: "numeric", nullable: false),
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
                name: "Clients",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clientId = table.Column<long>(type: "bigint", nullable: false),
                    clientName = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    zipcode = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "text", nullable: true),
                    isInactive = table.Column<bool>(type: "boolean", nullable: false),
                    customer = table.Column<bool>(type: "boolean", nullable: false),
                    tennantFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.id);
                    table.ForeignKey(
                        name: "FK_Clients_Tennants_tennantFK",
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
                name: "FinancialYears",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    financialYearId = table.Column<long>(type: "bigint", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    customerAccount = table.Column<int>(type: "integer", nullable: false),
                    supplierAccount = table.Column<int>(type: "integer", nullable: false),
                    tennantFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialYears", x => x.id);
                    table.ForeignKey(
                        name: "FK_FinancialYears_Tennants_tennantFK",
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
                    clientName = table.Column<string>(type: "text", nullable: true),
                    jobId = table.Column<long>(type: "bigint", nullable: false),
                    jobName = table.Column<string>(type: "text", nullable: true),
                    jobSiteId = table.Column<long>(type: "bigint", nullable: false),
                    isFixedPrice = table.Column<bool>(type: "boolean", nullable: false),
                    fixedPriceAmount = table.Column<double>(type: "double precision", nullable: false),
                    materials = table.Column<string>(type: "text", nullable: true),
                    hoursOfWork = table.Column<double>(type: "double precision", nullable: false),
                    hasWarranty = table.Column<bool>(type: "boolean", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    clientId = table.Column<long>(type: "bigint", nullable: false),
                    tennantFK = table.Column<long>(type: "bigint", nullable: false),
                    clientFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_clientFK",
                        column: x => x.clientFK,
                        principalTable: "Clients",
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
                name: "Vouchers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    voucherId = table.Column<long>(type: "bigint", nullable: false),
                    number = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    paymentId = table.Column<int>(type: "integer", nullable: false),
                    clientId = table.Column<long>(type: "bigint", nullable: false),
                    clientFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Vouchers_Clients_clientFK",
                        column: x => x.clientFK,
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbsenceRegisters",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    absenceRegisterId = table.Column<long>(type: "bigint", nullable: false),
                    fromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    toDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    duration = table.Column<double>(type: "double precision", nullable: false),
                    soleCaretaker = table.Column<bool>(type: "boolean", nullable: false),
                    abcenseType = table.Column<string>(type: "text", nullable: true),
                    abcenseTypeText = table.Column<string>(type: "text", nullable: true),
                    comment = table.Column<string>(type: "text", nullable: true),
                    degreeDisability = table.Column<string>(type: "text", nullable: true),
                    employeeId = table.Column<long>(type: "bigint", nullable: false),
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
                    qty = table.Column<int>(type: "integer", nullable: false),
                    rate = table.Column<double>(type: "double precision", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
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
                    employeeId = table.Column<long>(type: "bigint", nullable: false),
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
                name: "Accounts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accountId = table.Column<long>(type: "bigint", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true),
                    MVAcode = table.Column<long>(type: "bigint", nullable: false),
                    SAFTcode = table.Column<long>(type: "bigint", nullable: false),
                    financialYearid = table.Column<long>(type: "bigint", nullable: false),
                    financialYearFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Accounts_FinancialYears_financialYearFK",
                        column: x => x.financialYearFK,
                        principalTable: "FinancialYears",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    voucherFK = table.Column<long>(type: "bigint", nullable: false),
                    invoiceId = table.Column<long>(type: "bigint", nullable: false),
                    dueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    clientId = table.Column<long>(type: "bigint", nullable: false),
                    amountTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    specification = table.Column<string>(type: "text", nullable: true),
                    invoicePdf = table.Column<string>(type: "text", nullable: true),
                    orderId = table.Column<long>(type: "bigint", nullable: false),
                    voucherId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.voucherFK);
                    table.ForeignKey(
                        name: "FK_Invoices_Vouchers_voucherFK",
                        column: x => x.voucherFK,
                        principalTable: "Vouchers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    postId = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    MVAcode = table.Column<long>(type: "bigint", nullable: false),
                    accountId = table.Column<long>(type: "bigint", nullable: false),
                    voucherId = table.Column<long>(type: "bigint", nullable: false),
                    voucherFK = table.Column<long>(type: "bigint", nullable: false),
                    accountFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Posts_Accounts_accountFK",
                        column: x => x.accountFK,
                        principalTable: "Accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Vouchers_voucherFK",
                        column: x => x.voucherFK,
                        principalTable: "Vouchers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLines",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    invoiceLineId = table.Column<long>(type: "bigint", nullable: false),
                    productName = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unit = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    amountTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    discount = table.Column<decimal>(type: "numeric", nullable: false),
                    invoiceId = table.Column<long>(type: "bigint", nullable: false),
                    invoiceFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLines", x => x.id);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Invoices_invoiceFK",
                        column: x => x.invoiceFK,
                        principalTable: "Invoices",
                        principalColumn: "voucherFK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceRegisters_employeeFK",
                table: "AbsenceRegisters",
                column: "employeeFK");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_financialYearFK",
                table: "Accounts",
                column: "financialYearFK");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceAndBudgets_tennantFK",
                table: "BalanceAndBudgets",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_tennantFK",
                table: "Clients",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_tennantFK",
                table: "Employees",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYears_tennantFK",
                table: "FinancialYears",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_invoiceFK",
                table: "InvoiceLines",
                column: "invoiceFK");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_clientFK",
                table: "Orders",
                column: "clientFK");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_tennantFK",
                table: "Orders",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_accountFK",
                table: "Posts",
                column: "accountFK");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_voucherFK",
                table: "Posts",
                column: "voucherFK");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegisters_employeeFK",
                table: "TimeRegisters",
                column: "employeeFK");

            migrationBuilder.CreateIndex(
                name: "IX_User_tennantFK",
                table: "User",
                column: "tennantFK");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_clientFK",
                table: "Vouchers",
                column: "clientFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbsenceRegisters");

            migrationBuilder.DropTable(
                name: "BalanceAndBudgets");

            migrationBuilder.DropTable(
                name: "ErrorLogs");

            migrationBuilder.DropTable(
                name: "InvoiceLines");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "TimeRegisters");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "FinancialYears");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Tennants");
        }
    }
}
