using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Datawarehouse_Backend.Migrations.LoginDatabase
{
    public partial class fieldToTennant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "orgNr",
                table: "users");

            migrationBuilder.AddColumn<long>(
                name: "tennantid",
                table: "users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tennant",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tennantName = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    zipcode = table.Column<int>(type: "integer", nullable: false),
                    city = table.Column<string>(type: "text", nullable: true),
                    businessId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tennant", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_tennantid",
                table: "users",
                column: "tennantid");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Tennant_tennantid",
                table: "users",
                column: "tennantid",
                principalTable: "Tennant",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Tennant_tennantid",
                table: "users");

            migrationBuilder.DropTable(
                name: "Tennant");

            migrationBuilder.DropIndex(
                name: "IX_users_tennantid",
                table: "users");

            migrationBuilder.DropColumn(
                name: "tennantid",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "orgNr",
                table: "users",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");
        }
    }
}
