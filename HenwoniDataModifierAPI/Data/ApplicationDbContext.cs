using HenwoniDataModifierAPI.Models.Employment;
using HenwoniDataModifierAPI.Models.HelpSupport;
using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Models.Platform;
using HenwoniDataModifierAPI.Models.Services.Genre;
using HenwoniDataModifierAPI.Models.Services;
using HenwoniDataModifierAPI.Models.Skills;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HenwoniDataModifierAPI.Models.Common;

namespace HenwoniDataModifierAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SkillCategory> SkillCategories { get; set; }
        public DbSet<Models.HelpSupport.SupportTicketDepartment> SupportTicketDepartments { get; set; }
        public DbSet<Models.Employment.JobContractType> JobContractTypes { get; set; }
        public DbSet<Models.Employment.JobIndustry> JobIndustries { get; set; }
        public DbSet<Models.Employment.JobContract> JobContracts { get; set; }
        public DbSet<Models.Common.RefCommonJobTitle> RefCommonJobTitles { get; set; }
        public DbSet<Models.Common.RefCJTDescriptionTemplate> RefCJTDescriptionTemplates { get; set; }
        public DbSet<Models.Skills.CandidateSkill> CandidateSkills { get; set; }
        public DbSet<Models.Skills.CandidateSoftSkill> CandidateSoftSkills { get; set; }
        public DbSet<Models.Services.ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Models.Services.EntertainmentCategory> EntertainmentCategories { get; set; }
        public DbSet<Models.Services.ServiceTag> ServiceTags { get; set; }
        public DbSet<Models.Services.ServiceTemplate> ServiceTemplates { get; set; }
        public DbSet<Models.Services.Genre.EntertainmentGenre> EntertainmentGenres { get; set; }
        public DbSet<Models.Services.Genre.LiteratureGenre> LiteratureGenres { get; set; }
        public DbSet<Models.Services.Genre.MusicGenre> MusicGenres { get; set; }
        #region Location
        public DbSet<Models.Pricing.Currency> Currencies { get; set; }
        public DbSet<Models.Location.Continent> Continents { get; set; }
        public DbSet<Models.Location.Language> Languages { get; set; }
        public DbSet<Models.Location.ContinentRegion> ContinentRegions { get; set; }
        public DbSet<Models.Location.Country> Countries { get; set; }
        public DbSet<Models.Location.State> States { get; set; }
        public DbSet<Models.Location.City> Cities { get; set; }
        public DbSet<Models.Location.CountryTimeZone> CountryTimeZones { get; set; }
        public DbSet<Models.Location.CountryTranslations> CountryTranslations { get; set; }
        public DbSet<Models.Location.Town> Towns { get; set; }
        #endregion
        public DbSet<Models.Article> Articles { get; set; }
        public DbSet<Models.Platform.PlatformSubscriptionPlan> PlatformSubscriptionPlans { get; set; }
        public DbSet<Models.Platform.PlatformSubscriptionPlanFeature> PlatformSubscriptionPlanFeatures { get; set; }
        public DbSet<Models.Platform.PlatformSubscriptionPlanPrice> PlatformSubscriptionPlanPrices { get; set; }
        public DbSet<Models.Candidate.CandidateProfileStyle> CandidateProfileStyles { get; set; }
        public DbSet<Models.Project.ProjectType> ProjectTypes { get; set; }
        public DbSet<Models.Candidate.PortfolioStyle> PortfolioStyles { get; set; }
        public DbSet<Models.Organisation.OrganisationType> OrganisationTypes { get; set; }
        public DbSet<Models.Organisation.CandidateRole> CandidateRoles { get; set; }
        public DbSet<Models.Employment.JobLevel> JobLevels { get; set; }
        public DbSet<CommonServiceReference> CommonServiceReferences { get; set; }
        public DbSet<RefCommonJobTitleSalary> RefCommonJobTitleSalaries { get; set; }
        public DbSet<Models.Employment.JobContractAuditingRequirement> JobContractAuditingRequirements { get; set; }
        public DbSet<ServiceTemplateEntertainmentType> ServiceTemplateEntertainmentTypes { get; set; }
        public DbSet<ServiceTemplateEntertainmentForm> ServiceTemplateEntertainmentForms { get; set; }
        public DbSet<PlatformAppVersion> PlatformAppVersions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SupportTicketDepartment>().HasIndex(u => u.SystemName).IsUnique();
            modelBuilder.Entity<JobContractType>().HasIndex(u => u.SystemName).IsUnique();
            modelBuilder.Entity<EntertainmentCategory>().HasIndex(u => u.SystemName).IsUnique();

            modelBuilder.Entity<CandidateSkill>()
                .HasOne(t => t.PrimaryJobIndustry)
                .WithOne()
                .HasForeignKey<CandidateSkill>(c => c.PrimaryJobIndustryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ContinentRegion>()
                .HasOne(t => t.Continent)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Country>()
                .HasOne(t => t.Continent)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<State>()
                .HasOne(t => t.Country)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<City>()
                .HasOne(t => t.State)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<City>()
                .HasOne(t => t.Country)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<City>()
                .HasOne(t => t.ContinentRegion)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Town>()
                .HasOne(t => t.City)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Town>()
                .HasOne(t => t.Continent)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Town>()
                .HasOne(t => t.ContinentRegion)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Town>()
                .HasOne(t => t.Country)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Town>()
                .HasOne(t => t.State)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RefCommonJobTitleSalary>()
                .HasOne(t => t.Currency)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<JobIndustry>()
                    .HasMany(e => e.Skills)
                    .WithMany(x => x.JobIndustries);
            /*modelBuilder.Entity<ApplicationUser>().HasOne(t => t.CandidateEmployment)
					 .WithOne(t => t.User)
					  .HasForeignKey<CandidateEmployment>(t => t.UserId);*/
            modelBuilder.Entity<EntertainmentCategory>().HasIndex(u => u.SystemName).IsUnique();
            modelBuilder.Entity<LiteratureGenre>().HasIndex(u => u.SystemName).IsUnique();
            modelBuilder.Entity<MusicGenre>().HasIndex(u => u.SystemName).IsUnique();
            modelBuilder.Entity<ServiceTemplateEntertainmentType>().HasIndex(u => u.SystemName).IsUnique();
            modelBuilder.Entity<ServiceTemplateEntertainmentForm>().HasIndex(u => u.SystemName).IsUnique();
            modelBuilder.Entity<EntertainmentGenre>().HasIndex(u => u.SystemName).IsUnique();
            modelBuilder.Entity<RefCommonJobTitle>().HasIndex(u => u.SystemName).IsUnique();
        }
    }
}
