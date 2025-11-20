using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenwoniDataModifierAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateIntro_AspNetUsers_AuthorId",
                table: "RefCJTDTemplateIntro");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateIntro_Languages_LanguageId",
                table: "RefCJTDTemplateIntro");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateIntro_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                table: "RefCJTDTemplateIntro");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateResponsibility_AspNetUsers_AuthorId",
                table: "RefCJTDTemplateResponsibility");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateResponsibility_Languages_LanguageId",
                table: "RefCJTDTemplateResponsibility");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateResponsibility_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                table: "RefCJTDTemplateResponsibility");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateSkillExperience_AspNetUsers_AuthorId",
                table: "RefCJTDTemplateSkillExperience");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateSkillExperience_Languages_LanguageId",
                table: "RefCJTDTemplateSkillExperience");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateSkillExperience_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                table: "RefCJTDTemplateSkillExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefCJTDTemplateSkillExperience",
                table: "RefCJTDTemplateSkillExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefCJTDTemplateResponsibility",
                table: "RefCJTDTemplateResponsibility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefCJTDTemplateIntro",
                table: "RefCJTDTemplateIntro");

            migrationBuilder.RenameTable(
                name: "RefCJTDTemplateSkillExperience",
                newName: "SkillsExperiences");

            migrationBuilder.RenameTable(
                name: "RefCJTDTemplateResponsibility",
                newName: "Responsibilities");

            migrationBuilder.RenameTable(
                name: "RefCJTDTemplateIntro",
                newName: "Intros");

            migrationBuilder.RenameIndex(
                name: "IX_RefCJTDTemplateSkillExperience_RefCJTDescriptionTemplateId",
                table: "SkillsExperiences",
                newName: "IX_SkillsExperiences_RefCJTDescriptionTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_RefCJTDTemplateSkillExperience_LanguageId",
                table: "SkillsExperiences",
                newName: "IX_SkillsExperiences_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_RefCJTDTemplateSkillExperience_AuthorId",
                table: "SkillsExperiences",
                newName: "IX_SkillsExperiences_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_RefCJTDTemplateResponsibility_RefCJTDescriptionTemplateId",
                table: "Responsibilities",
                newName: "IX_Responsibilities_RefCJTDescriptionTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_RefCJTDTemplateResponsibility_LanguageId",
                table: "Responsibilities",
                newName: "IX_Responsibilities_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_RefCJTDTemplateResponsibility_AuthorId",
                table: "Responsibilities",
                newName: "IX_Responsibilities_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_RefCJTDTemplateIntro_RefCJTDescriptionTemplateId",
                table: "Intros",
                newName: "IX_Intros_RefCJTDescriptionTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_RefCJTDTemplateIntro_LanguageId",
                table: "Intros",
                newName: "IX_Intros_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_RefCJTDTemplateIntro_AuthorId",
                table: "Intros",
                newName: "IX_Intros_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillsExperiences",
                table: "SkillsExperiences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Responsibilities",
                table: "Responsibilities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Intros",
                table: "Intros",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Transilations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    DefaultLanguageText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemContextIdentity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinSystemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transilations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transilations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transilations_LanguageId",
                table: "Transilations",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Intros_AspNetUsers_AuthorId",
                table: "Intros",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Intros_Languages_LanguageId",
                table: "Intros",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Intros_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                table: "Intros",
                column: "RefCJTDescriptionTemplateId",
                principalTable: "RefCJTDescriptionTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Responsibilities_AspNetUsers_AuthorId",
                table: "Responsibilities",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Responsibilities_Languages_LanguageId",
                table: "Responsibilities",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Responsibilities_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                table: "Responsibilities",
                column: "RefCJTDescriptionTemplateId",
                principalTable: "RefCJTDescriptionTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillsExperiences_AspNetUsers_AuthorId",
                table: "SkillsExperiences",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillsExperiences_Languages_LanguageId",
                table: "SkillsExperiences",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillsExperiences_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                table: "SkillsExperiences",
                column: "RefCJTDescriptionTemplateId",
                principalTable: "RefCJTDescriptionTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intros_AspNetUsers_AuthorId",
                table: "Intros");

            migrationBuilder.DropForeignKey(
                name: "FK_Intros_Languages_LanguageId",
                table: "Intros");

            migrationBuilder.DropForeignKey(
                name: "FK_Intros_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                table: "Intros");

            migrationBuilder.DropForeignKey(
                name: "FK_Responsibilities_AspNetUsers_AuthorId",
                table: "Responsibilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Responsibilities_Languages_LanguageId",
                table: "Responsibilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Responsibilities_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                table: "Responsibilities");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillsExperiences_AspNetUsers_AuthorId",
                table: "SkillsExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillsExperiences_Languages_LanguageId",
                table: "SkillsExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillsExperiences_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                table: "SkillsExperiences");

            migrationBuilder.DropTable(
                name: "Transilations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillsExperiences",
                table: "SkillsExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Responsibilities",
                table: "Responsibilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Intros",
                table: "Intros");

            migrationBuilder.RenameTable(
                name: "SkillsExperiences",
                newName: "RefCJTDTemplateSkillExperience");

            migrationBuilder.RenameTable(
                name: "Responsibilities",
                newName: "RefCJTDTemplateResponsibility");

            migrationBuilder.RenameTable(
                name: "Intros",
                newName: "RefCJTDTemplateIntro");

            migrationBuilder.RenameIndex(
                name: "IX_SkillsExperiences_RefCJTDescriptionTemplateId",
                table: "RefCJTDTemplateSkillExperience",
                newName: "IX_RefCJTDTemplateSkillExperience_RefCJTDescriptionTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_SkillsExperiences_LanguageId",
                table: "RefCJTDTemplateSkillExperience",
                newName: "IX_RefCJTDTemplateSkillExperience_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_SkillsExperiences_AuthorId",
                table: "RefCJTDTemplateSkillExperience",
                newName: "IX_RefCJTDTemplateSkillExperience_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Responsibilities_RefCJTDescriptionTemplateId",
                table: "RefCJTDTemplateResponsibility",
                newName: "IX_RefCJTDTemplateResponsibility_RefCJTDescriptionTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_Responsibilities_LanguageId",
                table: "RefCJTDTemplateResponsibility",
                newName: "IX_RefCJTDTemplateResponsibility_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_Responsibilities_AuthorId",
                table: "RefCJTDTemplateResponsibility",
                newName: "IX_RefCJTDTemplateResponsibility_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Intros_RefCJTDescriptionTemplateId",
                table: "RefCJTDTemplateIntro",
                newName: "IX_RefCJTDTemplateIntro_RefCJTDescriptionTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_Intros_LanguageId",
                table: "RefCJTDTemplateIntro",
                newName: "IX_RefCJTDTemplateIntro_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_Intros_AuthorId",
                table: "RefCJTDTemplateIntro",
                newName: "IX_RefCJTDTemplateIntro_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefCJTDTemplateSkillExperience",
                table: "RefCJTDTemplateSkillExperience",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefCJTDTemplateResponsibility",
                table: "RefCJTDTemplateResponsibility",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefCJTDTemplateIntro",
                table: "RefCJTDTemplateIntro",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateIntro_AspNetUsers_AuthorId",
                table: "RefCJTDTemplateIntro",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateIntro_Languages_LanguageId",
                table: "RefCJTDTemplateIntro",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateIntro_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                table: "RefCJTDTemplateIntro",
                column: "RefCJTDescriptionTemplateId",
                principalTable: "RefCJTDescriptionTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateResponsibility_AspNetUsers_AuthorId",
                table: "RefCJTDTemplateResponsibility",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateResponsibility_Languages_LanguageId",
                table: "RefCJTDTemplateResponsibility",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateResponsibility_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                table: "RefCJTDTemplateResponsibility",
                column: "RefCJTDescriptionTemplateId",
                principalTable: "RefCJTDescriptionTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateSkillExperience_AspNetUsers_AuthorId",
                table: "RefCJTDTemplateSkillExperience",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateSkillExperience_Languages_LanguageId",
                table: "RefCJTDTemplateSkillExperience",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateSkillExperience_RefCJTDescriptionTemplates_RefCJTDescriptionTemplateId",
                table: "RefCJTDTemplateSkillExperience",
                column: "RefCJTDescriptionTemplateId",
                principalTable: "RefCJTDescriptionTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
