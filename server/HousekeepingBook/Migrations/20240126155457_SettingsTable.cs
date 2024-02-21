using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousekeepingBook.Migrations
{
    /// <inheritdoc />
    public partial class SettingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_MonthlyInvoiceSummaries_MonthlyInvoiceSummaryId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_MonthlyInvoiceSummaryId",
                table: "Invoices");

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContributionMembersCount = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    MonthId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingsId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_MonthlyInvoiceSummaryId",
                table: "Invoices",
                column: "MonthlyInvoiceSummaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_MonthlyInvoiceSummaries_MonthlyInvoiceSummaryId",
                table: "Invoices",
                column: "MonthlyInvoiceSummaryId",
                principalTable: "MonthlyInvoiceSummaries",
                principalColumn: "MonthlyInvoiceSummaryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
