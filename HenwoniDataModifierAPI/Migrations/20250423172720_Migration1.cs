using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenwoniDataModifierAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Permalink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateProfileStyles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateProfileStyles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommonServiceReferences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ServiceCategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonServiceReferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Continents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobsCount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryTimeZones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZoneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GmtOffset = table.Column<long>(type: "bigint", nullable: false),
                    GmtOffsetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TzName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTimeZones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencySymbol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntertainmentCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntertainmentCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntertainmentGenres",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntertainmentGenres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobContractAuditingRequirements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobContractAuditingRequirements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobContractTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobContractTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobLevels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocaleTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LiteratureGenres",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteratureGenres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MusicGenres",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicGenres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlatformAppVersions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<double>(type: "float", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VersionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformAppVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlatformSubscriptionPlanFeatures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlatformSubscriptionPlanId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformSubscriptionPlanFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlatformSubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformSubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioStyles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioStyles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTemplateEntertainmentTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTemplateEntertainmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupportTicketDepartments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTicketDepartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContinentRegions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContinentId = table.Column<long>(type: "bigint", nullable: true),
                    JobsCount = table.Column<long>(type: "bigint", nullable: false),
                    ContinentId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContinentRegions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContinentRegions_Continents_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContinentRegions_Continents_ContinentId1",
                        column: x => x.ContinentId1,
                        principalTable: "Continents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobContracts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractCompleted = table.Column<bool>(type: "bit", nullable: false),
                    JobContractTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobContracts_JobContractTypes_JobContractTypeId",
                        column: x => x.JobContractTypeId,
                        principalTable: "JobContractTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefCommonJobTitles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobDescriptionTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PluralTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverageSalary = table.Column<double>(type: "float", nullable: true),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobLevelId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCommonJobTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefCommonJobTitles_JobLevels_JobLevelId",
                        column: x => x.JobLevelId,
                        principalTable: "JobLevels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlatformSubscriptionPlanPlatformSubscriptionPlanFeature",
                columns: table => new
                {
                    FeaturesId = table.Column<long>(type: "bigint", nullable: false),
                    PlansId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformSubscriptionPlanPlatformSubscriptionPlanFeature", x => new { x.FeaturesId, x.PlansId });
                    table.ForeignKey(
                        name: "FK_PlatformSubscriptionPlanPlatformSubscriptionPlanFeature_PlatformSubscriptionPlanFeatures_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "PlatformSubscriptionPlanFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatformSubscriptionPlanPlatformSubscriptionPlanFeature_PlatformSubscriptionPlans_PlansId",
                        column: x => x.PlansId,
                        principalTable: "PlatformSubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlatformSubscriptionPlanPrices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    PlanId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformSubscriptionPlanPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlatformSubscriptionPlanPrices_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatformSubscriptionPlanPrices_PlatformSubscriptionPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "PlatformSubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTemplateEntertainmentForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    EntertainmentTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTemplateEntertainmentForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceTemplateEntertainmentForms_ServiceTemplateEntertainmentTypes_EntertainmentTypeId",
                        column: x => x.EntertainmentTypeId,
                        principalTable: "ServiceTemplateEntertainmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISO3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISO2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumericCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capital = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultCurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    TLD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Native = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContinentId = table.Column<long>(type: "bigint", nullable: true),
                    ContinentRegionId = table.Column<long>(type: "bigint", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Emoji = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmojiU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopologyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobsCount = table.Column<long>(type: "bigint", nullable: false),
                    ContinentId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_ContinentRegions_ContinentRegionId",
                        column: x => x.ContinentRegionId,
                        principalTable: "ContinentRegions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Countries_Continents_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Countries_Continents_ContinentId1",
                        column: x => x.ContinentId1,
                        principalTable: "Continents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Countries_Currencies_DefaultCurrencyId",
                        column: x => x.DefaultCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobIndustries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobsCount = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RefCommonJobTitleId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobIndustries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobIndustries_JobIndustries_ParentId",
                        column: x => x.ParentId,
                        principalTable: "JobIndustries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobIndustries_RefCommonJobTitles_RefCommonJobTitleId",
                        column: x => x.RefCommonJobTitleId,
                        principalTable: "RefCommonJobTitles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefCJTDescriptionTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefCommonJobTitleId = table.Column<long>(type: "bigint", nullable: false),
                    JobLevelId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCJTDescriptionTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefCJTDescriptionTemplates_JobLevels_JobLevelId",
                        column: x => x.JobLevelId,
                        principalTable: "JobLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RefCJTDescriptionTemplates_RefCommonJobTitles_RefCommonJobTitleId",
                        column: x => x.RefCommonJobTitleId,
                        principalTable: "RefCommonJobTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefCommonJobTitleBenefit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefCommonJobTitleId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCommonJobTitleBenefit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefCommonJobTitleBenefit_RefCommonJobTitles_RefCommonJobTitleId",
                        column: x => x.RefCommonJobTitleId,
                        principalTable: "RefCommonJobTitles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CountryCountryTimeZone",
                columns: table => new
                {
                    CountriesId = table.Column<long>(type: "bigint", nullable: false),
                    TimeZonesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCountryTimeZone", x => new { x.CountriesId, x.TimeZonesId });
                    table.ForeignKey(
                        name: "FK_CountryCountryTimeZone_Countries_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryCountryTimeZone_CountryTimeZones_TimeZonesId",
                        column: x => x.TimeZonesId,
                        principalTable: "CountryTimeZones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PtBr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    De = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Es = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    It = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryTranslations_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContinentId = table.Column<long>(type: "bigint", nullable: true),
                    ContinentRegionId = table.Column<long>(type: "bigint", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    CountryId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_ContinentRegions_ContinentRegionId",
                        column: x => x.ContinentRegionId,
                        principalTable: "ContinentRegions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_States_Continents_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId1",
                        column: x => x.CountryId1,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkillIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    TotalSkillCredits = table.Column<int>(type: "int", nullable: false),
                    PrimaryJobIndustryId = table.Column<long>(type: "bigint", nullable: true),
                    RefCommonJobTitleId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateSkills_JobIndustries_PrimaryJobIndustryId",
                        column: x => x.PrimaryJobIndustryId,
                        principalTable: "JobIndustries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateSkills_RefCommonJobTitles_RefCommonJobTitleId",
                        column: x => x.RefCommonJobTitleId,
                        principalTable: "RefCommonJobTitles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateSoftSkills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobIndustryId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSoftSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateSoftSkills_JobIndustries_JobIndustryId",
                        column: x => x.JobIndustryId,
                        principalTable: "JobIndustries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    JobIndustryRefId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCategories_JobIndustries_JobIndustryRefId",
                        column: x => x.JobIndustryRefId,
                        principalTable: "JobIndustries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceCategories_ServiceCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ServiceCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContinentId = table.Column<long>(type: "bigint", nullable: true),
                    ContinentRegionId = table.Column<long>(type: "bigint", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    StateId = table.Column<long>(type: "bigint", nullable: true),
                    JobsCount = table.Column<long>(type: "bigint", nullable: false),
                    StateId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_ContinentRegions_ContinentRegionId",
                        column: x => x.ContinentRegionId,
                        principalTable: "ContinentRegions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cities_Continents_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId1",
                        column: x => x.StateId1,
                        principalTable: "States",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateSkillJobIndustry",
                columns: table => new
                {
                    JobIndustriesId = table.Column<long>(type: "bigint", nullable: false),
                    SkillsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSkillJobIndustry", x => new { x.JobIndustriesId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_CandidateSkillJobIndustry_CandidateSkills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "CandidateSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateSkillJobIndustry_JobIndustries_JobIndustriesId",
                        column: x => x.JobIndustriesId,
                        principalTable: "JobIndustries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidateSkillId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillCategories_CandidateSkills_CandidateSkillId",
                        column: x => x.CandidateSkillId,
                        principalTable: "CandidateSkills",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CommonServiceReferenceServiceCategory",
                columns: table => new
                {
                    CommonServiceReferencesId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceCategoriesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonServiceReferenceServiceCategory", x => new { x.CommonServiceReferencesId, x.ServiceCategoriesId });
                    table.ForeignKey(
                        name: "FK_CommonServiceReferenceServiceCategory_CommonServiceReferences_CommonServiceReferencesId",
                        column: x => x.CommonServiceReferencesId,
                        principalTable: "CommonServiceReferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommonServiceReferenceServiceCategory_ServiceCategories_ServiceCategoriesId",
                        column: x => x.ServiceCategoriesId,
                        principalTable: "ServiceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefCommonJobTitleSalaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationDomain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    StateId = table.Column<long>(type: "bigint", nullable: true),
                    CityId = table.Column<long>(type: "bigint", nullable: true),
                    JobTitleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCommonJobTitleSalaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefCommonJobTitleSalaries_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RefCommonJobTitleSalaries_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefCommonJobTitleSalaries_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RefCommonJobTitleSalaries_RefCommonJobTitles_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "RefCommonJobTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefCommonJobTitleSalaries_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContinentId = table.Column<long>(type: "bigint", nullable: true),
                    ContinentRegionId = table.Column<long>(type: "bigint", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    StateId = table.Column<long>(type: "bigint", nullable: true),
                    CityId = table.Column<long>(type: "bigint", nullable: true),
                    JobsCount = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Towns_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Towns_ContinentRegions_ContinentRegionId",
                        column: x => x.ContinentRegionId,
                        principalTable: "ContinentRegions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Towns_Continents_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Towns_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Towns_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkillJobIndustry_SkillsId",
                table: "CandidateSkillJobIndustry",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkills_PrimaryJobIndustryId",
                table: "CandidateSkills",
                column: "PrimaryJobIndustryId",
                unique: true,
                filter: "[PrimaryJobIndustryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkills_RefCommonJobTitleId",
                table: "CandidateSkills",
                column: "RefCommonJobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSoftSkills_JobIndustryId",
                table: "CandidateSoftSkills",
                column: "JobIndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ContinentId",
                table: "Cities",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ContinentRegionId",
                table: "Cities",
                column: "ContinentRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId1",
                table: "Cities",
                column: "StateId1");

            migrationBuilder.CreateIndex(
                name: "IX_CommonServiceReferenceServiceCategory_ServiceCategoriesId",
                table: "CommonServiceReferenceServiceCategory",
                column: "ServiceCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ContinentRegions_ContinentId",
                table: "ContinentRegions",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContinentRegions_ContinentId1",
                table: "ContinentRegions",
                column: "ContinentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ContinentId",
                table: "Countries",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ContinentId1",
                table: "Countries",
                column: "ContinentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ContinentRegionId",
                table: "Countries",
                column: "ContinentRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_DefaultCurrencyId",
                table: "Countries",
                column: "DefaultCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCountryTimeZone_TimeZonesId",
                table: "CountryCountryTimeZone",
                column: "TimeZonesId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryTranslations_CountryId",
                table: "CountryTranslations",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_EntertainmentCategories_SystemName",
                table: "EntertainmentCategories",
                column: "SystemName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntertainmentGenres_SystemName",
                table: "EntertainmentGenres",
                column: "SystemName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobContracts_JobContractTypeId",
                table: "JobContracts",
                column: "JobContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobContractTypes_SystemName",
                table: "JobContractTypes",
                column: "SystemName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobIndustries_ParentId",
                table: "JobIndustries",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_JobIndustries_RefCommonJobTitleId",
                table: "JobIndustries",
                column: "RefCommonJobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteratureGenres_SystemName",
                table: "LiteratureGenres",
                column: "SystemName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MusicGenres_SystemName",
                table: "MusicGenres",
                column: "SystemName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlatformSubscriptionPlanPlatformSubscriptionPlanFeature_PlansId",
                table: "PlatformSubscriptionPlanPlatformSubscriptionPlanFeature",
                column: "PlansId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformSubscriptionPlanPrices_CurrencyId",
                table: "PlatformSubscriptionPlanPrices",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformSubscriptionPlanPrices_PlanId",
                table: "PlatformSubscriptionPlanPrices",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDescriptionTemplates_JobLevelId",
                table: "RefCJTDescriptionTemplates",
                column: "JobLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCJTDescriptionTemplates_RefCommonJobTitleId",
                table: "RefCJTDescriptionTemplates",
                column: "RefCommonJobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCommonJobTitleBenefit_RefCommonJobTitleId",
                table: "RefCommonJobTitleBenefit",
                column: "RefCommonJobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCommonJobTitles_JobLevelId",
                table: "RefCommonJobTitles",
                column: "JobLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCommonJobTitleSalaries_CityId",
                table: "RefCommonJobTitleSalaries",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCommonJobTitleSalaries_CountryId",
                table: "RefCommonJobTitleSalaries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCommonJobTitleSalaries_CurrencyId",
                table: "RefCommonJobTitleSalaries",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCommonJobTitleSalaries_JobTitleId",
                table: "RefCommonJobTitleSalaries",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_RefCommonJobTitleSalaries_StateId",
                table: "RefCommonJobTitleSalaries",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategories_JobIndustryRefId",
                table: "ServiceCategories",
                column: "JobIndustryRefId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategories_ParentId",
                table: "ServiceCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTemplateEntertainmentForms_EntertainmentTypeId",
                table: "ServiceTemplateEntertainmentForms",
                column: "EntertainmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTemplateEntertainmentForms_SystemName",
                table: "ServiceTemplateEntertainmentForms",
                column: "SystemName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTemplateEntertainmentTypes_SystemName",
                table: "ServiceTemplateEntertainmentTypes",
                column: "SystemName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillCategories_CandidateSkillId",
                table: "SkillCategories",
                column: "CandidateSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_States_ContinentId",
                table: "States",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_States_ContinentRegionId",
                table: "States",
                column: "ContinentRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId1",
                table: "States",
                column: "CountryId1");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTicketDepartments_SystemName",
                table: "SupportTicketDepartments",
                column: "SystemName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Towns_CityId",
                table: "Towns",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_ContinentId",
                table: "Towns",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_ContinentRegionId",
                table: "Towns",
                column: "ContinentRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_CountryId",
                table: "Towns",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_StateId",
                table: "Towns",
                column: "StateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CandidateProfileStyles");

            migrationBuilder.DropTable(
                name: "CandidateRoles");

            migrationBuilder.DropTable(
                name: "CandidateSkillJobIndustry");

            migrationBuilder.DropTable(
                name: "CandidateSoftSkills");

            migrationBuilder.DropTable(
                name: "CommonServiceReferenceServiceCategory");

            migrationBuilder.DropTable(
                name: "CountryCountryTimeZone");

            migrationBuilder.DropTable(
                name: "CountryTranslations");

            migrationBuilder.DropTable(
                name: "EntertainmentCategories");

            migrationBuilder.DropTable(
                name: "EntertainmentGenres");

            migrationBuilder.DropTable(
                name: "JobContractAuditingRequirements");

            migrationBuilder.DropTable(
                name: "JobContracts");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "LiteratureGenres");

            migrationBuilder.DropTable(
                name: "MusicGenres");

            migrationBuilder.DropTable(
                name: "OrganisationTypes");

            migrationBuilder.DropTable(
                name: "PlatformAppVersions");

            migrationBuilder.DropTable(
                name: "PlatformSubscriptionPlanPlatformSubscriptionPlanFeature");

            migrationBuilder.DropTable(
                name: "PlatformSubscriptionPlanPrices");

            migrationBuilder.DropTable(
                name: "PortfolioStyles");

            migrationBuilder.DropTable(
                name: "ProjectTypes");

            migrationBuilder.DropTable(
                name: "RefCJTDescriptionTemplates");

            migrationBuilder.DropTable(
                name: "RefCommonJobTitleBenefit");

            migrationBuilder.DropTable(
                name: "RefCommonJobTitleSalaries");

            migrationBuilder.DropTable(
                name: "ServiceTags");

            migrationBuilder.DropTable(
                name: "ServiceTemplateEntertainmentForms");

            migrationBuilder.DropTable(
                name: "ServiceTemplates");

            migrationBuilder.DropTable(
                name: "SkillCategories");

            migrationBuilder.DropTable(
                name: "SupportTicketDepartments");

            migrationBuilder.DropTable(
                name: "Towns");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CommonServiceReferences");

            migrationBuilder.DropTable(
                name: "ServiceCategories");

            migrationBuilder.DropTable(
                name: "CountryTimeZones");

            migrationBuilder.DropTable(
                name: "JobContractTypes");

            migrationBuilder.DropTable(
                name: "PlatformSubscriptionPlanFeatures");

            migrationBuilder.DropTable(
                name: "PlatformSubscriptionPlans");

            migrationBuilder.DropTable(
                name: "ServiceTemplateEntertainmentTypes");

            migrationBuilder.DropTable(
                name: "CandidateSkills");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "JobIndustries");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "RefCommonJobTitles");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "JobLevels");

            migrationBuilder.DropTable(
                name: "ContinentRegions");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Continents");
        }
    }
}
