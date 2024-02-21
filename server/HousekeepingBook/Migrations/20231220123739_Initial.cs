using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousekeepingBook.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthlyInvoiceSummaries",
                columns: table => new
                {
                    MonthlyInvoiceSummaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonthId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyInvoiceSummaries", x => x.MonthlyInvoiceSummaryId);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreId);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<double>(type: "float", nullable: false),
                    CreateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonthlyInvoiceSummaryId = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_MonthlyInvoiceSummaries_MonthlyInvoiceSummaryId",
                        column: x => x.MonthlyInvoiceSummaryId,
                        principalTable: "MonthlyInvoiceSummaries",
                        principalColumn: "MonthlyInvoiceSummaryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_MonthlyInvoiceSummaryId",
                table: "Invoices",
                column: "MonthlyInvoiceSummaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_StoreId",
                table: "Invoices",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "MonthlyInvoiceSummaries");

            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}
