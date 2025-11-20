using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenwoniDataModifierAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobContracts_JobContractTypes_JobContractTypeId",
                table: "JobContracts");

            migrationBuilder.DropTable(
                name: "JobContractTypes");

            migrationBuilder.CreateTable(
                name: "JobLayoutTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobLayoutTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobLayoutTypes_JobLayoutTypes_ParentId",
                        column: x => x.ParentId,
                        principalTable: "JobLayoutTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobLayoutTypes_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobLayoutTypes_LanguageId",
                table: "JobLayoutTypes",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLayoutTypes_ParentId",
                table: "JobLayoutTypes",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLayoutTypes_SystemName",
                table: "JobLayoutTypes",
                column: "SystemName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobContracts_JobLayoutTypes_JobContractTypeId",
                table: "JobContracts",
                column: "JobContractTypeId",
                principalTable: "JobLayoutTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobContracts_JobLayoutTypes_JobContractTypeId",
                table: "JobContracts");

            migrationBuilder.DropTable(
                name: "JobLayoutTypes");

            migrationBuilder.CreateTable(
                name: "JobContractTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<long>(type: "bigint", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobContractTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobContractTypes_JobContractTypes_ParentId",
                        column: x => x.ParentId,
                        principalTable: "JobContractTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobContractTypes_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobContractTypes_LanguageId",
                table: "JobContractTypes",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_JobContractTypes_ParentId",
                table: "JobContractTypes",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_JobContractTypes_SystemName",
                table: "JobContractTypes",
                column: "SystemName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobContracts_JobContractTypes_JobContractTypeId",
                table: "JobContracts",
                column: "JobContractTypeId",
                principalTable: "JobContractTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
