using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenwoniDataModifierAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobIndustries_ApplicationUser_AuthorId",
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
                name: "FK_RefCommonJobTitles_ApplicationUser_AuthorId",
                table: "RefCommonJobTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCommonJobTitleSalaries_ApplicationUser_AuthorId",
                table: "RefCommonJobTitleSalaries");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "RefCJTDescriptionTemplates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_JobIndustries_AspNetUsers_AuthorId",
                table: "JobIndustries",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDescriptionTemplateAlias_AspNetUsers_AuthorId",
                table: "RefCJTDescriptionTemplateAlias",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDescriptionTemplates_AspNetUsers_AuthorId",
                table: "RefCJTDescriptionTemplates",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDescriptionTemplateTag_AspNetUsers_AuthorId",
                table: "RefCJTDescriptionTemplateTag",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateIntro_AspNetUsers_AuthorId",
                table: "RefCJTDTemplateIntro",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateResponsibility_AspNetUsers_AuthorId",
                table: "RefCJTDTemplateResponsibility",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCJTDTemplateSkillExperience_AspNetUsers_AuthorId",
                table: "RefCJTDTemplateSkillExperience",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCommonJobTitles_AspNetUsers_AuthorId",
                table: "RefCommonJobTitles",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefCommonJobTitleSalaries_AspNetUsers_AuthorId",
                table: "RefCommonJobTitleSalaries",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobIndustries_AspNetUsers_AuthorId",
                table: "JobIndustries");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDescriptionTemplateAlias_AspNetUsers_AuthorId",
                table: "RefCJTDescriptionTemplateAlias");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDescriptionTemplates_AspNetUsers_AuthorId",
                table: "RefCJTDescriptionTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDescriptionTemplateTag_AspNetUsers_AuthorId",
                table: "RefCJTDescriptionTemplateTag");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateIntro_AspNetUsers_AuthorId",
                table: "RefCJTDTemplateIntro");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateResponsibility_AspNetUsers_AuthorId",
                table: "RefCJTDTemplateResponsibility");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCJTDTemplateSkillExperience_AspNetUsers_AuthorId",
                table: "RefCJTDTemplateSkillExperience");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCommonJobTitles_AspNetUsers_AuthorId",
                table: "RefCommonJobTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_RefCommonJobTitleSalaries_AspNetUsers_AuthorId",
                table: "RefCommonJobTitleSalaries");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "RefCJTDescriptionTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_JobIndustries_ApplicationUser_AuthorId",
                table: "JobIndustries",
                column: "AuthorId",
                principalTable: "ApplicationUser",
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
    }
}
