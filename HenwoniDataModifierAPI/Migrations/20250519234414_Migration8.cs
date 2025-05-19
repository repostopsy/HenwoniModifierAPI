using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenwoniDataModifierAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "RefCJTDescriptionTemplates",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefCJTDescriptionTemplateAlias",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    LanguageId = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefCJTDescriptionTemplateId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCJTDescriptionTemplateAlias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefCJTDescriptionTemplateAlias_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RefCJTDescriptionTemplateAlias_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                        column: x => x.RefCJTDescriptionTemplateId,
                        principalTable: "RefCJTDescriptionTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefCJTDescriptionTemplateTag",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    LanguageId = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCJTDescriptionTemplateTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefCJTDescriptionTemplateTag_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefCJTDescriptionTemplateRefCJTDescriptionTemplateTag",
                columns: table => new
                {
                    TagsId = table.Column<long>(type: "bigint", nullable: false),
                    TemplatesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCJTDescriptionTemplateRefCJTDescriptionTemplateTag", x => new { x.TagsId, x.TemplatesId });
                    table.ForeignKey(
                        name: "FK_RefCJTDescriptionTemplateRefCJTDescriptionTemplateTag_RefCJTDescriptionTemplateTag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "RefCJTDescriptionTemplateTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefCJTDescriptionTemplateRefCJTDescriptionTemplateTag_RefCJTDescriptionTemplates_TemplatesId",
                        column: x => x.TemplatesId,
                        principalTable: "RefCJTDescriptionTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDescriptionTemplateAlias_LanguageId",
                table: "RefCJTDescriptionTemplateAlias",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDescriptionTemplateAlias_RefCJTDescriptionTemplateId",
                table: "RefCJTDescriptionTemplateAlias",
                column: "RefCJTDescriptionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDescriptionTemplateRefCJTDescriptionTemplateTag_TemplatesId",
                table: "RefCJTDescriptionTemplateRefCJTDescriptionTemplateTag",
                column: "TemplatesId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDescriptionTemplateTag_LanguageId",
                table: "RefCJTDescriptionTemplateTag",
                column: "LanguageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefCJTDescriptionTemplateAlias");

            migrationBuilder.DropTable(
                name: "RefCJTDescriptionTemplateRefCJTDescriptionTemplateTag");

            migrationBuilder.DropTable(
                name: "RefCJTDescriptionTemplateTag");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "RefCJTDescriptionTemplates");
        }
    }
}
