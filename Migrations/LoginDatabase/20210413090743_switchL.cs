using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Datawarehouse_Backend.Migrations.LoginDatabase
{
    public partial class switchL : Migration
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
                    tennantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceAndBudget", x => x.id);
                    table.ForeignKey(
                        name: "FK_BalanceAndBudget_Tennant_tennantId",
                        column: x => x.tennantId,
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
                    tennantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.id);
                    table.ForeignKey(
                        name: "FK_Customer_Tennant_tennantId",
                        column: x => x.tennantId,
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
                    employeeName = table.Column<string>(type: "text", nullable: true),
                    tennantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.id);
                    table.ForeignKey(
                        name: "FK_Employee_Tennant_tennantId",
                        column: x => x.tennantId,
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
                    invoiceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    tennantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceInbound", x => x.id);
                    table.ForeignKey(
                        name: "FK_InvoiceInbound_Tennant_tennantId",
                        column: x => x.tennantId,
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
                    tennantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Tennant_tennantId",
                        column: x => x.tennantId,
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
                    customerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsReceivable", x => x.id);
                    table.ForeignKey(
                        name: "FK_AccountsReceivable_Customer_customerId",
                        column: x => x.customerId,
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
                    tennantId = table.Column<long>(type: "bigint", nullable: false),
                    invoiceOutboundId = table.Column<long>(type: "bigint", nullable: false),
                    customerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_customerId",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Tennant_tennantId",
                        column: x => x.tennantId,
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
                    fromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    toDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    employeeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsenceRegister", x => x.id);
                    table.ForeignKey(
                        name: "FK_AbsenceRegister_Employee_employeeId",
                        column: x => x.employeeId,
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
                    employeeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRegister", x => x.id);
                    table.ForeignKey(
                        name: "FK_TimeRegister_Employee_employeeId",
                        column: x => x.employeeId,
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
                    invoiceDue = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    orderId = table.Column<long>(type: "bigint", nullable: false),
                    customerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceOutbound", x => x.id);
                    table.ForeignKey(
                        name: "FK_InvoiceOutbound_Customer_customerId",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceOutbound_Order_orderId",
                        column: x => x.orderId,
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceRegister_employeeId",
                table: "AbsenceRegister",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivable_customerId",
                table: "AccountsReceivable",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceAndBudget_tennantId",
                table: "BalanceAndBudget",
                column: "tennantId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_tennantId",
                table: "Customer",
                column: "tennantId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_tennantId",
                table: "Employee",
                column: "tennantId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceInbound_tennantId",
                table: "InvoiceInbound",
                column: "tennantId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOutbound_customerId",
                table: "InvoiceOutbound",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOutbound_orderId",
                table: "InvoiceOutbound",
                column: "orderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_customerId",
                table: "Order",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_tennantId",
                table: "Order",
                column: "tennantId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegister_employeeId",
                table: "TimeRegister",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_tennantId",
                table: "Users",
                column: "tennantId");
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
