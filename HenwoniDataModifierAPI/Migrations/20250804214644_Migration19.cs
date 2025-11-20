using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenwoniDataModifierAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "SkillsExperiences",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Responsibilities",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "RefCommonJobTitleSalaries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "RefCommonJobTitles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "LanguageId",
                table: "RefCommonJobTitleBenefit",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "RefCommonJobTitleBenefit",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "RefCommonJobTitleBenefit",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "RefCJTDescriptionTemplateTag",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "RefCJTDescriptionTemplates",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "RefCJTDescriptionTemplateAlias",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "LanguageId",
                table: "PlatformSubscriptionPlans",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "PlatformSubscriptionPlans",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "PlatformSubscriptionPlans",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "LanguageId",
                table: "JobLevels",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "JobLevels",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "JobLevels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "JobIndustries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "LanguageId",
                table: "JobContractTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "JobContractTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "JobContractTypes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Intros",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_RefCommonJobTitleBenefit_LanguageId",
                table: "RefCommonJobTitleBenefit",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCommonJobTitleBenefit_ParentId",
                table: "RefCommonJobTitleBenefit",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformSubscriptionPlans_LanguageId",
                table: "PlatformSubscriptionPlans",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformSubscriptionPlans_ParentId",
                table: "PlatformSubscriptionPlans",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLevels_LanguageId",
                table: "JobLevels",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLevels_ParentId",
                table: "JobLevels",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_JobContractTypes_LanguageId",
                table: "JobContractTypes",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_JobContractTypes_ParentId",
                table: "JobContractTypes",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobContractTypes_JobContractTypes_ParentId",
                table: "JobContractTypes",
                column: "ParentId",
                principalTable: "JobContractTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobContractTypes_Languages_LanguageId",
                table: "JobContractTypes",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobLevels_JobLevels_ParentId",
                table: "JobLevels",
                column: "ParentId",
                principalTable: "JobLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobLevels_Languages_LanguageId",
                table: "JobLevels",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformSubscriptionPlans_Languages_LanguageId",
                table: "PlatformSubscriptionPlans",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformSubscriptionPlans_PlatformSubscriptionPlans_ParentId",
                table: "PlatformSubscriptionPlans",
                column: "ParentId",
                principalTable: "PlatformSubscriptionPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCommonJobTitleBenefit_Languages_LanguageId",
                table: "RefCommonJobTitleBenefit",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCommonJobTitleBenefit_RefCommonJobTitleBenefit_ParentId",
                table: "RefCommonJobTitleBenefit",
                column: "ParentId",
                principalTable: "RefCommonJobTitleBenefit",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobContractTypes_JobContractTypes_ParentId",
                table: "JobContractTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_JobContractTypes_Languages_LanguageId",
                table: "JobContractTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_JobLevels_JobLevels_ParentId",
                table: "JobLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_JobLevels_Languages_LanguageId",
                table: "JobLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_PlatformSubscriptionPlans_Languages_LanguageId",
                table: "PlatformSubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_PlatformSubscriptionPlans_PlatformSubscriptionPlans_ParentId",
                table: "PlatformSubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCommonJobTitleBenefit_Languages_LanguageId",
                table: "RefCommonJobTitleBenefit");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCommonJobTitleBenefit_RefCommonJobTitleBenefit_ParentId",
                table: "RefCommonJobTitleBenefit");

            migrationBuilder.DropIndex(
                name: "IX_RefCommonJobTitleBenefit_LanguageId",
                table: "RefCommonJobTitleBenefit");

            migrationBuilder.DropIndex(
                name: "IX_RefCommonJobTitleBenefit_ParentId",
                table: "RefCommonJobTitleBenefit");

            migrationBuilder.DropIndex(
                name: "IX_PlatformSubscriptionPlans_LanguageId",
                table: "PlatformSubscriptionPlans");

            migrationBuilder.DropIndex(
                name: "IX_PlatformSubscriptionPlans_ParentId",
                table: "PlatformSubscriptionPlans");

            migrationBuilder.DropIndex(
                name: "IX_JobLevels_LanguageId",
                table: "JobLevels");

            migrationBuilder.DropIndex(
                name: "IX_JobLevels_ParentId",
                table: "JobLevels");

            migrationBuilder.DropIndex(
                name: "IX_JobContractTypes_LanguageId",
                table: "JobContractTypes");

            migrationBuilder.DropIndex(
                name: "IX_JobContractTypes_ParentId",
                table: "JobContractTypes");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "SkillsExperiences");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Responsibilities");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "RefCommonJobTitleSalaries");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "RefCommonJobTitles");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "RefCommonJobTitleBenefit");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "RefCommonJobTitleBenefit");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "RefCommonJobTitleBenefit");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "RefCJTDescriptionTemplateTag");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "RefCJTDescriptionTemplates");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "RefCJTDescriptionTemplateAlias");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "PlatformSubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "PlatformSubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "PlatformSubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "JobLevels");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "JobLevels");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "JobLevels");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "JobIndustries");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "JobContractTypes");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "JobContractTypes");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "JobContractTypes");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Intros");
        }
    }
}
