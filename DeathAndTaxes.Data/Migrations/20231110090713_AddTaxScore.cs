using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeathAndTaxes.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTaxScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxScores",
                columns: table => new
                {
                    TaxScoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Income = table.Column<double>(type: "float", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    DateCapotured = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostalCodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxScores", x => x.TaxScoreId);
                    table.ForeignKey(
                        name: "FK_TaxScores_PostalCodes_PostalCodeId",
                        column: x => x.PostalCodeId,
                        principalTable: "PostalCodes",
                        principalColumn: "PostalCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxScores_PostalCodeId",
                table: "TaxScores",
                column: "PostalCodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxScores");
        }
    }
}
