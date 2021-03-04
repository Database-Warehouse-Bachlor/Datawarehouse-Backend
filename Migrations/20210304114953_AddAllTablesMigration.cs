using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Datawarehouse_Backend.Migrations
{
    public partial class AddAllTablesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customerId = table.Column<long>(type: "bigint", nullable: false),
                    businessId = table.Column<long>(type: "bigint", nullable: false),
                    customerName = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    zipcode = table.Column<int>(type: "integer", nullable: false),
                    city = table.Column<string>(type: "text", nullable: true),
                    isInactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeId = table.Column<long>(type: "bigint", nullable: false),
                    tennantId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Employees", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceInbounds",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    invoiceId = table.Column<long>(type: "bigint", nullable: false),
                    tennantId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_InvoiceInbounds", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceOutbounds",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    invoiceId = table.Column<long>(type: "bigint", nullable: false),
                    customerId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_InvoiceOutbounds", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    orderId = table.Column<long>(type: "bigint", nullable: false),
                    tennantId = table.Column<long>(type: "bigint", nullable: false),
                    customerId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tennants",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tennantName = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    zipcode = table.Column<int>(type: "integer", nullable: false),
                    city = table.Column<string>(type: "text", nullable: true),
                    businessId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tennants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "timeRegisters",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_timeRegisters", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "InvoiceInbounds");

            migrationBuilder.DropTable(
                name: "InvoiceOutbounds");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Tennants");

            migrationBuilder.DropTable(
                name: "timeRegisters");
        }
    }
}
