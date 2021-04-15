using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Datawarehouse_Backend.Migrations
{
    public partial class AbsenceForeignKeyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "caseHandler",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "confimedDate",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "customerName",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "endDate",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "fixedPriceAmount",
                table: "Orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "hasWarranty",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "hoursOfWork",
                table: "Orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "isFixedPrice",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "jobId",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "jobName",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "jobSiteId",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastChanged",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "materials",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "orderDate",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "orderType",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "plannedDelivery",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "startedDelivery",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "warrantyDate",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "amountExVat",
                table: "InvoiceOutbounds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "amountIncVat",
                table: "InvoiceOutbounds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "amountTotal",
                table: "InvoiceOutbounds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "invoiceDate",
                table: "InvoiceOutbounds",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "invoiceExVat",
                table: "InvoiceOutbounds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "invoiceIncVat",
                table: "InvoiceOutbounds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "jobId",
                table: "InvoiceOutbounds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "caseHandler",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "confimedDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "customerName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "endDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "fixedPriceAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "hasWarranty",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "hoursOfWork",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "isFixedPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "jobId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "jobName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "jobSiteId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "lastChanged",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "materials",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "orderDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "orderType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "plannedDelivery",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "startedDelivery",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "warrantyDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "amountExVat",
                table: "InvoiceOutbounds");

            migrationBuilder.DropColumn(
                name: "amountIncVat",
                table: "InvoiceOutbounds");

            migrationBuilder.DropColumn(
                name: "amountTotal",
                table: "InvoiceOutbounds");

            migrationBuilder.DropColumn(
                name: "invoiceDate",
                table: "InvoiceOutbounds");

            migrationBuilder.DropColumn(
                name: "invoiceExVat",
                table: "InvoiceOutbounds");

            migrationBuilder.DropColumn(
                name: "invoiceIncVat",
                table: "InvoiceOutbounds");

            migrationBuilder.DropColumn(
                name: "jobId",
                table: "InvoiceOutbounds");
        }
    }
}
