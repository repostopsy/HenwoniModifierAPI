using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenwoniDataModifierAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HowToHire",
                table: "RefCServiceTitleTemplates",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HowToHire",
                table: "RefCServiceTitles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LanguageId",
                table: "RefCommonJobTitles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "RefCommonJobTitles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PeriodInDays",
                table: "PlatformSubscriptionPlans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ISO6391",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefCJTDTemplateIntro",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    LanguageId = table.Column<long>(type: "bigint", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefCJTDescriptionTemplateId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCJTDTemplateIntro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefCJTDTemplateIntro_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RefCJTDTemplateIntro_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                        column: x => x.RefCJTDescriptionTemplateId,
                        principalTable: "RefCJTDescriptionTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefCJTDTemplateResponsibility",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    LanguageId = table.Column<long>(type: "bigint", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefCJTDescriptionTemplateId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCJTDTemplateResponsibility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefCJTDTemplateResponsibility_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RefCJTDTemplateResponsibility_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                        column: x => x.RefCJTDescriptionTemplateId,
                        principalTable: "RefCJTDescriptionTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefCJTDTemplateSkillExperience",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    LanguageId = table.Column<long>(type: "bigint", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefCJTDescriptionTemplateId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCJTDTemplateSkillExperience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefCJTDTemplateSkillExperience_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RefCJTDTemplateSkillExperience_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                        column: x => x.RefCJTDescriptionTemplateId,
                        principalTable: "RefCJTDescriptionTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefCommonJobTitles_LanguageId",
                table: "RefCommonJobTitles",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDTemplateIntro_LanguageId",
                table: "RefCJTDTemplateIntro",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDTemplateIntro_RefCJTDescriptionTemplateId",
                table: "RefCJTDTemplateIntro",
                column: "RefCJTDescriptionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDTemplateResponsibility_LanguageId",
                table: "RefCJTDTemplateResponsibility",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDTemplateResponsibility_RefCJTDescriptionTemplateId",
                table: "RefCJTDTemplateResponsibility",
                column: "RefCJTDescriptionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDTemplateSkillExperience_LanguageId",
                table: "RefCJTDTemplateSkillExperience",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDTemplateSkillExperience_RefCJTDescriptionTemplateId",
                table: "RefCJTDTemplateSkillExperience",
                column: "RefCJTDescriptionTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCommonJobTitles_Languages_LanguageId",
                table: "RefCommonJobTitles",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefCommonJobTitles_Languages_LanguageId",
                table: "RefCommonJobTitles");

            migrationBuilder.DropTable(
                name: "RefCJTDTemplateIntro");

            migrationBuilder.DropTable(
                name: "RefCJTDTemplateResponsibility");

            migrationBuilder.DropTable(
                name: "RefCJTDTemplateSkillExperience");

            migrationBuilder.DropIndex(
                name: "IX_RefCommonJobTitles_LanguageId",
                table: "RefCommonJobTitles");

            migrationBuilder.DropColumn(
                name: "HowToHire",
                table: "RefCServiceTitleTemplates");

            migrationBuilder.DropColumn(
                name: "HowToHire",
                table: "RefCServiceTitles");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "RefCommonJobTitles");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "RefCommonJobTitles");

            migrationBuilder.DropColumn(
                name: "PeriodInDays",
                table: "PlatformSubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "ISO6391",
                table: "Languages");
        }
    }
}
