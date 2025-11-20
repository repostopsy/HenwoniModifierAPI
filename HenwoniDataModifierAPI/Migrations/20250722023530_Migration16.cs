using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenwoniDataModifierAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Transilations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transilations_AuthorId",
                table: "Transilations",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transilations_AspNetUsers_AuthorId",
                table: "Transilations",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transilations_AspNetUsers_AuthorId",
                table: "Transilations");

            migrationBuilder.DropIndex(
                name: "IX_Transilations_AuthorId",
                table: "Transilations");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Transilations");
        }
    }
}
