using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenwoniDataModifierAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LanguageId",
                table: "Cities",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_LanguageId",
                table: "Cities",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Languages_LanguageId",
                table: "Cities",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Languages_LanguageId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_LanguageId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Cities");
        }
    }
}
