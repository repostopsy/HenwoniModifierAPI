using System;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using DotLiquid.Tags;
using HenwoniDataModifierAPI.Data;
using HenwoniDataModifierAPI.Models;
using HenwoniDataModifierAPI.Models.Candidate;
using HenwoniDataModifierAPI.Models.Employment;
using HenwoniDataModifierAPI.Models.HelpSupport;
using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Models.Platform;
using HenwoniDataModifierAPI.Models.Pricing;
using HenwoniDataModifierAPI.Models.Project;
using HenwoniDataModifierAPI.Models.Services;
using HenwoniDataModifierAPI.Models.Services.Genre;
using HenwoniDataModifierAPI.Utilities;
using Humanizer;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HenwoniDataModifierAPI.Automatic
{
    public partial class AutomaticSetup
    {
        public async Task SetupOtherEntitiesAsync(ApplicationDbContext dbContext)
        {
            await JobFinanceSchemeTypeSetupAsync(dbContext);
            await OrganisationPartnershipFinanceSchemeTypesSetupAsync(dbContext);
            await SetupProjectFundMeListingCategoriesAsync(dbContext);
            await SetupAuditingRequirementsAsync(dbContext);
            await SetupCurrenciesAsync(dbContext);
            await SetupCandidateProfileStyleAsync(dbContext);
            await SetupEntertainmentGenresAsync(dbContext);
            await SetupMusicGenresAsync(dbContext);
            await SetupPlatformSubscriptionPlansAsync(dbContext);
            await SetupContinentAsync(dbContext);
            await EntertainmentCategoryAsync(dbContext);
            await JobContractTypeAsync(dbContext);
            await ServiceTemplateAsync(dbContext);
            await ServiceTagAsync(dbContext);
            await SetupCommonServiceReferencesAsync(dbContext);
            await SupportTicketDepartmentAsync(dbContext);
            await SetupProjectTypeAsync(dbContext);
            await SetupPortfolioStyleAsync(dbContext);
            await SetupJobLevelAsync(dbContext);
        }

        public static async Task SetupAuditingRequirementsAsync(ApplicationDbContext dbContext)
        {
            JobContractAuditingRequirement c1 = await dbContext.JobContractAuditingRequirements.Where(x => x.SystemName == "CompanyRemoteAccess").FirstOrDefaultAsync();
            if (c1 == null)
            {
                c1 = new JobContractAuditingRequirement { Title = "Company Remote Access", SystemName = "CompanyRemoteAccess", Excerpt = "The candidate will be able to access one of your computers to work through Hiremos Desktop" };
                dbContext.Add(c1);
            }
            JobContractAuditingRequirement c2 = await dbContext.JobContractAuditingRequirements.Where(x => x.SystemName == "VirtualWorking").FirstOrDefaultAsync();
            if (c2 == null)
            {
                c2 = new JobContractAuditingRequirement { Title = "Virtual Working", SystemName = "VirtualWorking", Excerpt = "Candidates can work in a virtual environment for contracts that can be done virtually. We provide virtual systems where candidates can work, audit and complete the contract." };
                dbContext.Add(c2);
            }
            JobContractAuditingRequirement c3 = await dbContext.JobContractAuditingRequirements.Where(x => x.SystemName == "Video_Audio").FirstOrDefaultAsync();
            if (c3 == null)
            {
                c3 = new JobContractAuditingRequirement { Title = "Video & Audio", SystemName = "Video_Audio", Excerpt = "Candidate will require video and audio capturing hardware to perform the tasks" };
                dbContext.Add(c3);
            }
            await dbContext.SaveChangesAsync();
        }

        public static async Task SetupJobLevelAsync(ApplicationDbContext dbContext)
        {
            JobLevel c1 = await dbContext.JobLevels.Where(x => x.SystemName == "Entry").FirstOrDefaultAsync();
            if (c1 == null)
            {
                c1 = new JobLevel { Title = "Entry", SystemName = "Entry", Excerpt = "Entry" };
                dbContext.Add(c1);
            }

            JobLevel c2 = await dbContext.JobLevels.Where(x => x.SystemName == "Junior").FirstOrDefaultAsync();
            if (c2 == null)
            {
                c2 = new JobLevel { Title = "Junior", SystemName = "Junior", Excerpt = "Junior" };
                dbContext.Add(c2);
            }

            JobLevel c3 = await dbContext.JobLevels.Where(x => x.SystemName == "Professional").FirstOrDefaultAsync();
            if (c3 == null)
            {
                c3 = new JobLevel { Title = "Professional", SystemName = "Professional", Excerpt = "Professional" };
                dbContext.Add(c3);
            }

            JobLevel c4 = await dbContext.JobLevels.Where(x => x.SystemName == "Senior").FirstOrDefaultAsync();
            if (c4 == null)
            {
                c4 = new JobLevel { Title = "Senior", SystemName = "Senior", Excerpt = "Senior" };
                dbContext.Add(c4);
            }
            await dbContext.SaveChangesAsync();
        }

        public static async Task SetupPortfolioStyleAsync(ApplicationDbContext dbContext)
        {
            PortfolioStyle c1 = await dbContext.PortfolioStyles.Where(x => x.SystemName == "DefaultStyle").FirstOrDefaultAsync();
            if (c1 == null)
            {
                c1 = new PortfolioStyle { Title = "Default Style", SystemName = "DefaultStyle", Excerpt = "A image cover based portfolio with external links or assets such as articles" };
                dbContext.PortfolioStyles.Add(c1);
            }

            PortfolioStyle c2 = await dbContext.PortfolioStyles.Where(x => x.SystemName == "WebsitesPortfolio").FirstOrDefaultAsync();
            if (c2 == null)
            {
                c2 = new PortfolioStyle { Title = "Websites Portfolio Style", SystemName = "WebsitesPortfolio", Excerpt = "A portfolio linking to websites or urls." };
                dbContext.PortfolioStyles.Add(c2);
            }
            PortfolioStyle c3 = await dbContext.PortfolioStyles.Where(x => x.SystemName == "VideoFilmPortfolio").FirstOrDefaultAsync();
            if (c3 == null)
            {
                c3 = new PortfolioStyle { Title = "Video/Film portfolio", SystemName = "VideoFilmPortfolio", Excerpt = "A portfolio for displaying films, videos, sounds, etc" };
                dbContext.PortfolioStyles.Add(c3);
            }
            PortfolioStyle c4 = await dbContext.PortfolioStyles.Where(x => x.SystemName == "GalleryPhotography").FirstOrDefaultAsync();
            if (c4 == null)
            {
                c4 = new PortfolioStyle { Title = "Gallery/Photography", SystemName = "GalleryPhotography", Excerpt = "A portfolio for displaying gallery/photo" };
                dbContext.PortfolioStyles.Add(c4);
            }
            await dbContext.SaveChangesAsync();
        }

        public static async Task SetupProjectTypeAsync(ApplicationDbContext dbContext)
        {
            ProjectType c1 = await dbContext.ProjectTypes.Where(x => x.SystemName == "StandardProject").FirstOrDefaultAsync();
            if (c1 == null)
            {
                c1 = new ProjectType { Title = "Standard Project", SystemName = "StandardProject", Excerpt = "" };
                dbContext.ProjectTypes.Add(c1);
            }

            ProjectType c2 = await dbContext.ProjectTypes.Where(x => x.SystemName == "ProgrammingProject").FirstOrDefaultAsync();
            if (c2 == null)
            {
                c2 = new ProjectType { Title = "Programming Project", SystemName = "ProgrammingProject", Excerpt = "" };
                dbContext.ProjectTypes.Add(c2);
            }

            ProjectType c3 = await dbContext.ProjectTypes.Where(x => x.SystemName == "ProgrammingProject").FirstOrDefaultAsync();
            if (c3 == null)
            {
                c3 = new ProjectType { Title = "Music Project", SystemName = "MusicProject", Excerpt = "" };
                dbContext.ProjectTypes.Add(c3);
            }

            ProjectType c4 = await dbContext.ProjectTypes.Where(x => x.SystemName == "FilmingProject").FirstOrDefaultAsync();
            if (c4 == null)
            {
                c4 = new ProjectType { Title = "Filming Project", SystemName = "FilmingProject", Excerpt = "" };
                dbContext.ProjectTypes.Add(c4);
            }
            await dbContext.SaveChangesAsync();
        }

        public static async Task SupportTicketDepartmentAsync(ApplicationDbContext dbContext)
        {
            SupportTicketDepartment c1 = await dbContext.SupportTicketDepartments.Where(x => x.SystemName == "general-support").FirstOrDefaultAsync();
            if (c1 == null)
            {
                c1 = new SupportTicketDepartment { Title = "General Support", SystemName = "general-support", Description = "For all general support queries" };
                dbContext.SupportTicketDepartments.Add(c1);
            }
            SupportTicketDepartment c2 = await dbContext.SupportTicketDepartments.Where(x => x.SystemName == "bugs-issues").FirstOrDefaultAsync();
            if (c2 == null)
            {
                c2 = new SupportTicketDepartment { Title = "Website Bugs & Issues", SystemName = "bugs-issues", Description = "Report bug or issue on the site" };
                dbContext.SupportTicketDepartments.Add(c2);
            }
            await dbContext.SaveChangesAsync();
        }

        public static async Task JobFinanceSchemeTypeSetupAsync(ApplicationDbContext dbContext)
        {
            var l = await dbContext.Languages.Where(x => x.SystemName == Constants.DefaultLanguage).FirstOrDefaultAsync();
            var a = new JobFinanceSchemeType { Language=l, Title = JobFinanceSchemeType.SalaryWageCompensationName, Excerpt = "", SystemName = JobFinanceSchemeType.SalaryWageCompensationName };
            var b = new JobFinanceSchemeType { Language = l, Title = JobFinanceSchemeType.GeneralPartnershipName, Excerpt = "Employees become legal partners, sharing profits and liabilities. This requires a lot of personal liability.", SystemName = JobFinanceSchemeType.GeneralPartnershipName };
            var c = new JobFinanceSchemeType { Language = l, Title = JobFinanceSchemeType.LimitedPartnershipName, Excerpt = "Employees receive a percentage of profits without formal ownership. (Does not provide legal ownership in company)", SystemName = JobFinanceSchemeType.LimitedPartnershipName };
            var d = new JobFinanceSchemeType { Language = l, Title = JobFinanceSchemeType.ProfitSharingPartnershipName, Excerpt = "Employees receive a percentage of profits without formal ownership. (Does not provide legal ownership in company)", SystemName = JobFinanceSchemeType.ProfitSharingPartnershipName };
            var e = new JobFinanceSchemeType { Language = l, Title = JobFinanceSchemeType.EmployeeStockOwnershipName, Excerpt = "This gives employees a ownership stake in the company.)", SystemName = JobFinanceSchemeType.EmployeeStockOwnershipName };

            if (!await dbContext.JobFinanceSchemeTypes.AnyAsync(x=>x.SystemName== JobFinanceSchemeType.SalaryWageCompensationName)) dbContext.JobFinanceSchemeTypes.AddAsync(a);
            if (!await dbContext.JobFinanceSchemeTypes.AnyAsync(x => x.SystemName == JobFinanceSchemeType.GeneralPartnershipName)) dbContext.JobFinanceSchemeTypes.AddAsync(b);
            if (!await dbContext.JobFinanceSchemeTypes.AnyAsync(x => x.SystemName == JobFinanceSchemeType.LimitedPartnershipName)) dbContext.JobFinanceSchemeTypes.AddAsync(c);
            if (!await dbContext.JobFinanceSchemeTypes.AnyAsync(x => x.SystemName == JobFinanceSchemeType.ProfitSharingPartnershipName)) dbContext.JobFinanceSchemeTypes.AddAsync(d);
            if (!await dbContext.JobFinanceSchemeTypes.AnyAsync(x => x.SystemName == JobFinanceSchemeType.EmployeeStockOwnershipName)) dbContext.JobFinanceSchemeTypes.AddAsync(e);
            await dbContext.SaveChangesAsync();
        }

        public static async Task OrganisationPartnershipFinanceSchemeTypesSetupAsync(ApplicationDbContext dbContext)
        {
            var l = await dbContext.Languages.Where(x => x.SystemName == Constants.DefaultLanguage).FirstOrDefaultAsync();
            var b = new OrganisationPartnershipFinanceSchemeType { Language = l, Title = OrganisationPartnershipFinanceSchemeType.GeneralPartnershipName, Excerpt = "Employees become legal partners, sharing profits and liabilities. This requires a lot of personal liability.", SystemName = OrganisationPartnershipFinanceSchemeType.GeneralPartnershipName };
            var c = new OrganisationPartnershipFinanceSchemeType { Language = l, Title = OrganisationPartnershipFinanceSchemeType.LimitedPartnershipName, Excerpt = "Employees receive a percentage of profits without formal ownership. (Does not provide legal ownership in company)", SystemName = OrganisationPartnershipFinanceSchemeType.LimitedPartnershipName };
            var d = new OrganisationPartnershipFinanceSchemeType { Language = l, Title = OrganisationPartnershipFinanceSchemeType.ProfitSharingPartnershipName, Excerpt = "Employees receive a percentage of profits without formal ownership. (Does not provide legal ownership in company)", SystemName = OrganisationPartnershipFinanceSchemeType.ProfitSharingPartnershipName };
            var e = new OrganisationPartnershipFinanceSchemeType { Language = l, Title = OrganisationPartnershipFinanceSchemeType.EmployeeStockOwnershipName, Excerpt = "This gives employees a ownership stake in the company.)", SystemName = OrganisationPartnershipFinanceSchemeType.EmployeeStockOwnershipName };

            dbContext.OrganisationPartnershipFinanceSchemeTypes.AddAsync(b);
            dbContext.OrganisationPartnershipFinanceSchemeTypes.AddAsync(c);
            dbContext.OrganisationPartnershipFinanceSchemeTypes.AddAsync(d);
            dbContext.OrganisationPartnershipFinanceSchemeTypes.AddAsync(e);

            await dbContext.SaveChangesAsync();
        }
        public static async Task ServiceTagAsync(ApplicationDbContext dbContext)
        {
            ServiceTag c1 = await dbContext.ServiceTags.Where(x => x.SystemName == "collabration-project").FirstOrDefaultAsync();
            if (c1 == null)
            {
                c1 = new ServiceTag { Title = "NFT", SystemName = "collabration-project" };
                dbContext.ServiceTags.Add(c1);
            }
            ServiceTag c2 = await dbContext.ServiceTags.Where(x => x.SystemName == "collabration-project").FirstOrDefaultAsync();
            if (c2 == null)
            {
                c2 = new ServiceTag { Title = "Cartoons", SystemName = "cartoons" };
                dbContext.ServiceTags.Add(c2);
            }
            ServiceTag c3 = await dbContext.ServiceTags.Where(x => x.SystemName == "cover").FirstOrDefaultAsync();
            if (c3 == null)
            {
                c3 = new ServiceTag { Title = "Cover", SystemName = "cover" };
                dbContext.ServiceTags.Add(c3);
            }
            ServiceTag c4 = await dbContext.ServiceTags.Where(x => x.SystemName == "music").FirstOrDefaultAsync();
            if (c4 == null)
            {
                c4 = new ServiceTag { Title = "Music", SystemName = "music" };
                dbContext.ServiceTags.Add(c4);
            }
            await dbContext.SaveChangesAsync();
        }

        public static async Task SetupCommonServiceReferencesAsync(ApplicationDbContext dbContext)
        {
            ServiceCategory programmingTech = await dbContext.ServiceCategories.Where(x => x.SystemName == "programming-tech").FirstOrDefaultAsync();
            CommonServiceReference f1 = await dbContext.CommonServiceReferences.Where(x => x.SystemName == "CreateAWordpressWebsite").FirstOrDefaultAsync();
            if (f1 == null)
            {
                f1 = new CommonServiceReference { Title = "Create A Wordpress Website", SystemName = "CreateAWordpressWebsite", Excerpt = "I will create a Wordpress website for your project or business" };
                dbContext.CommonServiceReferences.Add(f1);
            }
            await dbContext.SaveChangesAsync();
        }

        public static async Task ServiceTemplateAsync(ApplicationDbContext dbContext)
        {
            // 01
            ServiceTemplateEntertainmentType stet1 = await dbContext.ServiceTemplateEntertainmentTypes.Where(x => x.SystemName == "LiveEntertainment").FirstOrDefaultAsync();
            if (stet1 == null)
            {
                stet1 = new ServiceTemplateEntertainmentType { Title = "Live Entertainment", SystemName = "LiveEntertainment", Excerpt = "Including concerts, threatre, comedy shows, circus, sports events" };
                dbContext.ServiceTemplateEntertainmentTypes.Add(stet1);
            }
            // Setup forms
            ServiceTemplateEntertainmentForm stef1 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "Concerts").FirstOrDefaultAsync();
            if (stef1 == null)
            {
                stef1 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet1.Id, Title = "Concerts", SystemName = "Concerts", Excerpt = "Enjoy live music performances" };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef1);
                stet1.Forms.Add(stef1);
            }
            ServiceTemplateEntertainmentForm stef2 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "Theatre").FirstOrDefaultAsync();
            if (stef2 == null)
            {
                stef2 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet1.Id, Title = "Theatre", SystemName = "Theatre", Excerpt = "Watch plays, musicals, and operas." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef2);
                stet1.Forms.Add(stef2);
            }
            ServiceTemplateEntertainmentForm stef3 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "ComedyShows").FirstOrDefaultAsync();
            if (stef3 == null)
            {
                stef3 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet1.Id, Title = "Comedy Shows", SystemName = "ComedyShows", Excerpt = "Laugh along with stand-up comedians" };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef3);
                stet1.Forms.Add(stef3);
            }
            ServiceTemplateEntertainmentForm stef4 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "Circus").FirstOrDefaultAsync();
            if (stef4 == null)
            {
                stef4 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet1.Id, Title = "Circus", SystemName = "Circus", Excerpt = "Experience acrobatics, clowns, and animal acts." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef4);
                stet1.Forms.Add(stef4);
            }
            ServiceTemplateEntertainmentForm stef5 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "SportsEvents").FirstOrDefaultAsync();
            if (stef5 == null)
            {
                stef5 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet1.Id, Title = "Sports Events", SystemName = "SportsEvents", Excerpt = "Attend live sports games and matches." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef5);
                stet1.Forms.Add(stef5);
            }
            await dbContext.SaveChangesAsync();

            // 02
            ServiceTemplateEntertainmentType stet2 = await dbContext.ServiceTemplateEntertainmentTypes.Where(x => x.SystemName == "MediaEntertainment").FirstOrDefaultAsync();
            if (stet2 == null)
            {
                stet2 = new ServiceTemplateEntertainmentType { Title = "Media Entertainment", SystemName = "MediaEntertainment", Excerpt = "Including movies, television, radio, podcasts" };
                dbContext.ServiceTemplateEntertainmentTypes.Add(stet2);
            }
            ServiceTemplateEntertainmentForm stef6 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "Movies").FirstOrDefaultAsync();
            if (stef6 == null)
            {
                stef6 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet2.Id, Title = "Movies", SystemName = "Movies", Excerpt = "Watch films in cinemas or at home." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef6);
                stet1.Forms.Add(stef6);
            }
            ServiceTemplateEntertainmentForm stef7 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "Television").FirstOrDefaultAsync();
            if (stef7 == null)
            {
                stef7 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet2.Id, Title = "Television", SystemName = "Television", Excerpt = "Enjoy TV shows, series, and documentaries." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef7);
                stet1.Forms.Add(stef7);
            }
            ServiceTemplateEntertainmentForm stef8 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "Radio").FirstOrDefaultAsync();
            if (stef8 == null)
            {
                stef8 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet2.Id, Title = "Radio", SystemName = "Radio", Excerpt = "Listen to music, talk shows, and news." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef8);
                stet1.Forms.Add(stef8);
            }
            ServiceTemplateEntertainmentForm stef8_1 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "MusicAlbum").FirstOrDefaultAsync();
            if (stef8_1 == null)
            {
                stef8_1 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet2.Id, Title = "Music Album", SystemName = "MusicAlbum", Excerpt = "Listen to a music album from an artist" };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef8_1);
                stet1.Forms.Add(stef8_1);
            }
            ServiceTemplateEntertainmentForm stef9 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "Podcasts").FirstOrDefaultAsync();
            if (stef9 == null)
            {
                stef9 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet2.Id, Title = "Podcasts", SystemName = "Podcasts", Excerpt = "Tune into various topics and stories." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef9);
                stet1.Forms.Add(stef9);
            }

            ServiceTemplateEntertainmentForm stef10 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "VLog").FirstOrDefaultAsync();
            if (stef10 == null)
            {
                stef10 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet2.Id, Title = "VLog", SystemName = "VLog", Excerpt = "A Vlog about lifestyle, etc" };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef10);
                stet1.Forms.Add(stef10);
            }


            // 03
            ServiceTemplateEntertainmentType stet3 = await dbContext.ServiceTemplateEntertainmentTypes.Where(x => x.SystemName == "CulturalEntertainment").FirstOrDefaultAsync();
            if (stet3 == null)
            {
                stet3 = new ServiceTemplateEntertainmentType { Title = "Cultural Entertainment", SystemName = "CulturalEntertainment", Excerpt = "Including museums, festivals, art exhibits, dance performances" };
                dbContext.ServiceTemplateEntertainmentTypes.Add(stet3);
            }
            ServiceTemplateEntertainmentForm stef11 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "Festivals").FirstOrDefaultAsync();
            if (stef11 == null)
            {
                stef11 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet3.Id, Title = "Festivals", SystemName = "Festivals", Excerpt = "Participate in cultural, music, and food festivals." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef11);
                stet1.Forms.Add(stef11);
            }
            ServiceTemplateEntertainmentForm stef12 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "ArtExhibits").FirstOrDefaultAsync();
            if (stef12 == null)
            {
                stef12 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet3.Id, Title = "Art Exhibits", SystemName = "ArtExhibits", Excerpt = "View paintings, sculptures, and other artworks." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef12);
                stet1.Forms.Add(stef12);
            }
            ServiceTemplateEntertainmentForm stef13 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "DancePerformances").FirstOrDefaultAsync();
            if (stef13 == null)
            {
                stef13 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet3.Id, Title = "Dance Performances", SystemName = "DancePerformances", Excerpt = "Enjoy ballet, contemporary, and traditional dances." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef13);
                stet1.Forms.Add(stef13);
            }
            ServiceTemplateEntertainmentForm stef21 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "Museums").FirstOrDefaultAsync();
            if (stef21 == null)
            {
                stef21 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet3.Id, Title = "Museums", SystemName = "Museums", Excerpt = "Explore art, history, and science exhibits." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef21);
                stet1.Forms.Add(stef21);
            }

            // 04
            ServiceTemplateEntertainmentType stet4 = await dbContext.ServiceTemplateEntertainmentTypes.Where(x => x.SystemName == "OutdoorEntertainment").FirstOrDefaultAsync();
            if (stet4 == null)
            {
                stet4 = new ServiceTemplateEntertainmentType { Title = "Outdoor Entertainment", SystemName = "OutdoorEntertainment", Excerpt = "Venue entertainment including amusement parks, zoos and aquariums, parks and gardens, sports and recreation" };
                dbContext.ServiceTemplateEntertainmentTypes.Add(stet4);
            }
            ServiceTemplateEntertainmentForm stef14 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "AmusementParks").FirstOrDefaultAsync();
            if (stef14 == null)
            {
                stef14 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet4.Id, Title = "Amusement Parks", SystemName = "AmusementParks", Excerpt = "Have fun on rides and attractions." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef14);
                stet1.Forms.Add(stef14);
            }
            ServiceTemplateEntertainmentForm stef15 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "ZoosAndAquariums").FirstOrDefaultAsync();
            if (stef15 == null)
            {
                stef15 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet4.Id, Title = "Zoos And Aquariums", SystemName = "ZoosAndAquariums", Excerpt = "See animals and marine life." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef15);
                stet1.Forms.Add(stef15);
            }
            ServiceTemplateEntertainmentForm stef16 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "ParksAndGardens").FirstOrDefaultAsync();
            if (stef16 == null)
            {
                stef16 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet4.Id, Title = "Parks and Gardens", SystemName = "ParksAndGardens", Excerpt = "Relax and enjoy nature." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef16);
                stet1.Forms.Add(stef16);
            }
            ServiceTemplateEntertainmentForm stef17 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "SportsAndRecreation").FirstOrDefaultAsync();
            if (stef17 == null)
            {
                stef17 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet4.Id, Title = "Sports and Recreation", SystemName = "SportsAndRecreation", Excerpt = "Engage in activities like hiking, cycling, and swimming." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef17);
                stet1.Forms.Add(stef17);
            }

            // 05
            ServiceTemplateEntertainmentType stet5 = await dbContext.ServiceTemplateEntertainmentTypes.Where(x => x.SystemName == "InteractiveEntertainment").FirstOrDefaultAsync();
            if (stet5 == null)
            {
                stet5 = new ServiceTemplateEntertainmentType { Title = "Interactive Entertainment", SystemName = "InteractiveEntertainment", Excerpt = "Venue entertainment including escape rooms, board games, role-playing games" };
                dbContext.ServiceTemplateEntertainmentTypes.Add(stet5);
            }
            ServiceTemplateEntertainmentForm stef18 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "EscapeRooms").FirstOrDefaultAsync();
            if (stef18 == null)
            {
                stef18 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet5.Id, Title = "Escape Rooms", SystemName = "EscapeRooms", Excerpt = "Solve puzzles and challenges to “escape” themed rooms." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef18);
                stet1.Forms.Add(stef18);
            }
            ServiceTemplateEntertainmentForm stef19 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "BoardGames").FirstOrDefaultAsync();
            if (stef19 == null)
            {
                stef19 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet5.Id, Title = "Board Games", SystemName = "BoardGames", Excerpt = "Play classic and modern board games with friends and family." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef19);
                stet1.Forms.Add(stef19);
            }
            ServiceTemplateEntertainmentForm stef20 = await dbContext.ServiceTemplateEntertainmentForms.Where(x => x.SystemName == "RolePlayingGames").FirstOrDefaultAsync();
            if (stef20 == null)
            {
                stef20 = new ServiceTemplateEntertainmentForm { EntertainmentTypeId = stet5.Id, Title = "Role-Playing Games", SystemName = "RolePlayingGames", Excerpt = "Immerse yourself in character-driven adventures." };
                dbContext.ServiceTemplateEntertainmentForms.Add(stef20);
                stet1.Forms.Add(stef20);
            }

            // Setup templates
            ServiceTemplate c1 = await dbContext.ServiceTemplates.Where(x => x.SystemName == "StandardTemplate").FirstOrDefaultAsync();
            if (c1 == null)
            {
                c1 = new ServiceTemplate { Title = "Standard Template", SystemName = "StandardTemplate", Excerpt = "Default service template" };
                dbContext.ServiceTemplates.Add(c1);
            }

            ServiceTemplate c2 = await dbContext.ServiceTemplates.Where(x => x.SystemName == "EntertainmentTemplate").FirstOrDefaultAsync();
            if (c2 == null)
            {
                c2 = new ServiceTemplate { Title = "Entertainment Template", SystemName = "EntertainmentTemplate", Excerpt = "A template for creating a TV show show made up of seasons and series" };
                dbContext.ServiceTemplates.Add(c2);
            }

            // new ServiceTemplate {  Title = "TV Show Template", SystemName= "TVShowTemplate", Excerpt="A template for creating a TV show show made up of seasons and series" },

            ServiceTemplate c3 = await dbContext.ServiceTemplates.Where(x => x.SystemName == "LiveStreamingTemplate").FirstOrDefaultAsync();
            if (c3 == null)
            {
                c3 = new ServiceTemplate { Title = "Live Streaming Template", SystemName = "LiveStreamingTemplate", Excerpt = "A scheduled live stream service provided over a live stream to multiple users" };
                dbContext.ServiceTemplates.Add(c3);
            }

            ServiceTemplate c4 = await dbContext.ServiceTemplates.Where(x => x.SystemName == "CourseTemplate").FirstOrDefaultAsync();
            if (c4 == null)
            {
                c4 = new ServiceTemplate { Title = "Course Template", SystemName = "CourseTemplate", Excerpt = "A service template for providing a course" };
                dbContext.ServiceTemplates.Add(c4);
            }

            ServiceTemplate c5 = await dbContext.ServiceTemplates.Where(x => x.SystemName == "TuitionTemplate").FirstOrDefaultAsync();
            if (c5 == null)
            {
                c5 = new ServiceTemplate { Title = "Tuition Template", SystemName = "TuitionTemplate", Excerpt = "A service template for providing tuition through a single live chat or multiple user live streaming" };
                dbContext.ServiceTemplates.Add(c5);
            }
            await dbContext.SaveChangesAsync();

        }

        public static async Task SetupCurrenciesAsync(ApplicationDbContext dbContext)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var t = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            using (Stream stream = assembly.GetManifestResourceStream("HenwoniDataModifierAPI.Data.currencies.json"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string txt = reader.ReadToEnd();
                    List<Currency> currencies = JsonSerializer.Deserialize<List<Currency>>(txt);
                    foreach (Currency currency in currencies)
                    {
                        Currency dbCurrency = await dbContext.Currencies.Where(x => x.CurrencyName == currency.CurrencyName).FirstOrDefaultAsync();
                        if (dbCurrency == null)
                        {
                            currency.Id = 0;
                            dbContext.Currencies.Add(currency);
                        }
                    }
                }
            }
            await dbContext.SaveChangesAsync();
        }
        public static async Task JobContractTypeAsync(ApplicationDbContext dbContext)
        {
            JobLayoutType c1 = await dbContext.JobLayoutTypes.Where(x => x.SystemName == "full-time").FirstOrDefaultAsync();
            if (c1 == null)
            {
                c1 = new JobLayoutType { SystemName = "full-time", Title = "Full Time", Description = "" };
                dbContext.JobLayoutTypes.Add(c1);
            }
            JobLayoutType c2 = await dbContext.JobLayoutTypes.Where(x => x.SystemName == "permanent").FirstOrDefaultAsync();
            if (c2 == null)
            {
                c2 = new JobLayoutType { SystemName = "permanent", Title = "Permanent", Description = "" };
                dbContext.JobLayoutTypes.Add(c2);
            }
            JobLayoutType c3 = await dbContext.JobLayoutTypes.Where(x => x.SystemName == "part-time").FirstOrDefaultAsync();
            if (c3 == null)
            {
                c3 = new JobLayoutType { SystemName = "part-time", Title = "Part Time", Description = "" };
                dbContext.JobLayoutTypes.Add(c3);
            }

            JobLayoutType c4 = await dbContext.JobLayoutTypes.Where(x => x.SystemName == "temporary").FirstOrDefaultAsync();
            if (c4 == null)
            {
                c4 = new JobLayoutType { SystemName = "temporary", Title = "Temporary", Description = "" };
                dbContext.JobLayoutTypes.Add(c4);
            }

            JobLayoutType c5 = await dbContext.JobLayoutTypes.Where(x => x.SystemName == "fixed-term").FirstOrDefaultAsync();
            if (c5 == null)
            {
                c5 = new JobLayoutType { SystemName = "fixed-term", Title = "Fixed Term", Description = "" };
                dbContext.JobLayoutTypes.Add(c5);
            }

            JobLayoutType c6 = await dbContext.JobLayoutTypes.Where(x => x.SystemName == "volunteer").FirstOrDefaultAsync();
            if (c6 == null)
            {
                c6 = new JobLayoutType { SystemName = "volunteer", Title = "Volunteer", Description = "" };
                dbContext.JobLayoutTypes.Add(c6);
            }

            JobLayoutType c7 = await dbContext.JobLayoutTypes.Where(x => x.SystemName == "apprenticeship").FirstOrDefaultAsync();
            if (c7 == null)
            {
                c7 = new JobLayoutType { SystemName = "apprenticeship", Title = "Apprenticeship", Description = "" };
                dbContext.JobLayoutTypes.Add(c7);
            }

            JobLayoutType c8 = await dbContext.JobLayoutTypes.Where(x => x.SystemName == "internship").FirstOrDefaultAsync();
            if (c8 == null)
            {
                c8 = new JobLayoutType { SystemName = "internship", Title = "Internship", Description = "" };
                dbContext.JobLayoutTypes.Add(c8);
            }
            await dbContext.SaveChangesAsync();
        }
        public static async Task EntertainmentCategoryAsync(ApplicationDbContext dbContext)
        {
            EntertainmentCategory c1 = await dbContext.EntertainmentCategories.Where(x => x.SystemName == "tv-show").FirstOrDefaultAsync();
            if (c1 == null)
            {
                c1 = new EntertainmentCategory { Title = "TV Show", SystemName = "tv-show", Excerpt = "" };
                dbContext.EntertainmentCategories.Add(c1);
            }
            await dbContext.SaveChangesAsync();

        }
        public static async Task SetupCandidateProfileStyleAsync(ApplicationDbContext dbContext)
        {
            CandidateProfileStyle c1 = await dbContext.CandidateProfileStyles.Where(x => x.SystemName == "StandardStyle").FirstOrDefaultAsync();
            if (c1 == null)
            {
                c1 = new CandidateProfileStyle { Title = "Standard Style", SystemName = "StandardStyle", Excerpt = "A standard resume style profile" };
                dbContext.CandidateProfileStyles.Add(c1);
            }


            CandidateProfileStyle c2 = await dbContext.CandidateProfileStyles.Where(x => x.SystemName == "VideoFilmStyle").FirstOrDefaultAsync();
            if (c2 == null)
            {
                c2 = new CandidateProfileStyle { Title = "Videos & Films", SystemName = "VideoFilmStyle", Excerpt = "A profile for showcasing films and videos" };
                dbContext.CandidateProfileStyles.Add(c2);
            }


            CandidateProfileStyle c3 = await dbContext.CandidateProfileStyles.Where(x => x.SystemName == "PhotographyGalleryStyle").FirstOrDefaultAsync();
            if (c3 == null)
            {
                c3 = new CandidateProfileStyle { Title = "Photography/GalleryStyle", SystemName = "PhotographyGalleryStyle", Excerpt = "" };
                dbContext.CandidateProfileStyles.Add(c3);
            }

            CandidateProfileStyle c4 = await dbContext.CandidateProfileStyles.Where(x => x.SystemName == "ArtistProfileStyle").FirstOrDefaultAsync();
            if (c4 == null)
            {
                c4 = new CandidateProfileStyle { Title = "Artist Profile", SystemName = "ArtistProfileStyle", Excerpt = "A profile for artists" };
                dbContext.CandidateProfileStyles.Add(c4);
            }
            await dbContext.SaveChangesAsync();
        }

        // Might be unnecessary since the data is loaded by another process as well
        // @TODO: Verfiy that - that process has the same data
        public static async Task SetupContinentAsync(ApplicationDbContext dbContext)
        {
            Continent c1 = await dbContext.Continents.Where(x => x.SystemName == "Africa").FirstOrDefaultAsync();
            if (c1 == null)
            {
                c1 = new Continent { Name = "Africa", SystemName = "Africa" };
                dbContext.Continents.Add(c1);
            }

            Continent c2 = await dbContext.Continents.Where(x => x.SystemName == "Americas").FirstOrDefaultAsync();
            if (c2 == null)
            {
                c2 = new Continent { Name = "Americas", SystemName = "Americas" };
                dbContext.Continents.Add(c2);
            }

            Continent c3 = await dbContext.Continents.Where(x => x.SystemName == "Asia").FirstOrDefaultAsync();
            if (c3 == null)
            {
                c3 = new Continent { Name = "Asia", SystemName = "Asia" };
                dbContext.Continents.Add(c3);
            }

            Continent c4 = await dbContext.Continents.Where(x => x.SystemName == "Europe").FirstOrDefaultAsync();
            if (c4 == null)
            {
                c4 = new Continent { Name = "Europe", SystemName = "Europe" };
                dbContext.Continents.Add(c4);
            }

            Continent c5 = await dbContext.Continents.Where(x => x.SystemName == "Oceania").FirstOrDefaultAsync();
            if (c5 == null)
            {
                c5 = new Continent { Name = "Oceania", SystemName = "Oceania" };
                dbContext.Continents.Add(c5);
            }

            Continent c6 = await dbContext.Continents.Where(x => x.SystemName == "Polar").FirstOrDefaultAsync();
            if (c6 == null)
            {
                c6 = new Continent { Name = "Polar", SystemName = "Polar" };
                dbContext.Continents.Add(c6);
            }
            await dbContext.SaveChangesAsync();
        }

        public static async Task SetupPlatformSubscriptionPlansAsync(ApplicationDbContext dbContext)
        {
            PlatformSubscriptionPlan f1 = await dbContext.PlatformSubscriptionPlans.Where(x => x.SystemName == "freeforever").FirstOrDefaultAsync();
            if (f1 == null)
            {
                f1 = new PlatformSubscriptionPlan { Title = "Free", SystemName = "freeforever", Excerpt = "Best Settings for Startups", Active = true, Order = 0 };
                var D = await dbContext.Currencies.ToListAsync();
                PlatformSubscriptionPlanPrice pricing = new PlatformSubscriptionPlanPrice();
                pricing.Price = 0;
                pricing.Currency = await dbContext.Currencies.Where(x => x.CurrencyName == "USD").FirstOrDefaultAsync();
                pricing.CurrencyId = pricing.Currency.Id;
                pricing.Plan = f1;
                pricing.PlanId = f1.Id;
                f1.Pricing.Add(pricing);
                dbContext.PlatformSubscriptionPlans.Add(f1);
            }
            PlatformSubscriptionPlan f2 = await dbContext.PlatformSubscriptionPlans.Where(x => x.SystemName == "premium").FirstOrDefaultAsync();
            if (f2 == null)
            {
                f2 = new PlatformSubscriptionPlan { Title = "Premium", SystemName = "premium", Excerpt = "Best Settings for Advanced Business", Active = true, Order = 1 };

                PlatformSubscriptionPlanPrice pricing = new PlatformSubscriptionPlanPrice();
                pricing.Price = 23;
                pricing.Currency = await dbContext.Currencies.Where(x => x.CurrencyName == "USD").FirstOrDefaultAsync();
                pricing.CurrencyId = pricing.Currency.Id;
                pricing.Plan = f2;
                pricing.PlanId = f2.Id;
                f2.Pricing.Add(pricing);
                dbContext.PlatformSubscriptionPlans.Add(f2);
            }
            PlatformSubscriptionPlanFeature y1 = await dbContext.PlatformSubscriptionPlanFeatures.Where(x => x.SystemName == "Organisations").FirstOrDefaultAsync();
            if (y1 == null)
            {
                y1 = new PlatformSubscriptionPlanFeature { Title = "Up to 1 Organisation", SystemName = "Organisations", Excerpt = "Create up to 1 organisation", Active = true };
                dbContext.PlatformSubscriptionPlanFeatures.Add(y1);
            }
            PlatformSubscriptionPlanFeature y2 = await dbContext.PlatformSubscriptionPlanFeatures.Where(x => x.SystemName == "Collaborations").FirstOrDefaultAsync();
            if (y2 == null)
            {
                y2 = new PlatformSubscriptionPlanFeature { Title = "Collaborations", SystemName = "Collaborations", Excerpt = "Patnerships and collabolations", Active = true };
                dbContext.PlatformSubscriptionPlanFeatures.Add(y2);
            }
            PlatformSubscriptionPlanFeature y3 = await dbContext.PlatformSubscriptionPlanFeatures.Where(x => x.SystemName == "ProjectManagement").FirstOrDefaultAsync();
            if (y3 == null)
            {
                y3 = new PlatformSubscriptionPlanFeature { Title = "Project management", SystemName = "ProjectManagement", Excerpt = "Project management tools", Active = true };
                dbContext.PlatformSubscriptionPlanFeatures.Add(y3);
            }
            PlatformSubscriptionPlanFeature y4 = await dbContext.PlatformSubscriptionPlanFeatures.Where(x => x.SystemName == "ProjectsToCreate").FirstOrDefaultAsync();
            if (y4 == null)
            {
                y4 = new PlatformSubscriptionPlanFeature { Title = "Up to 20 projects", SystemName = "ProjectsToCreate", Excerpt = "Create up to 20 projects for your candidates", Active = true };
                dbContext.PlatformSubscriptionPlanFeatures.Add(y4);
            }
            PlatformSubscriptionPlanFeature y5 = await dbContext.PlatformSubscriptionPlanFeatures.Where(x => x.SystemName == "ProjectMilestonesTimelines").FirstOrDefaultAsync();
            if (y5 == null)
            {
                y5 = new PlatformSubscriptionPlanFeature { Title = "Project Milestones, Timelines", SystemName = "ProjectMilestonesTimelines", Excerpt = "Get analytical and performance data for your projects", Active = true };
                dbContext.PlatformSubscriptionPlanFeatures.Add(y5);
            }
            PlatformSubscriptionPlanFeature y6 = await dbContext.PlatformSubscriptionPlanFeatures.Where(x => x.SystemName == "AnalyticsPerformance").FirstOrDefaultAsync();
            if (y6 == null)
            {
                y6 = new PlatformSubscriptionPlanFeature { Title = "Analytics & Performance", SystemName = "AnalyticsPerformance", Excerpt = "Other analytical and performance data including candidates", Active = true };
                dbContext.PlatformSubscriptionPlanFeatures.Add(y6);
            }
            PlatformSubscriptionPlanFeature y7 = await dbContext.PlatformSubscriptionPlanFeatures.Where(x => x.SystemName == "HireCandidates").FirstOrDefaultAsync();
            if (y7 == null)
            {
                y7 = new PlatformSubscriptionPlanFeature { Title = "Hire Candidates", SystemName = "HireCandidates", Excerpt = "Find and hire candidates", Active = true };
                dbContext.PlatformSubscriptionPlanFeatures.Add(y7);
            }
            PlatformSubscriptionPlanFeature y8 = await dbContext.PlatformSubscriptionPlanFeatures.Where(x => x.SystemName == "InterviewCandidates").FirstOrDefaultAsync();
            if (y8 == null)
            {
                y8 = new PlatformSubscriptionPlanFeature { Title = "Interview Candidate", SystemName = "InterviewCandidates", Excerpt = "Interview and connect with candidates", Active = true };
                dbContext.PlatformSubscriptionPlanFeatures.Add(y8);
            }
            PlatformSubscriptionPlanFeature y9 = await dbContext.PlatformSubscriptionPlanFeatures.Where(x => x.SystemName == "AccessToFunding").FirstOrDefaultAsync();
            if (y9 == null)
            {
                y9 = new PlatformSubscriptionPlanFeature { Title = "Access To Funding", SystemName = "AccessToFunding", Excerpt = "Access to funding for your projects", Active = true };
                dbContext.PlatformSubscriptionPlanFeatures.Add(y9);
            }
            PlatformSubscriptionPlanFeature y10 = await dbContext.PlatformSubscriptionPlanFeatures.Where(x => x.SystemName == "PostBrowseJobs").FirstOrDefaultAsync();
            if (y10 == null)
            {
                y10 = new PlatformSubscriptionPlanFeature { Title = "Post & Browse Jobs", SystemName = "PostBrowseJobs", Excerpt = "Post and browse jobs", Active = true };
                dbContext.PlatformSubscriptionPlanFeatures.Add(y10);
            }
            PlatformSubscriptionPlanFeature y11 = await dbContext.PlatformSubscriptionPlanFeatures.Where(x => x.SystemName == "PostBrowseServices").FirstOrDefaultAsync();
            if (y11 == null)
            {
                y11 = new PlatformSubscriptionPlanFeature { Title = "Post & Browse Services", SystemName = "PostBrowseServices", Excerpt = "Post and browse services", Active = true };
                dbContext.PlatformSubscriptionPlanFeatures.Add(y11);
            }
            await dbContext.SaveChangesAsync();
            f1.Features.Add(y1); await dbContext.SaveChangesAsync(); f1.Features.Add(y2); f1.Features.Add(y3); f1.Features.Add(y4); f1.Features.Add(y5); f1.Features.Add(y6); f1.Features.Add(y7); f1.Features.Add(y8);
            f2.Features.Add(y1); f2.Features.Add(y2); f2.Features.Add(y3); f2.Features.Add(y4); f2.Features.Add(y5); f2.Features.Add(y6); f2.Features.Add(y7); f2.Features.Add(y8); f2.Features.Add(y9); f2.Features.Add(y10); f2.Features.Add(y11);
            await dbContext.SaveChangesAsync();
        }
        public static async Task SetupMusicGenresAsync(ApplicationDbContext dbContext)
        {
            MusicGenre y1 = await dbContext.MusicGenres.Where(x => x.SystemName == "pop").FirstOrDefaultAsync();
            if (y1 == null)
            {
                y1 = new MusicGenre { Title = "Pop", SystemName = "pop", Excerpt = "Mainstream, catchy melodies and hooks." };
                dbContext.MusicGenres.Add(y1);
            }
            MusicGenre y2 = await dbContext.MusicGenres.Where(x => x.SystemName == "rock").FirstOrDefaultAsync();
            if (y2 == null)
            {
                y2 = new MusicGenre { Title = "Rock", SystemName = "rock", Excerpt = "Characterized by electric guitars, strong rhythms." };
                dbContext.MusicGenres.Add(y2);
            }
            MusicGenre y3 = await dbContext.MusicGenres.Where(x => x.SystemName == "hiphop").FirstOrDefaultAsync();
            if (y3 == null)
            {
                y3 = new MusicGenre { Title = "Hip-Hop", SystemName = "hiphop", Excerpt = "Rhythmic music often featuring rapping." };
                dbContext.MusicGenres.Add(y3);
            }
            MusicGenre y4 = await dbContext.MusicGenres.Where(x => x.SystemName == "jazz").FirstOrDefaultAsync();
            if (y4 == null)
            {
                y4 = new MusicGenre { Title = "Jazz", SystemName = "jazz", Excerpt = "Improvisation, complex chords, and syncopation." };
                dbContext.MusicGenres.Add(y4);
            }
            MusicGenre y5 = await dbContext.MusicGenres.Where(x => x.SystemName == "classical").FirstOrDefaultAsync();
            if (y5 == null)
            {
                y5 = new MusicGenre { Title = "Classical", SystemName = "classical", Excerpt = "Orchestral and instrumental music from the Western tradition." };
                dbContext.MusicGenres.Add(y5);
            }
            MusicGenre y6 = await dbContext.MusicGenres.Where(x => x.SystemName == "country").FirstOrDefaultAsync();
            if (y6 == null)
            {
                y6 = new MusicGenre { Title = "Country", SystemName = "country", Excerpt = "Folk music roots, storytelling lyrics." };
                dbContext.MusicGenres.Add(y6);
            }
            MusicGenre y7 = await dbContext.MusicGenres.Where(x => x.SystemName == "electronic").FirstOrDefaultAsync();
            if (y7 == null)
            {
                y7 = new MusicGenre { Title = "Electronic", SystemName = "electronic", Excerpt = "Synthesized sounds and beats, includes subgenres like EDM." };
                dbContext.MusicGenres.Add(y7);
            }
            MusicGenre y8 = await dbContext.MusicGenres.Where(x => x.SystemName == "rhythm-and-blues").FirstOrDefaultAsync();
            if (y8 == null)
            {
                y8 = new MusicGenre { Title = "R&B (Rhythm and Blues)", SystemName = "rhythm-and-blues", Excerpt = "Smooth vocals, groove-based music." };
                dbContext.MusicGenres.Add(y8);
            }
            MusicGenre y9 = await dbContext.MusicGenres.Where(x => x.SystemName == "reggae").FirstOrDefaultAsync();
            if (y9 == null)
            {
                y9 = new MusicGenre { Title = "Reggae", SystemName = "reggae", Excerpt = "Originating from Jamaica, features offbeat rhythms." };
                dbContext.MusicGenres.Add(y9);
            }
            await dbContext.SaveChangesAsync();
            MusicGenre y10 = await dbContext.MusicGenres.Where(x => x.SystemName == "blues").FirstOrDefaultAsync();
            if (y10 == null)
            {
                y10 = new MusicGenre { Title = "Blues", SystemName = "blues", Excerpt = "Expressive, soulful music with roots in African American communities." };
                dbContext.MusicGenres.Add(y10);
            }
            MusicGenre y11 = await dbContext.MusicGenres.Where(x => x.SystemName == "latin").FirstOrDefaultAsync();
            if (y11 == null)
            {
                y11 = new MusicGenre { Title = "Latin", SystemName = "latin", Excerpt = "Includes various styles like salsa, merengue, and reggaeton." };
                dbContext.MusicGenres.Add(y11);
            }
            MusicGenre y12 = await dbContext.MusicGenres.Where(x => x.SystemName == "metal").FirstOrDefaultAsync();
            if (y12 == null)
            {
                y12 = new MusicGenre { Title = "Metal", SystemName = "metal", Excerpt = "Heavy guitars, aggressive vocals, and powerful rhythms." };
                dbContext.MusicGenres.Add(y12);
            }
            MusicGenre y13 = await dbContext.MusicGenres.Where(x => x.SystemName == "folk").FirstOrDefaultAsync();
            if (y13 == null)
            {
                y13 = new MusicGenre { Title = "Folk", SystemName = "folk", Excerpt = "Traditional and acoustic, often storytelling." };
                dbContext.MusicGenres.Add(y13);
            }
            MusicGenre y14 = await dbContext.MusicGenres.Where(x => x.SystemName == "soul").FirstOrDefaultAsync();
            if (y14 == null)
            {
                y14 = new MusicGenre { Title = "Soul", SystemName = "soul", Excerpt = "Deeply emotional and expressive, combining R&B and gospel influences." };
                dbContext.MusicGenres.Add(y14);
            }
            MusicGenre y15 = await dbContext.MusicGenres.Where(x => x.SystemName == "punk").FirstOrDefaultAsync();
            if (y15 == null)
            {
                y15 = new MusicGenre { Title = "Punk", SystemName = "punk", Excerpt = "Fast, aggressive, and rebellious music." };
                dbContext.MusicGenres.Add(y15);
            }
            await dbContext.SaveChangesAsync();
        }
        public static async Task SetupEntertainmentGenresAsync(ApplicationDbContext dbContext)
        {
            EntertainmentGenre y1 = await dbContext.EntertainmentGenres.Where(x => x.SystemName == "action").FirstOrDefaultAsync();
            if (y1 == null)
            {
                y1 = new EntertainmentGenre { Title = "Action", SystemName = "action", Excerpt = "Action genre" };
                dbContext.EntertainmentGenres.Add(y1);
            }
            EntertainmentGenre y2 = await dbContext.EntertainmentGenres.Where(x => x.SystemName == "adventure").FirstOrDefaultAsync();
            if (y2 == null)
            {
                y2 = new EntertainmentGenre { Title = "Adventure", SystemName = "adventure", Excerpt = "Adventure genre" };
                dbContext.EntertainmentGenres.Add(y2);
            }

            EntertainmentGenre y3 = await dbContext.EntertainmentGenres.Where(x => x.SystemName == "comedy").FirstOrDefaultAsync();
            if (y3 == null)
            {
                y3 = new EntertainmentGenre { Title = "Comedy", SystemName = "comedy", Excerpt = "Comedy genre" };
                dbContext.EntertainmentGenres.Add(y3);
            }
            EntertainmentGenre y4 = await dbContext.EntertainmentGenres.Where(x => x.SystemName == "drama").FirstOrDefaultAsync();
            if (y4 == null)
            {
                y4 = new EntertainmentGenre { Title = "Drama", SystemName = "drama", Excerpt = "Drama genre" };
                dbContext.EntertainmentGenres.Add(y4);
            }
            EntertainmentGenre y5 = await dbContext.EntertainmentGenres.Where(x => x.SystemName == "fantasy").FirstOrDefaultAsync();
            if (y5 == null)
            {
                y5 = new EntertainmentGenre { Title = "Fantasy", SystemName = "fantasy", Excerpt = "Fantasy genre" };
                dbContext.EntertainmentGenres.Add(y5);
            }
            EntertainmentGenre y6 = await dbContext.EntertainmentGenres.Where(x => x.SystemName == "horror").FirstOrDefaultAsync();
            if (y6 == null)
            {
                y6 = new EntertainmentGenre { Title = "Horror", SystemName = "horror", Excerpt = "Horror genre" };
                dbContext.EntertainmentGenres.Add(y6);
            }
            EntertainmentGenre y7 = await dbContext.EntertainmentGenres.Where(x => x.SystemName == "mystery").FirstOrDefaultAsync();
            if (y7 == null)
            {
                y7 = new EntertainmentGenre { Title = "Mystery", SystemName = "mystery", Excerpt = "Mystery genre" };
                dbContext.EntertainmentGenres.Add(y7);
            }
            EntertainmentGenre y8 = await dbContext.EntertainmentGenres.Where(x => x.SystemName == "romance").FirstOrDefaultAsync();
            if (y8 == null)
            {
                y8 = new EntertainmentGenre { Title = "Romance", SystemName = "romance", Excerpt = "Romance genre" };
                dbContext.EntertainmentGenres.Add(y8);
            }
            EntertainmentGenre y9 = await dbContext.EntertainmentGenres.Where(x => x.SystemName == "science-fiction").FirstOrDefaultAsync();
            if (y9 == null)
            {
                y9 = new EntertainmentGenre { Title = "Science Fiction", SystemName = "science-fiction", Excerpt = "Science fiction genre" };
                dbContext.EntertainmentGenres.Add(y9);
            }
            EntertainmentGenre y10 = await dbContext.EntertainmentGenres.Where(x => x.SystemName == "thriller").FirstOrDefaultAsync();
            if (y10 == null)
            {
                y10 = new EntertainmentGenre { Title = "Thriller", SystemName = "thriller", Excerpt = "Thriller genre" };
                dbContext.EntertainmentGenres.Add(y10);
            }
            await dbContext.SaveChangesAsync();
        }

        public static async Task SetupProjectFundMeListingCategoriesAsync(ApplicationDbContext dbContext)
        {
            ProjectFundMeListingCategory y1 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "art").FirstOrDefaultAsync();
            if (y1 == null)
            {
                y1 = new ProjectFundMeListingCategory { Title = "Art", SystemName = "art", Excerpt = "Art" };
                dbContext.ProjectFundMeListingCategories.Add(y1);
            }
            ProjectFundMeListingCategory y2 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "ceramics").FirstOrDefaultAsync();
            if (y2 == null)
            {
                y2 = new ProjectFundMeListingCategory { Parent = y1, Title = "Ceramics", SystemName = "ceramics", Excerpt = "Ceramics" };
                dbContext.ProjectFundMeListingCategories.Add(y2);
            }
            ProjectFundMeListingCategory y3 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "conceptual-art").FirstOrDefaultAsync();
            if (y3 == null)
            {
                y3 = new ProjectFundMeListingCategory { Parent = y1, Title = "Conceptual Art", SystemName = "conceptual-art", Excerpt = "Conceptual Art" };
                dbContext.ProjectFundMeListingCategories.Add(y3);
            }
            ProjectFundMeListingCategory y4 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "digital-art").FirstOrDefaultAsync();
            if (y4 == null)
            {
                y4 = new ProjectFundMeListingCategory { Parent = y1, Title = "Digital Art", SystemName = "digital-art", Excerpt = "Digital Art" };
                dbContext.ProjectFundMeListingCategories.Add(y4);
            }
            ProjectFundMeListingCategory y5 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "illustration").FirstOrDefaultAsync();
            if (y5 == null)
            {
                y5 = new ProjectFundMeListingCategory { Parent = y1, Title = "Illustration", SystemName = "illustration", Excerpt = "Illustration" };
                dbContext.ProjectFundMeListingCategories.Add(y5);
            }
            ProjectFundMeListingCategory y6 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "installations").FirstOrDefaultAsync();
            if (y6 == null)
            {
                y6 = new ProjectFundMeListingCategory { Parent = y1, Title = "Installations", SystemName = "installations", Excerpt = "Installations" };
                dbContext.ProjectFundMeListingCategories.Add(y6);
            }
            ProjectFundMeListingCategory y7 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "mixed-media").FirstOrDefaultAsync();
            if (y7 == null)
            {
                y7 = new ProjectFundMeListingCategory { Parent = y1, Title = "Mixed Media", SystemName = "mixed-media", Excerpt = "Mixed Media" };
                dbContext.ProjectFundMeListingCategories.Add(y7);
            }
            ProjectFundMeListingCategory y8 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "painting").FirstOrDefaultAsync();
            if (y8 == null)
            {
                y8 = new ProjectFundMeListingCategory { Parent = y1, Title = "Painting", SystemName = "painting", Excerpt = "Painting" };
                dbContext.ProjectFundMeListingCategories.Add(y8);
            }
            ProjectFundMeListingCategory y9 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "performance-art").FirstOrDefaultAsync();
            if (y9 == null)
            {
                y9 = new ProjectFundMeListingCategory { Parent = y1, Title = "Performance Art", SystemName = "performance-art", Excerpt = "Performance Art" };
                dbContext.ProjectFundMeListingCategories.Add(y9);
            }
            ProjectFundMeListingCategory y10 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "public-art").FirstOrDefaultAsync();
            if (y10 == null)
            {
                y10 = new ProjectFundMeListingCategory { Parent = y1, Title = "Public Art", SystemName = "public-art", Excerpt = "Public Art" };
                dbContext.ProjectFundMeListingCategories.Add(y10);
            }
            ProjectFundMeListingCategory y11 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "sculpture").FirstOrDefaultAsync();
            if (y11 == null)
            {
                y11 = new ProjectFundMeListingCategory { Parent = y1, Title = "Sculpture", SystemName = "sculpture", Excerpt = "Sculpture" };
                dbContext.ProjectFundMeListingCategories.Add(y11);
            }
            ProjectFundMeListingCategory y12 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "social-practice").FirstOrDefaultAsync();
            if (y12 == null)
            {
                y12 = new ProjectFundMeListingCategory { Parent = y1, Title = "Social Practice", SystemName = "social-practice", Excerpt = "Social Practice" };
                dbContext.ProjectFundMeListingCategories.Add(y12);
            }
            ProjectFundMeListingCategory y13 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "textiles").FirstOrDefaultAsync();
            if (y13 == null)
            {
                y13 = new ProjectFundMeListingCategory { Parent = y1, Title = "Textiles", SystemName = "textiles", Excerpt = "Textiles" };
                dbContext.ProjectFundMeListingCategories.Add(y13);
            }
            ProjectFundMeListingCategory y14 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "video-art").FirstOrDefaultAsync();
            if (y14 == null)
            {
                y14 = new ProjectFundMeListingCategory { Parent = y1, Title = "Video Art", SystemName = "video-art", Excerpt = "Video Art" };
                dbContext.ProjectFundMeListingCategories.Add(y14);
            }



            ProjectFundMeListingCategory y15 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "comics").FirstOrDefaultAsync();
            if (y15 == null)
            {
                y15 = new ProjectFundMeListingCategory { Title = "Comics", SystemName = "comics", Excerpt = "Comics" };
                dbContext.ProjectFundMeListingCategories.Add(y15);
            }
            ProjectFundMeListingCategory y16 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "anthologies").FirstOrDefaultAsync();
            if (y16 == null)
            {
                y16 = new ProjectFundMeListingCategory { Parent = y15, Title = "Anthologies", SystemName = "anthologies", Excerpt = "Anthologies" };
                dbContext.ProjectFundMeListingCategories.Add(y16);
            }
            ProjectFundMeListingCategory y17 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "comic-books").FirstOrDefaultAsync();
            if (y17 == null)
            {
                y17 = new ProjectFundMeListingCategory { Parent = y15, Title = "Comic Books", SystemName = "comic-books", Excerpt = "Comic Books" };
                dbContext.ProjectFundMeListingCategories.Add(y17);
            }
            ProjectFundMeListingCategory y18 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "events").FirstOrDefaultAsync();
            if (y18 == null)
            {
                y18 = new ProjectFundMeListingCategory { Parent = y15, Title = "Events", SystemName = "events", Excerpt = "Events" };
                dbContext.ProjectFundMeListingCategories.Add(y18);
            }
            ProjectFundMeListingCategory y19 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "graphic-novels").FirstOrDefaultAsync();
            if (y19 == null)
            {
                y19 = new ProjectFundMeListingCategory { Parent = y15, Title = "Graphic Novels", SystemName = "graphic-novels", Excerpt = "Graphic Novels" };
                dbContext.ProjectFundMeListingCategories.Add(y19);
            }
            ProjectFundMeListingCategory y20 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "webcomics").FirstOrDefaultAsync();
            if (y20 == null)
            {
                y20 = new ProjectFundMeListingCategory { Parent = y15, Title = "Webcomics", SystemName = "webcomics", Excerpt = "Webcomics" };
                dbContext.ProjectFundMeListingCategories.Add(y20);
            }



            ProjectFundMeListingCategory p3 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "crafts").FirstOrDefaultAsync();
            if (p3 == null)
            {
                p3 = new ProjectFundMeListingCategory { Title = "Crafts", SystemName = "crafts", Excerpt = "Crafts" };
                dbContext.ProjectFundMeListingCategories.Add(p3);
            }
            List<string> ee = new List<string>() { "Candles", "Crochet", "DIY", "Embroidery", "Glass", "Knitting", "Pottery", "Printing", "Quilts", "Stationery", "Weaving", "Woodworking" };
            foreach (string x in ee)
            {
                // Replace multiple spaces
                string str = Regex.Replace(x, @"\s+", " ").Trim();
                str = Regex.Replace(str, " ", "-").Trim();
                string systemName = str.GenerateSlug();
                ProjectFundMeListingCategory eee = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
                if (eee == null)
                {
                    eee = new ProjectFundMeListingCategory { Parent= p3, Title = x, SystemName = systemName, Excerpt = x };
                    dbContext.ProjectFundMeListingCategories.Add(eee);
                }
            }



            ProjectFundMeListingCategory p4 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "dance").FirstOrDefaultAsync();
            if (p4 == null)
            {
                p4 = new ProjectFundMeListingCategory { Title = "Dance", SystemName = "dance", Excerpt = "Dance" };
                dbContext.ProjectFundMeListingCategories.Add(p4);
            }
            List<string> ee2 = new List<string>() { "Performances", "Spaces", "Workshops" };
            foreach (string x in ee2)
            {
                string systemName = x.GenerateSlug();
                ProjectFundMeListingCategory eee = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
                if (eee == null)
                {
                    eee = new ProjectFundMeListingCategory { Parent = p4, Title = x, SystemName = systemName, Excerpt = x };
                    dbContext.ProjectFundMeListingCategories.Add(eee);
                }
            }



            ProjectFundMeListingCategory p5 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "design").FirstOrDefaultAsync();
            if (p5 == null)
            {
                p5 = new ProjectFundMeListingCategory { Title = "Design", SystemName = "design", Excerpt = "Design" };
                dbContext.ProjectFundMeListingCategories.Add(p5);
            }
            List<string> ee3 = new List<string>() {
                "Architecture", "Civic Design", "Graphic Design", "Interactive Design", "Product Design", "Toys"
            };
            foreach (string x in ee3)
            {
                string systemName = x.GenerateSlug();
                ProjectFundMeListingCategory eee = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
                if (eee == null)
                {
                    eee = new ProjectFundMeListingCategory { Parent = p5, Title = x, SystemName = systemName, Excerpt = x };
                    dbContext.ProjectFundMeListingCategories.Add(eee);
                }
            }



            ProjectFundMeListingCategory p6 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "fashion").FirstOrDefaultAsync();
            if (p6 == null)
            {
                p6 = new ProjectFundMeListingCategory { Title = "Fashion", SystemName = "fashion", Excerpt = "Fashion" };
                dbContext.ProjectFundMeListingCategories.Add(p6);
            }
            List<string> ee4 = new List<string>() {
                "Accessories", "Apparel", "Childrenswear", "Couture", "Footwear", "Jewelry", "Pet Fashion", "Ready-to-wear"
            };
            foreach (string x in ee4)
            {
                string systemName = x.GenerateSlug();
                ProjectFundMeListingCategory eee = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
                if (eee == null)
                {
                    eee = new ProjectFundMeListingCategory { Parent = p6, Title = x, SystemName = systemName, Excerpt = x };
                    dbContext.ProjectFundMeListingCategories.Add(eee);
                }
            }



            ProjectFundMeListingCategory p7 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "Film").FirstOrDefaultAsync();
            if (p7 == null)
            {
                p7 = new ProjectFundMeListingCategory { Title = "Film", SystemName = "film", Excerpt = "Film" };
                dbContext.ProjectFundMeListingCategories.Add(p7);
            }
            List<string> ee5 = new List<string>() {
                "Action", "Animation", "Comedy", "Documentary", "Drama", "Experimental", "Family", "Fantasy", "Festivals", "Horror", "Movie Theaters", "Music Videos", "Narrative Film", "Romance", "Science Fiction", "Shorts", "Television", "Thrillers", "Webseries"
            };
            foreach (string x in ee5)
            {
                string systemName = x.GenerateSlug();
                ProjectFundMeListingCategory eee = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
                if (eee == null)
                {
                    eee = new ProjectFundMeListingCategory { Parent = p7, Title = x, SystemName = systemName, Excerpt = x };
                    dbContext.ProjectFundMeListingCategories.Add(eee);
                }
            }



            ProjectFundMeListingCategory p8 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "food").FirstOrDefaultAsync();
            if (p8 == null)
            {
                p8 = new ProjectFundMeListingCategory { Title = "Food", SystemName = "food", Excerpt = "Food" };
                dbContext.ProjectFundMeListingCategories.Add(p8);
            }
            List<string> ee6 = new List<string>() {
                "Community Gardens","Cookbooks","Drinks","Events","Farmer's Markets","Farms","Food Trucks","Restaurants","Small Batch","Spaces","Vegan"
            };
            foreach (string x in ee6)
            {
                string systemName = x.GenerateSlug();
                ProjectFundMeListingCategory eee = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
                if (eee == null)
                {
                    eee = new ProjectFundMeListingCategory { Parent = p8, Title = x, SystemName = systemName, Excerpt = x };
                    dbContext.ProjectFundMeListingCategories.Add(eee);
                }
            }



            ProjectFundMeListingCategory p9 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "games").FirstOrDefaultAsync();
            if (p9 == null)
            {
                p9 = new ProjectFundMeListingCategory { Title = "Games", SystemName = "games", Excerpt = "Games" };
                dbContext.ProjectFundMeListingCategories.Add(p9);
            }
            List<string> ee7 = new List<string>() {
                "Gaming Hardware","Live Games","Mobile Games","Playing Cards","Puzzles","Tabletop Games"
            };
            foreach (string x in ee7)
            {
                string systemName = x.GenerateSlug();
                ProjectFundMeListingCategory eee = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
                if (eee == null)
                {
                    eee = new ProjectFundMeListingCategory { Parent = p9, Title = x, SystemName = systemName, Excerpt = x };
                    dbContext.ProjectFundMeListingCategories.Add(eee);
                }
            }



            ProjectFundMeListingCategory p10 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "music").FirstOrDefaultAsync();
            if (p10 == null)
            {
                p10 = new ProjectFundMeListingCategory { Title = "Music", SystemName = "music", Excerpt = "Music" };
                dbContext.ProjectFundMeListingCategories.Add(p10);
            }
            List<string> ee8 = new List<string>() {
                "Blues","Classical Music","Comedy","Country & Folk","Electronic Music","Faith","Hip-Hop","Indie Rock","Jazz","Kids","Latin","Metal","Pop","Punk","R&B","Rock","World Music"
            };
            foreach (string x in ee8)
            {
                string systemName = x.GenerateSlug();
                ProjectFundMeListingCategory eee = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
                if (eee == null)
                {
                    eee = new ProjectFundMeListingCategory { Parent = p10, Title = x, SystemName = systemName, Excerpt = x };
                    dbContext.ProjectFundMeListingCategories.Add(eee);
                }
            }



            ProjectFundMeListingCategory p11 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "photography").FirstOrDefaultAsync();
            if (p11 == null)
            {
                p11 = new ProjectFundMeListingCategory { Title = "Photography", SystemName = "photography", Excerpt = "Photography" };
                dbContext.ProjectFundMeListingCategories.Add(p11);
            }
            List<string> ee9 = new List<string>() {
                "Animals","Fine Art","Nature","People","Photobooks","Places"
            };
            foreach (string x in ee9)
            {
                string systemName = x.GenerateSlug();
                ProjectFundMeListingCategory eee = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
                if (eee == null)
                {
                    eee = new ProjectFundMeListingCategory { Parent = p11, Title = x, SystemName = systemName, Excerpt = x };
                    dbContext.ProjectFundMeListingCategories.Add(eee);
                }
            }



            ProjectFundMeListingCategory p12 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "publishing").FirstOrDefaultAsync();
            if (p12 == null)
            {
                p12 = new ProjectFundMeListingCategory { Title = "Publishing", SystemName = "publishing", Excerpt = "Publishing" };
                dbContext.ProjectFundMeListingCategories.Add(p12);
            }
            List<string> ee10 = new List<string>() {
                "Academic","Anthologies","Art Books","Calendars","Children's Books","Comedy","Fiction","Letterpress","Literary Journals","Literary Spaces","Nonfiction","Periodicals","Poetry","Radio & Podcasts","Translations","Young Adult","Zines"
            };
            foreach (string x in ee10)
            {
                string systemName = x.GenerateSlug();
                ProjectFundMeListingCategory eee = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
                if (eee == null)
                {
                    eee = new ProjectFundMeListingCategory { Parent = p12, Title = x, SystemName = systemName, Excerpt = x };
                    dbContext.ProjectFundMeListingCategories.Add(eee);
                }
            }



            ProjectFundMeListingCategory p13 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "technology").FirstOrDefaultAsync();
            if (p13 == null)
            {
                p13 = new ProjectFundMeListingCategory { Title = "Technology", SystemName = "technology", Excerpt = "Technology" };
                dbContext.ProjectFundMeListingCategories.Add(p13);
            }
            List<string> ee11 = new List<string>() {
                "3D Printing","Apps","Camera Equipment","DIY Electronics","Fabrication Tools","Flight","Gadgets","Hardware","Makerspaces","Robots","Software","Sound","Space Exploration","Wearables","Web"
            };
            foreach (string x in ee11)
            {
                string systemName = x.GenerateSlug();
                ProjectFundMeListingCategory eee = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
                if (eee == null)
                {
                    eee = new ProjectFundMeListingCategory { Parent = p13, Title = x, SystemName = systemName, Excerpt = x };
                    dbContext.ProjectFundMeListingCategories.Add(eee);
                }
            }



            ProjectFundMeListingCategory p14 = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == "theater").FirstOrDefaultAsync();
            if (p14 == null)
            {
                p14 = new ProjectFundMeListingCategory { Title = "Theater", SystemName = "theater", Excerpt = "Theater" };
                dbContext.ProjectFundMeListingCategories.Add(p14);
            }
            List<string> ee12 = new List<string>() {
                "Comedy","Experimental","Festivals","Immersive","Musical","Plays"
            };
            foreach (string x in ee12)
            {
                string systemName = x.GenerateSlug();
                ProjectFundMeListingCategory eee = await dbContext.ProjectFundMeListingCategories.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
                if (eee == null)
                {
                    eee = new ProjectFundMeListingCategory { Parent = p14, Title = x, SystemName = systemName, Excerpt = x };
                    dbContext.ProjectFundMeListingCategories.Add(eee);
                }
            }



            await dbContext.SaveChangesAsync();
        }

    }
}
