using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenwoniDataModifierAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ServiceCategoryId",
                table: "RefCServiceTitles",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefCServiceTitles_ServiceCategoryId",
                table: "RefCServiceTitles",
                column: "ServiceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCServiceTitles_ServiceCategories_ServiceCategoryId",
                table: "RefCServiceTitles",
                column: "ServiceCategoryId",
                principalTable: "ServiceCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefCServiceTitles_ServiceCategories_ServiceCategoryId",
                table: "RefCServiceTitles");

            migrationBuilder.DropIndex(
                name: "IX_RefCServiceTitles_ServiceCategoryId",
                table: "RefCServiceTitles");

            migrationBuilder.DropColumn(
                name: "ServiceCategoryId",
                table: "RefCServiceTitles");
        }
    }
}
