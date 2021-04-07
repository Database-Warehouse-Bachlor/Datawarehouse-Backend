using Microsoft.EntityFrameworkCore.Migrations;

namespace Datawarehouse_Backend.Migrations.LoginDatabase
{
    public partial class addedRequiredField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Tennant_tennantid",
                table: "users");

            migrationBuilder.AlterColumn<long>(
                name: "tennantid",
                table: "users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_Tennant_tennantid",
                table: "users",
                column: "tennantid",
                principalTable: "Tennant",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Tennant_tennantid",
                table: "users");

            migrationBuilder.AlterColumn<long>(
                name: "tennantid",
                table: "users",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Tennant_tennantid",
                table: "users",
                column: "tennantid",
                principalTable: "Tennant",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
