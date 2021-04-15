using Microsoft.EntityFrameworkCore.Migrations;

namespace Datawarehouse_Backend.Migrations
{
    public partial class AbsenceForeignKeyUpdateNullableEmployeeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceRegisters_Employees_employeeId",
                table: "AbsenceRegisters");

            migrationBuilder.AlterColumn<long>(
                name: "employeeId",
                table: "AbsenceRegisters",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceRegisters_Employees_employeeId",
                table: "AbsenceRegisters",
                column: "employeeId",
                principalTable: "Employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceRegisters_Employees_employeeId",
                table: "AbsenceRegisters");

            migrationBuilder.AlterColumn<long>(
                name: "employeeId",
                table: "AbsenceRegisters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceRegisters_Employees_employeeId",
                table: "AbsenceRegisters",
                column: "employeeId",
                principalTable: "Employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
