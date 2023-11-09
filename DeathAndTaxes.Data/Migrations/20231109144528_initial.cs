using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeathAndTaxes.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxCalculationTypes",
                columns: table => new
                {
                    TaxCalculationTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculationTypes", x => x.TaxCalculationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TaxIncomeBrackets",
                columns: table => new
                {
                    TaxIncomeBracketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncomeBracket = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxIncomeBrackets", x => x.TaxIncomeBracketId);
                });

            migrationBuilder.CreateTable(
                name: "TaxPercentageRates",
                columns: table => new
                {
                    TaxPercentageRateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PercentageRate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxPercentageRates", x => x.TaxPercentageRateId);
                });

            migrationBuilder.CreateTable(
                name: "PostalCodes",
                columns: table => new
                {
                    PostalCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxCalculationTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodes", x => x.PostalCodeId);
                    table.ForeignKey(
                        name: "FK_PostalCodes_TaxCalculationTypes_TaxCalculationTypeId",
                        column: x => x.TaxCalculationTypeId,
                        principalTable: "TaxCalculationTypes",
                        principalColumn: "TaxCalculationTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlatRateTaxes",
                columns: table => new
                {
                    FlatRateTaxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxPercentageRateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatRateTaxes", x => x.FlatRateTaxId);
                    table.ForeignKey(
                        name: "FK_FlatRateTaxes_TaxPercentageRates_TaxPercentageRateId",
                        column: x => x.TaxPercentageRateId,
                        principalTable: "TaxPercentageRates",
                        principalColumn: "TaxPercentageRateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlatValueTaxes",
                columns: table => new
                {
                    FlatValueTaxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Base = table.Column<double>(type: "float", nullable: false),
                    Months = table.Column<int>(type: "int", nullable: false),
                    TaxPercentageRateId = table.Column<int>(type: "int", nullable: false),
                    TaxIncomeBracketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatValueTaxes", x => x.FlatValueTaxId);
                    table.ForeignKey(
                        name: "FK_FlatValueTaxes_TaxIncomeBrackets_TaxIncomeBracketId",
                        column: x => x.TaxIncomeBracketId,
                        principalTable: "TaxIncomeBrackets",
                        principalColumn: "TaxIncomeBracketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlatValueTaxes_TaxPercentageRates_TaxPercentageRateId",
                        column: x => x.TaxPercentageRateId,
                        principalTable: "TaxPercentageRates",
                        principalColumn: "TaxPercentageRateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressiveTaxes",
                columns: table => new
                {
                    ProgressiveTaxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxPercentageRateId = table.Column<int>(type: "int", nullable: false),
                    TaxIncomeBracketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressiveTaxes", x => x.ProgressiveTaxId);
                    table.ForeignKey(
                        name: "FK_ProgressiveTaxes_TaxIncomeBrackets_TaxIncomeBracketId",
                        column: x => x.TaxIncomeBracketId,
                        principalTable: "TaxIncomeBrackets",
                        principalColumn: "TaxIncomeBracketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgressiveTaxes_TaxPercentageRates_TaxPercentageRateId",
                        column: x => x.TaxPercentageRateId,
                        principalTable: "TaxPercentageRates",
                        principalColumn: "TaxPercentageRateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlatRateTaxes_TaxPercentageRateId",
                table: "FlatRateTaxes",
                column: "TaxPercentageRateId");

            migrationBuilder.CreateIndex(
                name: "IX_FlatValueTaxes_TaxIncomeBracketId",
                table: "FlatValueTaxes",
                column: "TaxIncomeBracketId");

            migrationBuilder.CreateIndex(
                name: "IX_FlatValueTaxes_TaxPercentageRateId",
                table: "FlatValueTaxes",
                column: "TaxPercentageRateId");

            migrationBuilder.CreateIndex(
                name: "IX_PostalCodes_TaxCalculationTypeId",
                table: "PostalCodes",
                column: "TaxCalculationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressiveTaxes_TaxIncomeBracketId",
                table: "ProgressiveTaxes",
                column: "TaxIncomeBracketId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressiveTaxes_TaxPercentageRateId",
                table: "ProgressiveTaxes",
                column: "TaxPercentageRateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlatRateTaxes");

            migrationBuilder.DropTable(
                name: "FlatValueTaxes");

            migrationBuilder.DropTable(
                name: "PostalCodes");

            migrationBuilder.DropTable(
                name: "ProgressiveTaxes");

            migrationBuilder.DropTable(
                name: "TaxCalculationTypes");

            migrationBuilder.DropTable(
                name: "TaxIncomeBrackets");

            migrationBuilder.DropTable(
                name: "TaxPercentageRates");
        }
    }
}
