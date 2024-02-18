using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousekeepingBook.Migrations
{
    /// <inheritdoc />
    public partial class addPreferredColorModeToSettingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "PreferredColorMode",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "light");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredColorMode",
                table: "Settings");

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Settings",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");
        }
    }
}
