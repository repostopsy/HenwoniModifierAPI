using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenwoniDataModifierAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LanguageId",
                table: "RefCJTDescriptionTemplates",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDescriptionTemplates_LanguageId",
                table: "RefCJTDescriptionTemplates",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDescriptionTemplates_Languages_LanguageId",
                table: "RefCJTDescriptionTemplates",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDescriptionTemplates_Languages_LanguageId",
                table: "RefCJTDescriptionTemplates");

            migrationBuilder.DropIndex(
                name: "IX_RefCJTDescriptionTemplates_LanguageId",
                table: "RefCJTDescriptionTemplates");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "RefCJTDescriptionTemplates");
        }
    }
}
