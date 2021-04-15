using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Datawarehouse_Backend.Migrations
{
    public partial class ModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "account",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "amount",
                table: "TimeRegisters",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "invoiceRate",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isCaseworker",
                table: "TimeRegisters",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "orderId",
                table: "TimeRegisters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "payType",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "payTypeName",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "personDepartment",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "personDepartmentName",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "personName",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "processingCode",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "qyt",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "rate",
                table: "TimeRegisters",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "recordDepartment",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "recordDepartmentName",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "recordType",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "recordTypeName",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "summaryType",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "viaType",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "workComment",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "workplace",
                table: "TimeRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "year",
                table: "TimeRegisters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "birthdate",
                table: "Employees",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "employmentRate",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "employmentType",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isCaseworker",
                table: "Employees",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "leaveDate",
                table: "Employees",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "posistionCategoryId",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ssbPayType",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ssbPositionCode",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ssbPositionText",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "startDate",
                table: "Employees",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "statusText",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "account",
                table: "BalanceAndBudgets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "department",
                table: "BalanceAndBudgets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "endBalance",
                table: "BalanceAndBudgets",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "BalanceAndBudgets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "periodBalance",
                table: "BalanceAndBudgets",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "periodDate",
                table: "BalanceAndBudgets",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "startBalance",
                table: "BalanceAndBudgets",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "amount",
                table: "AccountsReceivables",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "amountDue",
                table: "AccountsReceivables",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "customerName",
                table: "AccountsReceivables",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dueDate",
                table: "AccountsReceivables",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "jobId",
                table: "AccountsReceivables",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "note",
                table: "AccountsReceivables",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "oderId",
                table: "AccountsReceivables",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "overdueNotice",
                table: "AccountsReceivables",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "recordDate",
                table: "AccountsReceivables",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "recordType",
                table: "AccountsReceivables",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "abcenseType",
                table: "AbsenceRegisters",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "abcenseTypeText",
                table: "AbsenceRegisters",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "comment",
                table: "AbsenceRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "degreeDisability",
                table: "AbsenceRegisters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "soleCaretaker",
                table: "AbsenceRegisters",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "role",
                table: "User");

            migrationBuilder.DropColumn(
                name: "account",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "amount",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "invoiceRate",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "isCaseworker",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "orderId",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "payType",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "payTypeName",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "personDepartment",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "personDepartmentName",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "personName",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "processingCode",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "qyt",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "rate",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "recordDepartment",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "recordDepartmentName",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "recordType",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "recordTypeName",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "summaryType",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "viaType",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "workComment",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "workplace",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "year",
                table: "TimeRegisters");

            migrationBuilder.DropColumn(
                name: "birthdate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "employmentRate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "employmentType",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "isCaseworker",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "leaveDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "posistionCategoryId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ssbPayType",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ssbPositionCode",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ssbPositionText",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "startDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "statusText",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "account",
                table: "BalanceAndBudgets");

            migrationBuilder.DropColumn(
                name: "department",
                table: "BalanceAndBudgets");

            migrationBuilder.DropColumn(
                name: "endBalance",
                table: "BalanceAndBudgets");

            migrationBuilder.DropColumn(
                name: "name",
                table: "BalanceAndBudgets");

            migrationBuilder.DropColumn(
                name: "periodBalance",
                table: "BalanceAndBudgets");

            migrationBuilder.DropColumn(
                name: "periodDate",
                table: "BalanceAndBudgets");

            migrationBuilder.DropColumn(
                name: "startBalance",
                table: "BalanceAndBudgets");

            migrationBuilder.DropColumn(
                name: "amount",
                table: "AccountsReceivables");

            migrationBuilder.DropColumn(
                name: "amountDue",
                table: "AccountsReceivables");

            migrationBuilder.DropColumn(
                name: "customerName",
                table: "AccountsReceivables");

            migrationBuilder.DropColumn(
                name: "dueDate",
                table: "AccountsReceivables");

            migrationBuilder.DropColumn(
                name: "jobId",
                table: "AccountsReceivables");

            migrationBuilder.DropColumn(
                name: "note",
                table: "AccountsReceivables");

            migrationBuilder.DropColumn(
                name: "oderId",
                table: "AccountsReceivables");

            migrationBuilder.DropColumn(
                name: "overdueNotice",
                table: "AccountsReceivables");

            migrationBuilder.DropColumn(
                name: "recordDate",
                table: "AccountsReceivables");

            migrationBuilder.DropColumn(
                name: "recordType",
                table: "AccountsReceivables");

            migrationBuilder.DropColumn(
                name: "abcenseType",
                table: "AbsenceRegisters");

            migrationBuilder.DropColumn(
                name: "abcenseTypeText",
                table: "AbsenceRegisters");

            migrationBuilder.DropColumn(
                name: "comment",
                table: "AbsenceRegisters");

            migrationBuilder.DropColumn(
                name: "degreeDisability",
                table: "AbsenceRegisters");

            migrationBuilder.DropColumn(
                name: "soleCaretaker",
                table: "AbsenceRegisters");
        }
    }
}
