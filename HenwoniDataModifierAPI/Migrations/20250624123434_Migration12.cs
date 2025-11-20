using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenwoniDataModifierAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodInDays",
                table: "PlatformSubscriptionPlans");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "RefCommonJobTitleSalaries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "RefCommonJobTitleSalaries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "RefCommonJobTitles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "RefCommonJobTitles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "RefCommonJobTitleBenefit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "RefCommonJobTitleBenefit",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "RefCJTDTemplateSkillExperience",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "RefCJTDTemplateSkillExperience",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "RefCJTDTemplateResponsibility",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "RefCJTDTemplateResponsibility",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "RefCJTDTemplateIntro",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "RefCJTDTemplateIntro",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "RefCJTDescriptionTemplateTag",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "RefCJTDescriptionTemplateTag",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "RefCJTDescriptionTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "RefCJTDescriptionTemplates",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "RefCJTDescriptionTemplateAlias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "RefCJTDescriptionTemplateAlias",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "JobIndustries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "JobIndustries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LanguageId",
                table: "JobIndustries",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefCommonJobTitleSalaries_AuthorId",
                table: "RefCommonJobTitleSalaries",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCommonJobTitles_AuthorId",
                table: "RefCommonJobTitles",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCommonJobTitleBenefit_AuthorId",
                table: "RefCommonJobTitleBenefit",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDTemplateSkillExperience_AuthorId",
                table: "RefCJTDTemplateSkillExperience",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDTemplateResponsibility_AuthorId",
                table: "RefCJTDTemplateResponsibility",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDTemplateIntro_AuthorId",
                table: "RefCJTDTemplateIntro",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDescriptionTemplateTag_AuthorId",
                table: "RefCJTDescriptionTemplateTag",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDescriptionTemplates_AuthorId",
                table: "RefCJTDescriptionTemplates",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDescriptionTemplateAlias_AuthorId",
                table: "RefCJTDescriptionTemplateAlias",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_JobIndustries_AuthorId",
                table: "JobIndustries",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_JobIndustries_LanguageId",
                table: "JobIndustries",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobIndustries_ApplicationUser_AuthorId",
                table: "JobIndustries",
                column: "AuthorId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobIndustries_Languages_LanguageId",
                table: "JobIndustries",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDescriptionTemplateAlias_ApplicationUser_AuthorId",
                table: "RefCJTDescriptionTemplateAlias",
                column: "AuthorId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDescriptionTemplates_ApplicationUser_AuthorId",
                table: "RefCJTDescriptionTemplates",
                column: "AuthorId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDescriptionTemplateTag_ApplicationUser_AuthorId",
                table: "RefCJTDescriptionTemplateTag",
                column: "AuthorId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateIntro_ApplicationUser_AuthorId",
                table: "RefCJTDTemplateIntro",
                column: "AuthorId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateResponsibility_ApplicationUser_AuthorId",
                table: "RefCJTDTemplateResponsibility",
                column: "AuthorId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateSkillExperience_ApplicationUser_AuthorId",
                table: "RefCJTDTemplateSkillExperience",
                column: "AuthorId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCommonJobTitleBenefit_AspNetUsers_AuthorId",
                table: "RefCommonJobTitleBenefit",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCommonJobTitles_ApplicationUser_AuthorId",
                table: "RefCommonJobTitles",
                column: "AuthorId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCommonJobTitleSalaries_ApplicationUser_AuthorId",
                table: "RefCommonJobTitleSalaries",
                column: "AuthorId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobIndustries_ApplicationUser_AuthorId",
                table: "JobIndustries");

            migrationBuilder.DropForeignKey(
                name: "FK_JobIndustries_Languages_LanguageId",
                table: "JobIndustries");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDescriptionTemplateAlias_ApplicationUser_AuthorId",
                table: "RefCJTDescriptionTemplateAlias");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDescriptionTemplates_ApplicationUser_AuthorId",
                table: "RefCJTDescriptionTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDescriptionTemplateTag_ApplicationUser_AuthorId",
                table: "RefCJTDescriptionTemplateTag");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateIntro_ApplicationUser_AuthorId",
                table: "RefCJTDTemplateIntro");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateResponsibility_ApplicationUser_AuthorId",
                table: "RefCJTDTemplateResponsibility");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateSkillExperience_ApplicationUser_AuthorId",
                table: "RefCJTDTemplateSkillExperience");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCommonJobTitleBenefit_AspNetUsers_AuthorId",
                table: "RefCommonJobTitleBenefit");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCommonJobTitles_ApplicationUser_AuthorId",
                table: "RefCommonJobTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCommonJobTitleSalaries_ApplicationUser_AuthorId",
                table: "RefCommonJobTitleSalaries");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_RefCommonJobTitleSalaries_AuthorId",
                table: "RefCommonJobTitleSalaries");

            migrationBuilder.DropIndex(
                name: "IX_RefCommonJobTitles_AuthorId",
                table: "RefCommonJobTitles");

            migrationBuilder.DropIndex(
                name: "IX_RefCommonJobTitleBenefit_AuthorId",
                table: "RefCommonJobTitleBenefit");

            migrationBuilder.DropIndex(
                name: "IX_RefCJTDTemplateSkillExperience_AuthorId",
                table: "RefCJTDTemplateSkillExperience");

            migrationBuilder.DropIndex(
                name: "IX_RefCJTDTemplateResponsibility_AuthorId",
                table: "RefCJTDTemplateResponsibility");

            migrationBuilder.DropIndex(
                name: "IX_RefCJTDTemplateIntro_AuthorId",
                table: "RefCJTDTemplateIntro");

            migrationBuilder.DropIndex(
                name: "IX_RefCJTDescriptionTemplateTag_AuthorId",
                table: "RefCJTDescriptionTemplateTag");

            migrationBuilder.DropIndex(
                name: "IX_RefCJTDescriptionTemplates_AuthorId",
                table: "RefCJTDescriptionTemplates");

            migrationBuilder.DropIndex(
                name: "IX_RefCJTDescriptionTemplateAlias_AuthorId",
                table: "RefCJTDescriptionTemplateAlias");

            migrationBuilder.DropIndex(
                name: "IX_JobIndustries_AuthorId",
                table: "JobIndustries");

            migrationBuilder.DropIndex(
                name: "IX_JobIndustries_LanguageId",
                table: "JobIndustries");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "RefCommonJobTitleSalaries");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "RefCommonJobTitleSalaries");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "RefCommonJobTitles");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "RefCommonJobTitles");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "RefCommonJobTitleBenefit");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "RefCommonJobTitleBenefit");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "RefCJTDTemplateSkillExperience");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "RefCJTDTemplateSkillExperience");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "RefCJTDTemplateResponsibility");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "RefCJTDTemplateResponsibility");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "RefCJTDTemplateIntro");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "RefCJTDTemplateIntro");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "RefCJTDescriptionTemplateTag");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "RefCJTDescriptionTemplateTag");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "RefCJTDescriptionTemplates");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "RefCJTDescriptionTemplates");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "RefCJTDescriptionTemplateAlias");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "RefCJTDescriptionTemplateAlias");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "JobIndustries");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "JobIndustries");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "JobIndustries");

            migrationBuilder.AddColumn<long>(
                name: "PeriodInDays",
                table: "PlatformSubscriptionPlans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
