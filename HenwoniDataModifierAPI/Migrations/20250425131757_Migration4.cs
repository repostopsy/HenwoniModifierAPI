using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenwoniDataModifierAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LanguageId",
                table: "States",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LanguageId",
                table: "Countries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LanguageId",
                table: "Continents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LanguageId",
                table: "ContinentRegions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_LanguageId",
                table: "States",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_LanguageId",
                table: "Countries",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Continents_LanguageId",
                table: "Continents",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ContinentRegions_LanguageId",
                table: "ContinentRegions",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContinentRegions_Languages_LanguageId",
                table: "ContinentRegions",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Continents_Languages_LanguageId",
                table: "Continents",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Languages_LanguageId",
                table: "Countries",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_States_Languages_LanguageId",
                table: "States",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContinentRegions_Languages_LanguageId",
                table: "ContinentRegions");

            migrationBuilder.DropForeignKey(
                name: "FK_Continents_Languages_LanguageId",
                table: "Continents");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Languages_LanguageId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Languages_LanguageId",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_LanguageId",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Countries_LanguageId",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Continents_LanguageId",
                table: "Continents");

            migrationBuilder.DropIndex(
                name: "IX_ContinentRegions_LanguageId",
                table: "ContinentRegions");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "States");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Continents");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "ContinentRegions");
        }
    }
}
