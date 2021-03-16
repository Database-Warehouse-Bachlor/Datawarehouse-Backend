using Microsoft.EntityFrameworkCore.Migrations;

namespace Datawarehouse_Backend.Migrations
{
    public partial class updatedUserfieldname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Tennants_tennantid",
                table: "User");

            migrationBuilder.AlterColumn<long>(
                name: "tennantid",
                table: "User",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Tennants_tennantid",
                table: "User",
                column: "tennantid",
                principalTable: "Tennants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Tennants_tennantid",
                table: "User");

            migrationBuilder.AlterColumn<long>(
                name: "tennantid",
                table: "User",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Tennants_tennantid",
                table: "User",
                column: "tennantid",
                principalTable: "Tennants",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
