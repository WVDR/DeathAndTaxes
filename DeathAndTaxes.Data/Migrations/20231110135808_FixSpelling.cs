using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeathAndTaxes.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixSpelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCapotured",
                table: "TaxScores",
                newName: "DateCaptured");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCaptured",
                table: "TaxScores",
                newName: "DateCapotured");
        }
    }
}
