using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenwoniDataModifierAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RefCServiceTitleId",
                table: "RefCJTDescriptionTemplates",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RefCServiceTitleId",
                table: "JobIndustries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RefCServiceTitleId",
                table: "CandidateSoftSkills",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RefCServiceTitleId",
                table: "CandidateSkills",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefCServiceTitles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PluralTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverageSalary = table.Column<double>(type: "float", nullable: true),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCServiceTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefCServiceTitles_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefCServiceTitleTemplateTags",
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
                    table.PrimaryKey("PK_RefCServiceTitleTemplateTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefCServiceTitleTemplateTags_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefCServiceTitleTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Template = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    RefCServiceTitleId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCServiceTitleTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefCServiceTitleTemplates_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefCServiceTitleTemplates_RefCServiceTitles_RefCServiceTitleId",
                        column: x => x.RefCServiceTitleId,
                        principalTable: "RefCServiceTitles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefCServiceTitleTemplateAliases",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    LanguageId = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefCServiceTitleTemplateId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCServiceTitleTemplateAliases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefCServiceTitleTemplateAliases_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RefCServiceTitleTemplateAliases_RefCServiceTitleTemplates_RefCServiceTitleTemplateId",
                        column: x => x.RefCServiceTitleTemplateId,
                        principalTable: "RefCServiceTitleTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefCServiceTitleTemplateRefCServiceTitleTemplateTag",
                columns: table => new
                {
                    RefCServiceTitleTemplatesId = table.Column<long>(type: "bigint", nullable: false),
                    TagsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCServiceTitleTemplateRefCServiceTitleTemplateTag", x => new { x.RefCServiceTitleTemplatesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_RefCServiceTitleTemplateRefCServiceTitleTemplateTag_RefCServiceTitleTemplateTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "RefCServiceTitleTemplateTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefCServiceTitleTemplateRefCServiceTitleTemplateTag_RefCServiceTitleTemplates_RefCServiceTitleTemplatesId",
                        column: x => x.RefCServiceTitleTemplatesId,
                        principalTable: "RefCServiceTitleTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDescriptionTemplates_RefCServiceTitleId",
                table: "RefCJTDescriptionTemplates",
                column: "RefCServiceTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_JobIndustries_RefCServiceTitleId",
                table: "JobIndustries",
                column: "RefCServiceTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSoftSkills_RefCServiceTitleId",
                table: "CandidateSoftSkills",
                column: "RefCServiceTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkills_RefCServiceTitleId",
                table: "CandidateSkills",
                column: "RefCServiceTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCServiceTitles_LanguageId",
                table: "RefCServiceTitles",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCServiceTitleTemplateAliases_LanguageId",
                table: "RefCServiceTitleTemplateAliases",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCServiceTitleTemplateAliases_RefCServiceTitleTemplateId",
                table: "RefCServiceTitleTemplateAliases",
                column: "RefCServiceTitleTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCServiceTitleTemplateRefCServiceTitleTemplateTag_TagsId",
                table: "RefCServiceTitleTemplateRefCServiceTitleTemplateTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCServiceTitleTemplates_LanguageId",
                table: "RefCServiceTitleTemplates",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCServiceTitleTemplates_RefCServiceTitleId",
                table: "RefCServiceTitleTemplates",
                column: "RefCServiceTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCServiceTitleTemplateTags_LanguageId",
                table: "RefCServiceTitleTemplateTags",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateSkills_RefCServiceTitles_RefCServiceTitleId",
                table: "CandidateSkills",
                column: "RefCServiceTitleId",
                principalTable: "RefCServiceTitles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateSoftSkills_RefCServiceTitles_RefCServiceTitleId",
                table: "CandidateSoftSkills",
                column: "RefCServiceTitleId",
                principalTable: "RefCServiceTitles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobIndustries_RefCServiceTitles_RefCServiceTitleId",
                table: "JobIndustries",
                column: "RefCServiceTitleId",
                principalTable: "RefCServiceTitles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDescriptionTemplates_RefCServiceTitles_RefCServiceTitleId",
                table: "RefCJTDescriptionTemplates",
                column: "RefCServiceTitleId",
                principalTable: "RefCServiceTitles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateSkills_RefCServiceTitles_RefCServiceTitleId",
                table: "CandidateSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateSoftSkills_RefCServiceTitles_RefCServiceTitleId",
                table: "CandidateSoftSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_JobIndustries_RefCServiceTitles_RefCServiceTitleId",
                table: "JobIndustries");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDescriptionTemplates_RefCServiceTitles_RefCServiceTitleId",
                table: "RefCJTDescriptionTemplates");

            migrationBuilder.DropTable(
                name: "RefCServiceTitleTemplateAliases");

            migrationBuilder.DropTable(
                name: "RefCServiceTitleTemplateRefCServiceTitleTemplateTag");

            migrationBuilder.DropTable(
                name: "RefCServiceTitleTemplateTags");

            migrationBuilder.DropTable(
                name: "RefCServiceTitleTemplates");

            migrationBuilder.DropTable(
                name: "RefCServiceTitles");

            migrationBuilder.DropIndex(
                name: "IX_RefCJTDescriptionTemplates_RefCServiceTitleId",
                table: "RefCJTDescriptionTemplates");

            migrationBuilder.DropIndex(
                name: "IX_JobIndustries_RefCServiceTitleId",
                table: "JobIndustries");

            migrationBuilder.DropIndex(
                name: "IX_CandidateSoftSkills_RefCServiceTitleId",
                table: "CandidateSoftSkills");

            migrationBuilder.DropIndex(
                name: "IX_CandidateSkills_RefCServiceTitleId",
                table: "CandidateSkills");

            migrationBuilder.DropColumn(
                name: "RefCServiceTitleId",
                table: "RefCJTDescriptionTemplates");

            migrationBuilder.DropColumn(
                name: "RefCServiceTitleId",
                table: "JobIndustries");

            migrationBuilder.DropColumn(
                name: "RefCServiceTitleId",
                table: "CandidateSoftSkills");

            migrationBuilder.DropColumn(
                name: "RefCServiceTitleId",
                table: "CandidateSkills");
        }
    }
}
