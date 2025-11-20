using HenwoniDataModifierAPI.Data;
using HenwoniDataModifierAPI.Models.Common;
using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Models.Platform;
using HenwoniDataModifierAPI.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System;
using System.Reflection;
using System.Text.Json;
using HenwoniDataModifierAPI.Data.External.CountriesStatesCitiesDatabase;
using HenwoniDataModifierAPI.Models.Skills;
using HenwoniDataModifierAPI.Models.Services;
using HenwoniDataModifierAPI.Models.Organisation;
using HenwoniDataModifierAPI.Models.Employment;
using HenwoniDataModifierAPI.Models;
using HenwoniDataModifierAPI.Models.Networks;
using HenwoniDataModifierAPI.Models.Translator;

namespace HenwoniDataModifierAPI.Automatic
{
    public partial class AutomaticSetup : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _period = TimeSpan.FromSeconds(5);
        private bool _stopTask = false;
        ApplicationDbContext dbContext;
        private readonly IConfiguration _config;
        public Language DefaultLanguage { get; set; }

        public AutomaticSetup (IServiceProvider serviceProvider, IConfiguration config)
        {
            _serviceProvider = serviceProvider;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            await using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await SetupTransilationsAsync(context);
            await SetupSiteMetaAsync(context);
            await SetupLanguagesAsync(context);
            DefaultLanguage = await context.Languages.Where(x => x.SystemName == Constants.DefaultLanguage).FirstOrDefaultAsync();
            await SetupMyNetworkCategoriesAsync(context);
            await SetupJobTitlesAsync(context);
            await SetupOtherEntitiesAsync(context);
            await SetupLocationsAsync(context);
            await SetupSkillsAsync(context);
            await SetupServiceCategoriesAsync(context);
            await SetupJobIndustriesAsync(context);
            await SetupCandidateRoleAsync(context);
        }
        public static async Task SetupCandidateRoleAsync(ApplicationDbContext dbContext)
        {
            CandidateRole y1 = await dbContext.CandidateRoles.Where(x => x.SystemName == "corporation").FirstOrDefaultAsync();
            if (y1 == null)
            {
                y1 = new CandidateRole { Title = "Chief Executive Officer", SystemName = "CEO", Abbr = "CEO", Excerpt = "" };
                dbContext.CandidateRoles.Add(y1);
            }
            CandidateRole y2 = await dbContext.CandidateRoles.Where(x => x.SystemName == "CF0").FirstOrDefaultAsync();
            if (y2 == null)
            {
                y2 = new CandidateRole { Title = "Chief Financial Officer ", SystemName = "CEO", Abbr = "CEO", Excerpt = "" };
                dbContext.CandidateRoles.Add(y2);
            }
            CandidateRole y3 = await dbContext.CandidateRoles.Where(x => x.SystemName == "CCO").FirstOrDefaultAsync();
            if (y3 == null)
            {
                y3 = new CandidateRole { Title = "Chief Commercial Officer", SystemName = "CCO", Abbr = "CCO", Excerpt = "" };
                dbContext.CandidateRoles.Add(y3);
            }
            CandidateRole y4 = await dbContext.CandidateRoles.Where(x => x.SystemName == "managing-consultant").FirstOrDefaultAsync();
            if (y4 == null)
            {
                y4 = new CandidateRole { Title = "Managing Consultant", SystemName = "managing-consultant", Excerpt = "" };
                dbContext.CandidateRoles.Add(y4);
            }
        }

        public async Task SetupJobTitlesAsync(ApplicationDbContext dbContext)
        {
            await LanguagesFix1Async(dbContext);
            // await SetupJobTitlesUsingJobTitlesTextAsync();
            var assembly = Assembly.GetExecutingAssembly();
            var t = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            using (Stream stream = assembly.GetManifestResourceStream("HenwoniDataModifierAPI.Data.jobtitles.json"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string txt = "";
                    while (!reader.EndOfStream)
                    {
                        txt += reader.ReadLine() + "\n";
                    }
                    List<RefCommonJobTitle> jobTitles = JsonSerializer.Deserialize<List<RefCommonJobTitle>>(txt);
                    foreach (var jb in jobTitles)
                    {
                        var existing = await dbContext.RefCommonJobTitles.Where(x => x.SystemName == jb.SystemName).FirstOrDefaultAsync();
                        if (existing==null)
                        {
                            existing = new RefCommonJobTitle();
                            existing.CopyPropertiesFrom(jb);
                            dbContext.RefCommonJobTitles.Add(existing);
                            existing.Description = jb.Title;
                            existing.DateCreated = DateTime.UtcNow;
                            existing.PluralTitle = jb.Title + "s";
                        }
                        jb.DateUpdated = DateTime.UtcNow;
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        
        public class TransilationTEmp
        {
            public string name { get; set; }
            public string value { get; set; }
        }
        public class SiteMetaTEmp
        {
            public string name { get; set; }
            public string value { get; set; }
        }

        public static async Task SetupSiteMetaAsync(ApplicationDbContext dbContext)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var t = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            using (Stream stream = assembly.GetManifestResourceStream("HenwoniDataModifierAPI.Data.SiteMeta.json"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string txt = "";
                    while (!reader.EndOfStream)
                    {
                        txt += reader.ReadLine() + "\n";
                    }
                    List<SiteMetaTEmp> r = JsonSerializer.Deserialize<List<SiteMetaTEmp>>(txt);
                    foreach (var jb in r)
                    {
                        var existing = await dbContext.SiteMeta.Where(x => x.SystemName == jb.name).FirstOrDefaultAsync();
                        if (existing == null)
                        {
                            existing = new SiteMeta()
                            {
                                Value = jb.value,
                                SystemName = jb.name
                            };
                            dbContext.SiteMeta.Add(existing);
                            existing.Language = await dbContext.Languages.Where(x => x.SystemName == HenwoniDataModifierAPI.Utilities.Constants.DefaultLanguage).FirstOrDefaultAsync();
                        }
                        existing.Value = jb.value;
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public static async Task SetupTransilationsAsync(ApplicationDbContext dbContext)
        {
            // await SetupJobTitlesUsingTextAsync();
            var assembly = Assembly.GetExecutingAssembly();
            var t = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            using (Stream stream = assembly.GetManifestResourceStream("HenwoniDataModifierAPI.Data.Transilations.Pages.json"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string txt = "";
                    while (!reader.EndOfStream)
                    {
                        txt += reader.ReadLine() + "\n";
                    }
                    List<TransilationTEmp> r = JsonSerializer.Deserialize<List<TransilationTEmp>>(txt);
                    foreach (var jb in r)
                    {
                        var existing = await dbContext.Translations.Where(x => x.SystemContextIdentity == jb.name).FirstOrDefaultAsync();
                        if (existing == null)
                        {
                            existing = new HenwoniDataModifierAPI.Models.Location.Translation()
                            {
                                Text = jb.value,
                                SystemContextIdentity = jb.name,
                                SystemName = jb.name,
                                DefaultLanguageText = jb.value,
                            };
                            dbContext.Translations.Add(existing);
                            existing.SystemContextIdentity = jb.name;
                            existing.SystemName = jb.name;
                            existing.Language = await dbContext.Languages.Where(x => x.SystemName == HenwoniDataModifierAPI.Utilities.Constants.DefaultLanguage).FirstOrDefaultAsync();
                        }
                        existing.Text = jb.value;
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }


        private async Task LanguagesFix1Async(ApplicationDbContext dbContext)
        {
            foreach (var c in await dbContext.RefCommonJobTitles.Where(x => x.Language == null).ToListAsync())
            {
                c.Language = await dbContext.Languages.Where(x=>x.SystemName== Constants.DefaultLanguage).FirstOrDefaultAsync();
            }
            await dbContext.SaveChangesAsync();
        }

        private async Task SetupJobTitlesUsingJobTitlesTextAsync()
        {

            var assembly = Assembly.GetExecutingAssembly();
            var t = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            using (Stream stream = assembly.GetManifestResourceStream("HenwoniDataModifierAPI.Data.jobtitlestext.txt"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    List<string> titles = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        string title = reader.ReadLine();
                        if (!String.IsNullOrEmpty(title))
                        {
                            titles.Add(title.Trim());
                        }
                    }
                    if (await dbContext.RefCommonJobTitles.CountAsync() < titles.Count)
                    {
                        foreach (string title in titles)
                        {
                            if (await dbContext.RefCommonJobTitles.AnyAsync(x => x.Title == title)) continue;
                            string b = title.Trim();
                            string systemName = title.GenerateSlug();
                            int f = 1;
                            while (await dbContext.RefCommonJobTitles.Where(x => x.SystemName == systemName).AnyAsync(x => x.SystemName == systemName))
                            {
                                systemName = systemName + f;
                                f++;
                            }
                            if (await dbContext.RefCommonJobTitles.Where(x => x.SystemName == systemName).FirstOrDefaultAsync() == null)
                            {
                                RefCommonJobTitle jobTitle = new RefCommonJobTitle();
                                jobTitle.SystemName = systemName;
                                jobTitle.Title = title;
                                jobTitle.Description = title;
                                jobTitle.DateUpdated = DateTime.UtcNow;
                                jobTitle.DateCreated = DateTime.UtcNow;
                                jobTitle.PluralTitle = title + "s";
                                dbContext.RefCommonJobTitles.Add(jobTitle);
                            }
                            await dbContext.SaveChangesAsync();
                        }
                    }
                }
            }

        }

        public async Task SetupLocationsAsync(ApplicationDbContext dbContext)
        {
            Continent y0 = await dbContext.Continents.Where(x => x.SystemName == "Anywhere").FirstOrDefaultAsync();
            if (y0 == null)
            {
                y0 = new Continent { Name = "Anywhere", SystemName = "Anywhere" };
                dbContext.Continents.Add(y0);
            }
            ContinentRegion y1 = await dbContext.ContinentRegions.Where(x => x.SystemName == "Anywhere").FirstOrDefaultAsync();
            if (y1 == null)
            {
                y1 = new ContinentRegion { Name = "Anywhere", SystemName = "Anywhere" };
                dbContext.ContinentRegions.Add(y1);
            }
            Country y2 = await dbContext.Countries.Where(x => x.SystemName == "Anywhere").FirstOrDefaultAsync();
            if (y2 == null)
            {
                y2 = new Country { Name = "Anywhere", SystemName = "Anywhere", ISO2 = "Anywhere", ISO3 = "Anywhere", Latitude = "0", Longitude = "0", Nationality = "Any" };
                Models.Pricing.Currency currency = await dbContext.Currencies.Where(c => c.CurrencyName == "USD").FirstOrDefaultAsync();
                y2.DefaultCurrency = currency;
                y2.DefaultCurrencyId = currency.Id;
                dbContext.Countries.Add(y2);
            }
            Models.Location.State y3 = await dbContext.States.Where(x => x.SystemName == "Anywhere").FirstOrDefaultAsync();
            if (y3 == null)
            {
                y3 = new Models.Location.State { Name = "Anywhere", SystemName = "Anywhere" };
                dbContext.States.Add(y3);
            }
            Models.Location.City y4 = await dbContext.Cities.Where(x => x.SystemName == "Anywhere").FirstOrDefaultAsync();
            if (y4 == null)
            {
                y4 = new Models.Location.City { Name = "Anywhere", SystemName = "Anywhere", Latitude = "Anywhere", Longitude = "Anywhere" };
                dbContext.Cities.Add(y4);
            }
            var assembly = Assembly.GetExecutingAssembly();
            var t = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            using (Stream stream = assembly.GetManifestResourceStream("HenwoniDataModifierAPI.Data.countries+states+cities.json"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string fileContent = reader.ReadToEnd();
                    List<Data.External.CountriesStatesCitiesDatabase.CSCDCountry> jsonResponse = JsonSerializer.Deserialize<List<Data.External.CountriesStatesCitiesDatabase.CSCDCountry>>(fileContent);
                    if (jsonResponse != null)
                    {
                        int c = 0;
                        foreach (Data.External.CountriesStatesCitiesDatabase.CSCDCountry cscdCountry in jsonResponse)
                        {
                            Country country = await dbContext.Countries.Where(x => x.ISO3 == cscdCountry.ISO3).FirstOrDefaultAsync();
                            if (country == null)
                            {
                                country = new Country();
                                country.CopyPropertiesFrom(cscdCountry);
                                if (!String.IsNullOrEmpty(cscdCountry.Currency))
                                {
                                    Models.Pricing.Currency currency = await dbContext.Currencies.Where(c => c.CurrencyName == cscdCountry.Currency).FirstOrDefaultAsync();
                                    if (currency == null)
                                    {
                                        // Create it.
                                        currency = new Models.Pricing.Currency();
                                        currency.CurrencyName = cscdCountry.Currency;
                                        currency.CurrencySymbol = cscdCountry.CurrencySymbol;
                                        dbContext.Currencies.Add(currency);
                                    }
                                    country.DefaultCurrency = currency;
                                    country.DefaultCurrencyId = currency.Id;
                                }
                                country.SystemName = cscdCountry.Name.GenerateSlug();
                                country.Id = 0;
                                dbContext.Countries.Add(country);
                                CountryTranslations countryTranslations = await dbContext.CountryTranslations.Where(x => x.Country == country).FirstOrDefaultAsync();
                                if (countryTranslations == null)
                                {
                                    countryTranslations = new CountryTranslations();
                                    countryTranslations.CopyPropertiesFrom(cscdCountry.Translations);
                                    countryTranslations.Country = country;
                                    dbContext.CountryTranslations.Add(countryTranslations);
                                }
                                if (cscdCountry.TimeZones != null && cscdCountry.TimeZones.Length > 0)
                                {
                                    foreach (Timezone tz in cscdCountry.TimeZones)
                                    {
                                        CountryTimeZone timeZone = await dbContext.CountryTimeZones.Where(x => x.ZoneName == tz.ZoneName).FirstOrDefaultAsync();
                                        if (timeZone == null)
                                        {
                                            timeZone = new CountryTimeZone();
                                            timeZone.Countries.Add(country);
                                            timeZone.CopyPropertiesFrom(tz);
                                            timeZone.Id = 0;
                                            dbContext.CountryTimeZones.Add(timeZone);
                                            country.TimeZones.Add(timeZone);
                                        }
                                        else
                                        {
                                            timeZone.Countries.Add(country);
                                        }
                                    }
                                }
                                //await dbContext.SaveChangesAsync();
                            }
                            Continent continent = null;
                            ContinentRegion continentRegion = null;
                            if (!String.IsNullOrEmpty(cscdCountry.Region))
                            {
                                continent = await dbContext.Continents.Where(x => x.Name == cscdCountry.Region).FirstOrDefaultAsync();
                                if (continent == null)
                                {
                                    continent = new Continent();
                                    continent.Name = cscdCountry.Region;
                                    continent.SystemName = cscdCountry.Region.GenerateSlug().ToLower();
                                    dbContext.Continents.Add(continent);
                                }
                                continentRegion = await dbContext.ContinentRegions.Where(x => x.Name == cscdCountry.Subregion).FirstOrDefaultAsync();
                                if (continentRegion == null)
                                {
                                    continentRegion = new ContinentRegion();
                                    continentRegion.Name = cscdCountry.Subregion;
                                    continentRegion.SystemName = cscdCountry.Subregion.GenerateSlug().ToLower();
                                    continentRegion.Continent = continent;
                                    dbContext.ContinentRegions.Add(continentRegion);
                                }
                                country.ContinentRegion = continentRegion;
                                country.Continent = continent;
                                // country.ContinentId = continent.Id;
                                // await dbContext.SaveChangesAsync();
                            }
                            if (cscdCountry.States != null && cscdCountry.States.Length > 0)
                            {
                                foreach (Data.External.CountriesStatesCitiesDatabase.State cscdState in cscdCountry.States)
                                {
                                    Models.Location.State state = await dbContext.States.Where(x => x.Name == cscdState.Name).FirstOrDefaultAsync();
                                    if (state == null)
                                    {
                                        state = new Models.Location.State();
                                        state.CopyPropertiesFrom(cscdState);
                                        state.Id = 0;
                                        state.SystemName = cscdState.Name.GenerateSlug().ToLower();
                                        state.Country = country;
                                        state.Continent = continent;
                                        state.ContinentRegion = continentRegion;
                                        dbContext.States.Add(state);
                                    }
                                    if (cscdState.Cities != null && cscdState.Cities.Length > 0)
                                    {
                                        foreach (Data.External.CountriesStatesCitiesDatabase.City cscdCity in cscdState.Cities)
                                        {
                                            Models.Location.City city = await dbContext.Cities.Where(x => x.Name == cscdCity.Name).FirstOrDefaultAsync();
                                            if (city == null)
                                            {
                                                city = new Models.Location.City();
                                                city.CopyPropertiesFrom(cscdCity);
                                                city.Id = 0;
                                                city.State = state;
                                                city.SystemName = cscdCity.Name.GenerateSlug().ToLower();
                                                city.Country = country;
                                                city.Continent = continent;
                                                city.ContinentRegion = continentRegion;
                                                dbContext.Cities.Add(city);
                                            }
                                        }
                                        await dbContext.SaveChangesAsync();
                                    }
                                }

                            }
                            await dbContext.SaveChangesAsync();
                        }
                    }
                }
            }
            Debug.WriteLine("setupLocationsAsync THE END!");
        }

        public async Task SetupSkillsAsync(ApplicationDbContext dbContext)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var t = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            using (Stream stream = assembly.GetManifestResourceStream("HenwoniDataModifierAPI.Data.skills.txt"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string skillTitle = line.Trim();
                        CandidateSkill dbSkill = await dbContext.CandidateSkills.Where(x => x.SourceTitle == skillTitle).FirstOrDefaultAsync();
                        if (dbSkill == null)
                        {
                            // System.Diagnostics.Debug.WriteLine("Adding " + skillTitle);
                            CandidateSkill s = new CandidateSkill();
                            s.Title = skillTitle;
                            s.SourceTitle = skillTitle;
                            s.Excerpt = skillTitle;
                            s.Content = skillTitle;
                            s.SystemName = skillTitle.ToLower().GenerateSlug();
                            dbContext.CandidateSkills.Add(s);
                        }
                    }
                }
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task SetupEducationTrainingAsync(ApplicationDbContext dbContext)
        {
            MyNetworkCategory d0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "education-training").FirstOrDefaultAsync();
            if (d0 == null)
            {
                d0 = new MyNetworkCategory { Title = "Education & Training", Language = DefaultLanguage, SystemName = "education-training", Excerpt = "Dedicated to learning, teaching, and knowledge transfer. Encompasses formal education and lifelong learning." };
                dbContext.MyNetworkCategories.Add(d0);
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "teaching-pedagogy").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Teaching & Pedagogy", Language = DefaultLanguage, SystemName = "teaching-pedagogy", Excerpt = "Instructional methods and classroom management." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "curriculum-development").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Curriculum Development", Language = DefaultLanguage, SystemName = "curriculum-development", Excerpt = "Designing educational content and standards." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "educational-technology").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Educational Technology", Language = DefaultLanguage, SystemName = "educational-technology", Excerpt = "Tools and platforms for learning." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "e-learning").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "E-learning", Language = DefaultLanguage, SystemName = "e-learning", Excerpt = "Online courses and digital instruction." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "special-education").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Special Education", Language = DefaultLanguage, SystemName = "special-education", Excerpt = "Supporting diverse learning needs." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "academic-research").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Academic Research", Language = DefaultLanguage, SystemName = "academic-research", Excerpt = "Scholarly investigation and publication." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "instructional-design").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Instructional Design", Language = DefaultLanguage, SystemName = "instructional-design", Excerpt = "Structuring effective learning experiences." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
        }
        public async Task SetupEnvironmentalSustainabilityAsync(ApplicationDbContext dbContext)
        {
            MyNetworkCategory d0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "environmental-sustainability").FirstOrDefaultAsync();
            if (d0 == null)
            {
                d0 = new MyNetworkCategory { Title = "Environmental & Sustainability", Language = DefaultLanguage, SystemName = "environmental-sustainability", Excerpt = "Dedicated to protecting the planet and promoting responsible practices. Combines science, policy, and innovation." };
                dbContext.MyNetworkCategories.Add(d0);
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "climate-science").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Climate Science", Language = DefaultLanguage, SystemName = "climate-science", Excerpt = "Studying climate change and mitigation." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "renewable-energy").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Renewable Energy", Language = DefaultLanguage, SystemName = "renewable-energy", Excerpt = "Solar, wind, and sustainable power." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "conservation").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Conservation", Language = DefaultLanguage, SystemName = "conservation", Excerpt = "Wildlife protection and habitat preservation." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "urban-planning").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Urban Planning", Language = DefaultLanguage, SystemName = "urban-planning", Excerpt = "Designing sustainable cities and infrastructure." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "sustainable-development").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Sustainable Development", Language = DefaultLanguage, SystemName = "sustainable-development", Excerpt = "Balancing growth with environmental care." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "agriculture-food-systems").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Agriculture & Food Systems", Language = DefaultLanguage, SystemName = "agriculture-food-systems", Excerpt = "Farming, food security, and innovation." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
        }
        public async Task SetupLegalGovernmentAsync(ApplicationDbContext dbContext)
        {
            MyNetworkCategory d0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "legal-government").FirstOrDefaultAsync();
            if (d0 == null)
            {
                d0 = new MyNetworkCategory { Title = "Legal & Government", Language = DefaultLanguage, SystemName = "legal-government", Excerpt = "Focuses on law, policy, and public service. Involves regulation, justice, and civic responsibility." };
                dbContext.MyNetworkCategories.Add(d0);
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "law-Legal-studies").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Law & Legal Studies", Language = DefaultLanguage, SystemName = "law-Legal-studies", Excerpt = "Legal systems, contracts, and advocacy." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "public-policy").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Public Policy", Language = DefaultLanguage, SystemName = "public-policy", Excerpt = "Government decisions and societal impact." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "criminal-justice").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Criminal Justice", Language = DefaultLanguage, SystemName = "criminal-justice", Excerpt = "Law enforcement and corrections." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "international-relations").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "International Relations", Language = DefaultLanguage, SystemName = "international-relations", Excerpt = "Diplomacy and global affairs." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "government-civil-service").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Government & Civil Service", Language = DefaultLanguage, SystemName = "government-civil-service", Excerpt = "Public administration and policy." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "military-defense").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Military & Defense", Language = DefaultLanguage, SystemName = "military-defense", Excerpt = "National security and strategic operations." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
        }
        public async Task SetupEntertainmentMediaAsync(ApplicationDbContext dbContext)
        {
            MyNetworkCategory d0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "entertainment-media").FirstOrDefaultAsync();
            if (d0 == null)
            {
                d0 = new MyNetworkCategory { Title = "Entertainment & Media", Language = DefaultLanguage, SystemName = "entertainment-media", Excerpt = "Engages audiences through storytelling, performance, and interaction. Combines creativity with technology.\r\n" };
                dbContext.MyNetworkCategories.Add(d0);
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "hospitality").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Television & Broadcasting", Language = DefaultLanguage, SystemName = "television-broadcasting", Excerpt = "News, shows, and live events." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "music-industry").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Music Industry", Language = DefaultLanguage, SystemName = "music-industry", Excerpt = "Including production, performance, and distribution." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "music-industry").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Gaming & Esports", Language = DefaultLanguage, SystemName = "gaming-esports", Excerpt = "Competitive play and game development." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "social-media").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Social Media", Language = DefaultLanguage, SystemName = "social-media", Excerpt = "Content creation and digital influence." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "journalism").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Journalism", Language = DefaultLanguage, SystemName = "journalism", Excerpt = "Reporting, writing, and investigative work." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "podcasting").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Podcasting", Language = DefaultLanguage, SystemName = "podcasting", Excerpt = "Audio storytelling and commentary." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "influencer-marketing").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Influencer Marketing", Language = DefaultLanguage, SystemName = "influencer-marketing", Excerpt = "Brand partnerships and audience engagement." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "event-planning").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Event Planning", Language = DefaultLanguage, SystemName = "event-planning", Excerpt = "Organizing experiences and gatherings." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
        }

        public async Task SetupTravelLifestyleAsync(ApplicationDbContext dbContext)
        {
            MyNetworkCategory d0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "travel-lifestyle").FirstOrDefaultAsync();
            if (d0 == null)
            {
                d0 = new MyNetworkCategory { Title = "Travel & Lifestyle", Language = DefaultLanguage, SystemName = "travel-lifestyle", Excerpt = "Focuses on personal enrichment, leisure, and well-being. Often intersects with hospitality and consumer trends." };
                dbContext.MyNetworkCategories.Add(d0);
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "hospitality").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Hospitality", Language = DefaultLanguage, SystemName = "hospitality", Excerpt = "Hotels, customer service, and guest experiences." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "tourism").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Tourism", Language = DefaultLanguage, SystemName = "tourism", Excerpt = "Travel planning and destination management." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "culinary-arts").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Culinary Arts", Language = DefaultLanguage, SystemName = "culinary-arts", Excerpt = "Cooking, baking, and food presentation." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "wellness-fitness").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Wellness & Fitness", Language = DefaultLanguage, SystemName = "wellness-fitness", Excerpt = "Physical health and mental well-being." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "fashion-beauty").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Fashion & Beauty", Language = DefaultLanguage, SystemName = "fashion-beauty", Excerpt = "Style, grooming, and self-expression." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "home-garden").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Home & Garden", Language = DefaultLanguage, SystemName = "home-garden", Excerpt = "Interior styling and landscaping." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "personal-development").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Personal Development", Language = DefaultLanguage, SystemName = "personal-development", Excerpt = "Self-improvement and goal setting." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "hobbies-crafts").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Hobbies & Crafts", Language = DefaultLanguage, SystemName = "hobbies-crafts", Excerpt = "DIY, collecting, and creative pastimes." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
        }
        public async Task SocialCulturalAsync(ApplicationDbContext dbContext)
        {
            MyNetworkCategory d0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "social-cultural").FirstOrDefaultAsync();
            if (d0 == null)
            {
                d0 = new MyNetworkCategory { Title = "Social & Cultural", Language = DefaultLanguage, SystemName = "social-cultural", Excerpt = "Explores human behavior, beliefs, and societal structures. Often involves critical thinking and cultural awareness." };
                dbContext.MyNetworkCategories.Add(d0);
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "sociology").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Sociology", Language = DefaultLanguage, SystemName = "sociology", Excerpt = "Social systems, institutions, and relationships." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "psychology").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Psychology", Language = DefaultLanguage, SystemName = "psychology", Excerpt = "Mental processes and behavior." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "anthropology").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Anthropology", Language = DefaultLanguage, SystemName = "anthropology", Excerpt = "Human evolution, culture, and traditions." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "history").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "History", Language = DefaultLanguage, SystemName = "history", Excerpt = "Past events and their impact." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "political-science").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Political Science", Language = DefaultLanguage, SystemName = "political-science", Excerpt = "Governance, policy, and political theory." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "philosophy").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Philosophy", Language = DefaultLanguage, SystemName = "philosophy", Excerpt = "Ethics, logic, and existential questions." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "religion-theology").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Religion & Theology", Language = DefaultLanguage, SystemName = "religion-theology", Excerpt = "Belief systems and spiritual practices." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "languages-linguistics").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Languages & Linguistics", Language = DefaultLanguage, SystemName = "languages-linguistics", Excerpt = "Communication, grammar, and translation." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "cultural-studies").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Cultural Studies", Language = DefaultLanguage, SystemName = "cultural-studies", Excerpt = "Identity, media, and global perspectives." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "ethics").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Ethics", Language = DefaultLanguage, SystemName = "ethics", Excerpt = "Moral reasoning and decision-making." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
        }
        public async Task SetupBusinessProfessionalAsync(ApplicationDbContext dbContext)
        {
            MyNetworkCategory d0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "business-professional").FirstOrDefaultAsync();
            if (d0 == null)
            {
                d0 = new MyNetworkCategory { Title = "Business & Professional", Language = DefaultLanguage, SystemName = "business-professional", Excerpt = "Focuses on strategy, operations, and organizational success. Involves leadership, financial management, and market dynamics" };
                dbContext.MyNetworkCategories.Add(d0);
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "marketing").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Marketing", Language = DefaultLanguage, SystemName = "marketing", Excerpt = "Promoting products and understanding consumer behavior." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "finance").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Finance", Language = DefaultLanguage, SystemName = "finance", Excerpt = "Managing money, investments, and risk" };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "accounting").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Accounting", Language = DefaultLanguage, SystemName = "accounting", Excerpt = "Tracking financial transactions and compliance." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
            }
            {
                MyNetworkCategory e1 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "human-resources").FirstOrDefaultAsync();
                if (e1 == null)
                {
                    e1 = new MyNetworkCategory { Parent = d0, Title = "Human Resources", Language = DefaultLanguage, SystemName = "human-resources", Excerpt = "Talent acquisition, development, and culture." };
                    dbContext.MyNetworkCategories.Add(e1);
                }
            }
            {
                MyNetworkCategory e1 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "management-leadership").FirstOrDefaultAsync();
                if (e1 == null)
                {
                    e1 = new MyNetworkCategory { Parent = d0, Title = "Management & Leadership", Language = DefaultLanguage, SystemName = "management-leadership", Excerpt = "Strategic planning and team coordination." };
                    dbContext.MyNetworkCategories.Add(e1);
                }
            }
            {
                MyNetworkCategory e1 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "entrepreneurship").FirstOrDefaultAsync();
                if (e1 == null)
                {
                    e1 = new MyNetworkCategory { Parent = d0, Title = "Entrepreneurship", Language = DefaultLanguage, SystemName = "entrepreneurship", Excerpt = "Building and scaling businesses." };
                    dbContext.MyNetworkCategories.Add(e1);
                }
            }
            {
                MyNetworkCategory e1 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "economics").FirstOrDefaultAsync();
                if (e1 == null)
                {
                    e1 = new MyNetworkCategory { Parent = d0, Title = "Economics", Language = DefaultLanguage, SystemName = "economics", Excerpt = "Market forces, policy, and resource allocation." };
                    dbContext.MyNetworkCategories.Add(e1);
                }
            }
            {
                MyNetworkCategory e1 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "real-estate").FirstOrDefaultAsync();
                if (e1 == null)
                {
                    e1 = new MyNetworkCategory { Parent = d0, Title = "Real Estate", Language = DefaultLanguage, SystemName = "real-estate", Excerpt = "Property development, sales, and valuation." };
                    dbContext.MyNetworkCategories.Add(e1);
                }
            }
            {
                MyNetworkCategory e1 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "sales").FirstOrDefaultAsync();
                if (e1 == null)
                {
                    e1 = new MyNetworkCategory { Parent = d0, Title = "Sales", Language = DefaultLanguage, SystemName = "sales", Excerpt = "Customer engagement and revenue generation." };
                    dbContext.MyNetworkCategories.Add(e1);
                }
            }
            {
                MyNetworkCategory e1 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "operations-logistics").FirstOrDefaultAsync();
                if (e1 == null)
                {
                    e1 = new MyNetworkCategory { Parent = d0, Title = "Operations & Logistics", Language = DefaultLanguage, SystemName = "operations-logistics", Excerpt = "Supply chain and process optimization." };
                    dbContext.MyNetworkCategories.Add(e1);
                }
            }
        }
        public async Task SetupScientificMedicalAsync(ApplicationDbContext dbContext)
        {
            MyNetworkCategory d0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "scientific-medical").FirstOrDefaultAsync();
            if (d0 == null)
            {
                d0 = new MyNetworkCategory { Title = "Scientific & Medical", Language = DefaultLanguage, SystemName = "scientific-medical", Excerpt = "Driven by inquiry, experimentation, and evidence. Focuses on understanding natural phenomena and improving health." };
                dbContext.MyNetworkCategories.Add(d0);
            }
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "biology").FirstOrDefaultAsync();
                if (e0 == null)
                {
                    e0 = new MyNetworkCategory { Parent = d0, Title = "Biology", Language = DefaultLanguage, SystemName = "biology", Excerpt = "Study of living organisms and ecosystems." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
                MyNetworkCategory e1 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "chemistry").FirstOrDefaultAsync();
                if (e1 == null)
                {
                    e1 = new MyNetworkCategory { Parent = d0, Title = "Chemistry", Language = DefaultLanguage, SystemName = "chemistry", Excerpt = "Composition, reactions, and properties of matter." };
                    dbContext.MyNetworkCategories.Add(e1);
                }
                MyNetworkCategory e2 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "physics").FirstOrDefaultAsync();
                if (e2 == null)
                {
                    e2 = new MyNetworkCategory { Parent = d0, Title = "Physics", Language = DefaultLanguage, SystemName = "physics", Excerpt = "Forces, energy, and the structure of the universe." };
                    dbContext.MyNetworkCategories.Add(e2);
                }
                MyNetworkCategory e3 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "physics").FirstOrDefaultAsync();
                if (e3 == null)
                {
                    e3 = new MyNetworkCategory { Parent = d0, Title = "Medicine & Healthcare", Language = DefaultLanguage, SystemName = "medicine-healthcare", Excerpt = "Diagnosis, treatment, and patient care." };
                    dbContext.MyNetworkCategories.Add(e3);
                }
                MyNetworkCategory e4 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "environmental-science").FirstOrDefaultAsync();
                if (e4 == null)
                {
                    e4 = new MyNetworkCategory { Parent = d0, Title = "Environmental Science", Language = DefaultLanguage, SystemName = "environmental-science", Excerpt = "Ecosystems, sustainability, and climate." };
                    dbContext.MyNetworkCategories.Add(e4);
                }
                MyNetworkCategory e5 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "genetics").FirstOrDefaultAsync();
                if (e5 == null)
                {
                    e5 = new MyNetworkCategory { Parent = d0, Title = "Genetics", Language = DefaultLanguage, SystemName = "genetics", Excerpt = "Heredity, DNA, and molecular biology." };
                    dbContext.MyNetworkCategories.Add(e5);
                }
                MyNetworkCategory e6 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "pharmacology").FirstOrDefaultAsync();
                if (e6 == null)
                {
                    e6 = new MyNetworkCategory { Parent = d0, Title = "Pharmacology", Language = DefaultLanguage, SystemName = "pharmacology", Excerpt = "Drug development and effects" };
                    dbContext.MyNetworkCategories.Add(e6);
                }
                MyNetworkCategory e7 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "astronomy").FirstOrDefaultAsync();
                if (e7 == null)
                {
                    e7 = new MyNetworkCategory { Parent = d0, Title = "Astronomy", Language = DefaultLanguage, SystemName = "astronomy", Excerpt = "Celestial bodies and cosmic phenomena." };
                    dbContext.MyNetworkCategories.Add(e7);
                }
                MyNetworkCategory e8 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "astronomy").FirstOrDefaultAsync();
                if (e8 == null)
                {
                    e8 = new MyNetworkCategory { Parent = d0, Title = "Geology", Language = DefaultLanguage, SystemName = "geology", Excerpt = "Earth’s structure, minerals, and tectonics." };
                    dbContext.MyNetworkCategories.Add(e8);
                }
        }

        public async Task SetupCreativeDesignCategoriesAsync(ApplicationDbContext dbContext)
        {
            MyNetworkCategory d0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "creative-design").FirstOrDefaultAsync();
            if (d0 == null)
            {
                d0 = new MyNetworkCategory { Title = "Creative & Design", Language = DefaultLanguage, SystemName = "creative-design", Excerpt = "Network focusing Visual Arts, Graphic Design, Animation, Fashion Design, etc" };
                dbContext.MyNetworkCategories.Add(d0);
            }
                MyNetworkCategory e0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "visual-arts").FirstOrDefaultAsync();
                if (e0 == null)
                {
                e0 = new MyNetworkCategory { Parent= d0, Title = "Visual Arts", Language = DefaultLanguage, SystemName = "visual-arts", Excerpt = "Painting, drawing, sculpture, and mixed media." };
                    dbContext.MyNetworkCategories.Add(e0);
                }
                MyNetworkCategory e1 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "graphic-design").FirstOrDefaultAsync();
                if (e1 == null)
                {
                    e1 = new MyNetworkCategory { Parent = d0, Title = "Graphic Design", Language = DefaultLanguage, SystemName = "graphic-design", Excerpt = "Including Logos, branding, posters, and digital layouts." };
                    dbContext.MyNetworkCategories.Add(e1);
                }
                MyNetworkCategory e2 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "animation").FirstOrDefaultAsync();
                if (e2 == null)
                {
                    e2 = new MyNetworkCategory { Parent = d0, Title = "Animation", Language = DefaultLanguage, SystemName = "animation", Excerpt = "Including Motion graphics, character animation, and storytelling." };
                    dbContext.MyNetworkCategories.Add(e2);
                }
                MyNetworkCategory e3 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "fashion-design").FirstOrDefaultAsync();
                if (e3 == null)
                {
                    e3 = new MyNetworkCategory { Parent = d0, Title = "Fashion Design", Language = DefaultLanguage, SystemName = "fashion-design", Excerpt = "Including Clothing, accessories, and textile innovation." };
                    dbContext.MyNetworkCategories.Add(e3);
                }
                MyNetworkCategory e4 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "interior-design").FirstOrDefaultAsync();
                if (e4 == null)
                {
                    e4 = new MyNetworkCategory { Parent = d0, Title = "Interior Design", Language = DefaultLanguage, SystemName = "interior-design", Excerpt = "Including Spatial planning, decor, and ambiance creation." };
                    dbContext.MyNetworkCategories.Add(e4);
                }
                MyNetworkCategory e5 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "creative-design").FirstOrDefaultAsync();
                if (e5 == null)
                {
                    e5 = new MyNetworkCategory { Parent = d0, Title = "Photography", Language = DefaultLanguage, SystemName = "creative-design", Excerpt = "Capturing moments, portraits, and visual narratives." };
                    dbContext.MyNetworkCategories.Add(e5);
                }
                MyNetworkCategory e6 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "creative-design").FirstOrDefaultAsync();
                if (e6 == null)
                {
                    e6 = new MyNetworkCategory { Parent = d0, Title = "Music & Sound Design", Language = DefaultLanguage, SystemName = "music-sound-design", Excerpt = "Composition, audio editing, and soundscapes." };
                    dbContext.MyNetworkCategories.Add(e6);
                }
                MyNetworkCategory e7 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "writing-storytelling").FirstOrDefaultAsync();
                if (e7 == null)
                {
                    e7 = new MyNetworkCategory { Parent = d0, Title = "Writing & Storytelling", Language = DefaultLanguage, SystemName = "writing-storytelling", Excerpt = "Writing & Storytelling: Fiction, non - fiction, screenwriting, and copywriting." };
                    dbContext.MyNetworkCategories.Add(e7);
                }
                MyNetworkCategory e8 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "film-video-production").FirstOrDefaultAsync();
                if (e8 == null)
                {
                    e8 = new MyNetworkCategory { Parent = d0, Title = "Film & Video Production", Language = DefaultLanguage, SystemName = "film-video-production", Excerpt = "Cinematography, editing, and directing." };
                    dbContext.MyNetworkCategories.Add(e8);
                }
                MyNetworkCategory e9 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "game-design").FirstOrDefaultAsync();
                if (e9 == null)
                {
                    e9 = new MyNetworkCategory { Parent = d0, Title = "Game Design", Language = DefaultLanguage, SystemName = "game-design", Excerpt = "Including World-building, mechanics, and interactive storytelling." };
                    dbContext.MyNetworkCategories.Add(e9);
                }
                MyNetworkCategory e10 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "ui-ux-design").FirstOrDefaultAsync();
                if (e10 == null)
                {
                    e10 = new MyNetworkCategory { Parent = d0, Title = "UI / UX Design", Language = DefaultLanguage, SystemName = "ui-ux-design", Excerpt = "Designing user interfaces and optimizing user experiences." };
                    dbContext.MyNetworkCategories.Add(e10);
                }

            MyNetworkCategory d1 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "creative-design").FirstOrDefaultAsync();
            if (d1 == null)
            {
                d1 = new MyNetworkCategory { Title = "Analytical & Technical", Language = DefaultLanguage, SystemName = "analytical-technical", Excerpt = "Centers on logic, precision, and problem-solving. Often involves data, systems, and structured thinking." };
                dbContext.MyNetworkCategories.Add(d1);
            }
                MyNetworkCategory t0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "computer-science").FirstOrDefaultAsync();
                if (t0 == null)
                {
                    t0 = new MyNetworkCategory { Parent = d1, Title = "Computer Science", Language = DefaultLanguage, SystemName = "computer-science", Excerpt = "Programming, algorithms, and software architecture." };
                    dbContext.MyNetworkCategories.Add(t0);
                }
                MyNetworkCategory t1 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "computer-science").FirstOrDefaultAsync();
                if (t1 == null)
                {
                    t1 = new MyNetworkCategory { Parent = d1, Title = "Data Science", Language = DefaultLanguage, SystemName = "data-science", Excerpt = "Data analysis, machine learning, and predictive modeling." };
                    dbContext.MyNetworkCategories.Add(t1);
                }
                MyNetworkCategory t2 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "computer-science").FirstOrDefaultAsync();
                if (t2 == null)
                {
                    t2 = new MyNetworkCategory { Parent = d1, Title = "Mathematics", Language = DefaultLanguage, SystemName = "mathematics", Excerpt = "Abstract reasoning, calculations, and theoretical models." };
                    dbContext.MyNetworkCategories.Add(t2);
                }
                MyNetworkCategory t3 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "computer-science").FirstOrDefaultAsync();
                if (t3 == null)
                {
                    t3 = new MyNetworkCategory { Parent = d1, Title = "Engineering", Language = DefaultLanguage, SystemName = "engineering", Excerpt = "Designing and building systems (mechanical, electrical, civil)." };
                    dbContext.MyNetworkCategories.Add(t3);
                }
                MyNetworkCategory t4 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "ai-machine-learning").FirstOrDefaultAsync();
                if (t4 == null)
                {
                    t4 = new MyNetworkCategory { Parent = d1, Title = "AI & Machine Learning", Language = DefaultLanguage, SystemName = "ai-machine-learning", Excerpt = "Intelligent systems and automation." };
                    dbContext.MyNetworkCategories.Add(t4);
                }
                MyNetworkCategory t5 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "cybersecurity").FirstOrDefaultAsync();
                if (t5 == null)
                {
                    t5 = new MyNetworkCategory { Parent = d1, Title = "Cybersecurity", Language = DefaultLanguage, SystemName = "cybersecurity", Excerpt = "Protecting digital assets and networks." };
                    dbContext.MyNetworkCategories.Add(t5);
                }
                MyNetworkCategory t6 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "software-development").FirstOrDefaultAsync();
                if (t6 == null)
                {
                    t6 = new MyNetworkCategory { Parent = d1, Title = "Software Development", Language = DefaultLanguage, SystemName = "software-development", Excerpt = "Creating applications and platforms." };
                    dbContext.MyNetworkCategories.Add(t6);
                }
                MyNetworkCategory t7 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "robotics").FirstOrDefaultAsync();
                if (t7 == null)
                {
                    t7 = new MyNetworkCategory { Parent = d1, Title = "Robotics", Language = DefaultLanguage, SystemName = "robotics", Excerpt = "Designing intelligent machines and automation systems." };
                    dbContext.MyNetworkCategories.Add(t7);
                }
                MyNetworkCategory t8 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "robotics").FirstOrDefaultAsync();
                if (t8 == null)
                {
                    t8 = new MyNetworkCategory { Parent = d1, Title = "Statistics", Language = DefaultLanguage, SystemName = "statistics", Excerpt = "Data interpretation and probability modeling." };
                    dbContext.MyNetworkCategories.Add(t8);
                }
                MyNetworkCategory t9 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "systems-architecture").FirstOrDefaultAsync();
                if (t9 == null)
                {
                    t9 = new MyNetworkCategory { Parent = d1, Title = "Systems Architecture", Language = DefaultLanguage, SystemName = "systems-architecture", Excerpt = "Structuring complex IT systems and networks." };
                    dbContext.MyNetworkCategories.Add(t9);
                }
        }
        public async Task SetupMyNetworkCategoriesAsync(ApplicationDbContext dbContext)
        {
            MyNetworkCategory d0 = await dbContext.MyNetworkCategories.Where(x => x.SystemName == "standard").FirstOrDefaultAsync();
            if (d0 == null)
            {
                d0 = new MyNetworkCategory { Title = "Standard", SystemName = "standard", Excerpt = "", Language=DefaultLanguage };
                dbContext.MyNetworkCategories.Add(d0);
            }
            await SetupCreativeDesignCategoriesAsync(dbContext);
            await SetupScientificMedicalAsync(dbContext);
            await SetupBusinessProfessionalAsync(dbContext);
            await SetupEducationTrainingAsync(dbContext);
            await SocialCulturalAsync(dbContext);
            await SetupTravelLifestyleAsync(dbContext);
            await SetupEntertainmentMediaAsync(dbContext);
            await SetupLegalGovernmentAsync(dbContext);
            await SetupEnvironmentalSustainabilityAsync(dbContext);
        }
        public async Task SetupServiceCategoriesAsync(ApplicationDbContext dbContext)
        {
            ServiceCategory d1 = await dbContext.ServiceCategories.Where(x => x.SystemName == "entertainment").FirstOrDefaultAsync();
            if (d1 == null)
            {
                d1 = new ServiceCategory { Title = "Entertainment", SystemName = "entertainment", Excerpt = "" };
                dbContext.ServiceCategories.Add(d1);
            }
            ServiceCategory d2 = await dbContext.ServiceCategories.Where(x => x.SystemName == "literature").FirstOrDefaultAsync();
            if (d2 == null)
            {
                d2 = new ServiceCategory { Title = "Literature", SystemName = "literature", Excerpt = "" };
                dbContext.ServiceCategories.Add(d2);
            }
            ServiceCategory d3 = await dbContext.ServiceCategories.Where(x => x.SystemName == "graphics-design").FirstOrDefaultAsync();
            if (d3 == null)
            {
                d3 = new ServiceCategory { Title = "Graphics & Design", SystemName = "graphics-design", Excerpt = "" };
                dbContext.ServiceCategories.Add(d3);
            }
            ServiceCategory d4 = await dbContext.ServiceCategories.Where(x => x.SystemName == "programming-tech").FirstOrDefaultAsync();
            if (d4 == null)
            {
                d4 = new ServiceCategory { Title = "Programming & Tech", SystemName = "programming-tech", Excerpt = "" };
                dbContext.ServiceCategories.Add(d4);
            }
            ServiceCategory d5 = await dbContext.ServiceCategories.Where(x => x.SystemName == "digital-marketing").FirstOrDefaultAsync();
            if (d5 != null)
            {
                d5 = new ServiceCategory { Title = "Digital Marketing", SystemName = "digital-marketing", Excerpt = "" };
                dbContext.ServiceCategories.Add(d5);
            }
            ServiceCategory d6 = await dbContext.ServiceCategories.Where(x => x.SystemName == "video-animation").FirstOrDefaultAsync();
            if (d6 != null)
            {
                d6 = new ServiceCategory { Title = "Video & Animation", SystemName = "video-animation", Excerpt = "" };
                dbContext.ServiceCategories.Add(d6);
            }
            ServiceCategory d7 = await dbContext.ServiceCategories.Where(x => x.SystemName == "writing-translation").FirstOrDefaultAsync();
            if (d7 != null)
            {
                d7 = new ServiceCategory { Title = "Writing & Translation", SystemName = "writing-translation", Excerpt = "" };
                dbContext.ServiceCategories.Add(d7);
            }
            ServiceCategory d8 = await dbContext.ServiceCategories.Where(x => x.SystemName == "music-audio").FirstOrDefaultAsync();
            if (d8 != null)
            {
                d8 = new ServiceCategory { Title = "Music & Audio", SystemName = "music-audio", Excerpt = "" };
                dbContext.ServiceCategories.Add(d8);
            }
            ServiceCategory d9 = await dbContext.ServiceCategories.Where(x => x.SystemName == "business").FirstOrDefaultAsync();
            if (d9 != null)
            {
                d9 = new ServiceCategory { Title = "Business", SystemName = "business", Excerpt = "" };
                dbContext.ServiceCategories.Add(d9);
            }

            ServiceCategory d10 = await dbContext.ServiceCategories.Where(x => x.SystemName == "data").FirstOrDefaultAsync();
            if (d10 != null)
            {
                d10 = new ServiceCategory { Title = "Data", SystemName = "data", Excerpt = "" };
                dbContext.ServiceCategories.Add(d10);
            }
            ServiceCategory d11 = await dbContext.ServiceCategories.Where(x => x.SystemName == "photography").FirstOrDefaultAsync();
            if (d11 != null)
            {
                d11 = new ServiceCategory { Title = "Photography", SystemName = "photography", Excerpt = "" };
                dbContext.ServiceCategories.Add(d11);
            }
            ServiceCategory d12 = await dbContext.ServiceCategories.Where(x => x.SystemName == "ai-services").FirstOrDefaultAsync();
            if (d12 != null)
            {
                d12 = new ServiceCategory { Title = "AI Services", SystemName = "ai-services", Excerpt = "" };
                dbContext.ServiceCategories.Add(d12);
            }
            await dbContext.SaveChangesAsync();
            var assembly = Assembly.GetExecutingAssembly();
            var t = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            using (Stream stream = assembly.GetManifestResourceStream("HenwoniDataModifierAPI.Data.services_categories.json"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string txt = "";
                    while (!reader.EndOfStream)
                    {
                        txt += reader.ReadLine() + "\n";
                    }
                    List<Data.External.ServiceCategories.ExServiceCategory> categories = JsonSerializer.Deserialize<List<Data.External.ServiceCategories.ExServiceCategory>>(txt);
                    foreach (Data.External.ServiceCategories.ExServiceCategory category in categories)
                    {
                        Models.Services.ServiceCategory dbTopLevelServiceCategory = await dbContext.ServiceCategories.Where(x => x.SystemName == category.SystemName).FirstOrDefaultAsync();
                        if (dbTopLevelServiceCategory == null)
                        {
                            // Create it.
                            dbTopLevelServiceCategory = new Models.Services.ServiceCategory();
                            dbTopLevelServiceCategory.CopyPropertiesFrom(category);
                            dbContext.ServiceCategories.Add(dbTopLevelServiceCategory);
                        }
                        foreach (Data.External.ServiceCategories.ExServiceCategory subCategory in category.SubServiceCategories)
                        {
                            Models.Services.ServiceCategory dbServiceCategory = await dbContext.ServiceCategories.Where(x => x.SystemName == subCategory.SystemName).FirstOrDefaultAsync();
                            if (dbServiceCategory == null)
                            {
                                //Create it
                                dbServiceCategory = new ServiceCategory();
                                dbServiceCategory.CopyPropertiesFrom(subCategory);
                                dbServiceCategory.Parent = dbTopLevelServiceCategory;
                                dbContext.ServiceCategories.Add(dbServiceCategory);
                            }
                        }
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

        }

        public async Task SetupOrganisationTypeAsync(ApplicationDbContext dbContext)
        {
            OrganisationType y1 = await dbContext.OrganisationTypes.Where(x => x.SystemName == "corporation").FirstOrDefaultAsync();
            if (y1 == null)
            {
                y1 = new OrganisationType { Title = "Corporation", SystemName = "corporation", Excerpt = "A corporation is an organization—usually a group of people.", Content = "A corporation is an organization—usually a group of people or a company—authorized by the state to act as a single entity and recognized as such in law for certain purposes. Early incorporated entities were established by charter. Most jurisdictions now allow the creation of new corporations through registration." };
                dbContext.OrganisationTypes.Add(y1);
            }
            OrganisationType y2 = await dbContext.OrganisationTypes.Where(x => x.SystemName == "cooperative").FirstOrDefaultAsync();
            if (y2 == null)
            {
                y2 = new OrganisationType { Title = "Cooperative", SystemName = "cooperative", Excerpt = "A cooperative is \"an autonomous association of persons united voluntarily to meet their common economic", Content = "A cooperative is \"an autonomous association of persons united voluntarily to meet their common economic, social and cultural needs and aspirations through a jointly owned and democratically-controlled enterprise" };
                dbContext.OrganisationTypes.Add(y2);
            }

            OrganisationType y3 = await dbContext.OrganisationTypes.Where(x => x.SystemName == "LimitedLiabilityCompany").FirstOrDefaultAsync();
            if (y3 == null)
            {
                y3 = new OrganisationType { Title = "Limited Liability Company", SystemName = "LimitedLiabilityCompany", Excerpt = "A limited liability company is the United States-specific form of a private limited company.", Content = "A limited liability company is the United States-specific form of a private limited company. It is a business structure that can combine the pass-through taxation of a partnership or sole proprietorship with the limited liability of a corporation." };
                dbContext.OrganisationTypes.Add(y3);
            }
            OrganisationType y4 = await dbContext.OrganisationTypes.Where(x => x.SystemName == "NonprofitOrganization").FirstOrDefaultAsync();
            if (y4 == null)
            {
                y4 = new OrganisationType { Title = "Nonprofit Organization", SystemName = "NonprofitOrganization", Excerpt = "A nonprofit organization or non-profit organization", Content = "A nonprofit organization or non-profit organization, also known as a non-business entity, or nonprofit institution, is a legal entity organized and operated for a collective, public or social benefit, in contrary with an entity that operates as a business aiming to generate a profit for its owners" };
                dbContext.OrganisationTypes.Add(y4);
            }
            OrganisationType y5 = await dbContext.OrganisationTypes.Where(x => x.SystemName == "VoluntaryAssociation").FirstOrDefaultAsync();
            if (y5 == null)
            {
                y5 = new OrganisationType { Title = "Voluntary Association", SystemName = "VoluntaryAssociation", Excerpt = "A voluntary group or union is a group of individuals", Content = "A voluntary group or union is a group of individuals who enter into an agreement, usually as volunteers, to form a body to accomplish a purpose. Common examples include trade associations, trade unions, learned societies, professional associations, and environmental groups." };
                dbContext.OrganisationTypes.Add(y5);
            }
            OrganisationType y6 = await dbContext.OrganisationTypes.Where(x => x.SystemName == "CharitableOrganization").FirstOrDefaultAsync();
            if (y6 == null)
            {
                y6 = new OrganisationType { Title = "Charitable Organization", SystemName = "CharitableOrganization", Excerpt = "A charitable organization or charity is an organization", Content = "A charitable organization or charity is an organization whose primary objectives are philanthropy and social well-being. The legal definition of a charitable organization varies between countries and in some instances regions of the country." };
                dbContext.OrganisationTypes.Add(y6);
            }
            OrganisationType y7 = await dbContext.OrganisationTypes.Where(x => x.SystemName == "ProfessionalAssociation").FirstOrDefaultAsync();
            if (y7 == null)
            {
                y7 = new OrganisationType { Title = "Professional Association", SystemName = "ProfessionalAssociation", Excerpt = "A professional association is a group that usually seeks to further", Content = "A professional association is a group that usually seeks to further a particular profession, the interests of individuals and organisations engaged in that profession, and the public interest. In the United States, such an association is typically a nonprofit business league for tax purposes." };
                dbContext.OrganisationTypes.Add(y7);
            }
            OrganisationType y8 = await dbContext.OrganisationTypes.Where(x => x.SystemName == "PrivateLimitedCompany").FirstOrDefaultAsync();
            if (y8 == null)
            {
                y8 = new OrganisationType { Title = "Private Limited Company", SystemName = "PrivateLimitedCompany", Excerpt = "", Content = "A private limited company is any type of business entity in \"private\" ownership used in many jurisdictions, in contrast to a publicly listed company, with some differences from country to country." };
                dbContext.OrganisationTypes.Add(y8);
            }
            OrganisationType y9 = await dbContext.OrganisationTypes.Where(x => x.SystemName == "CharitableTrust").FirstOrDefaultAsync();
            if (y9 == null)
            {
                y9 = new OrganisationType { Title = "Charitable Trust", SystemName = "CharitableTrust", Excerpt = "A charitable trust is an irrevocable trust established for charitable purposes", Content = "A charitable trust is an irrevocable trust established for charitable purposes and, in some jurisdictions, a more specific term than \"charitable organization\". A charitable trust enjoys a varying degree of tax benefits in most countries. It also generates good will." };
                dbContext.OrganisationTypes.Add(y9);
            }
            OrganisationType y10 = await dbContext.OrganisationTypes.Where(x => x.SystemName == "NonGovernmentalOrganization").FirstOrDefaultAsync();
            if (y10 == null)
            {
                y10 = new OrganisationType { Title = "Non-Governmental Organization", SystemName = "NonGovernmentalOrganization", Excerpt = "A non-governmental organization or non-governmental organisation is an organization that generally is formed ", Content = "A non-governmental organization or non-governmental organisation is an organization that generally is formed independent from government." };
                dbContext.OrganisationTypes.Add(y10);
            }
            OrganisationType y11 = await dbContext.OrganisationTypes.Where(x => x.SystemName == "MutualOrganization").FirstOrDefaultAsync();
            if (y11 == null)
            {
                y11 = new OrganisationType { Title = "Mutual Organization", SystemName = "MutualOrganization", Excerpt = "A mutual organization, or mutual society is an organization based on the principle of mutuality and governed by private law.", Content = "A mutual organization, or mutual society is an organization based on the principle of mutuality and governed by private law. Unlike a true cooperative, members usually do not contribute to the capital of the company by direct investment, but derive their right to profits and votes through their customer relationship." };
                dbContext.OrganisationTypes.Add(y11);
            }
            OrganisationType y12 = await dbContext.OrganisationTypes.Where(x => x.SystemName == "PoliticalOrganisation").FirstOrDefaultAsync();
            if (y12 == null)
            {
                y12 = new OrganisationType { Title = "Political Organisation", SystemName = "PoliticalOrganisation", Excerpt = "A political organization is any organization that involves itself in the political process", Content = "A political organization is any organization that involves itself in the political process, including political parties, non-governmental organizations, and special interest advocacy groups." };
                dbContext.OrganisationTypes.Add(y12);
            }
            await dbContext.SaveChangesAsync();
        }

        private async Task SetupLanguages2Async(ApplicationDbContext dbContext)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var t = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            using (Stream stream = assembly.GetManifestResourceStream("HenwoniDataModifierAPI.Data.languages2.json"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    List<Data.External.Languages.NExLanguage2.ExLanguage2> jsonResponse = JsonSerializer.Deserialize<List<Data.External.Languages.NExLanguage2.ExLanguage2>>(json);
                    int c = 0;
                    foreach (Data.External.Languages.NExLanguage2.ExLanguage2 exLanguage in jsonResponse)
                    {
                        String systemName = exLanguage.Name.ToLower().GenerateSlug();
                        Language c2 = await dbContext.Languages.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
                        if (c2 != null)
                        {
                            c2.LocaleTitle = exLanguage.NativeName;
                            c2.NativeName = exLanguage.NativeName;
                        }
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        private async Task SetupLanguages3Async(ApplicationDbContext dbContext)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var t = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            using (Stream stream = assembly.GetManifestResourceStream("HenwoniDataModifierAPI.Data.languages3.json"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    List<Data.External.Languages.NExLanguage3.ExLanguage3> jsonResponse = JsonSerializer.Deserialize<List<Data.External.Languages.NExLanguage3.ExLanguage3>>(json);
                    int c = 0;
                    foreach (var exLanguage in jsonResponse)
                    {
                        String systemName = exLanguage.Name.ToLower().GenerateSlug();
                        Language c2 = await dbContext.Languages.Where(x => x.SystemName == systemName || x.Code== exLanguage.Iso639_1).FirstOrDefaultAsync();
                        if (c2 != null)
                        {
                            c2.ISO6391 = exLanguage.Iso639_1;
                            c2.ISO6392 = exLanguage.Iso639_2;
                            c2.ISO6393 = exLanguage.Iso639_3;
                            c2.Code = exLanguage.Iso639_1;
                            foreach (var cc in exLanguage.Countries)
                            {
                                var str1 = cc.Name.ToLower().GenerateSlug();
                                var code = cc.Code.ToLower();
                                var xx = await dbContext.Countries.Where(x => x.SystemName == str1 || x.ISO2 == code).FirstOrDefaultAsync();
                                if (xx!=null)
                                {
                                    xx.ISO2 = code;
                                    xx.Native = cc.NameLocal;
                                }
                                c2.Countries.Add(xx);
                            }
                        }
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private async Task SetupLanguages4Async(ApplicationDbContext dbContext)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var t = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            using (Stream stream = assembly.GetManifestResourceStream("HenwoniDataModifierAPI.Data.languages4.json"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    List<Data.External.Languages.NExLanguage4.ExLanguage4> jsonResponse = JsonSerializer.Deserialize<List<Data.External.Languages.NExLanguage4.ExLanguage4>>(json);
                    int c = 0;
                    foreach (var language in await dbContext.Languages.ToListAsync())
                    {
                        var carC = jsonResponse.Where(x => x.Name.ToLower().Contains(language.Title.ToLower())).FirstOrDefault();
                        if (carC!=null)
                        {
                            language.Charset = carC.Charset;
                        }
                    }
                    await dbContext.SaveChangesAsync();
                    foreach (var e in jsonResponse)
                    {
                        if (e.Name.Contains("(") && e.Name.Contains(")"))
                        {
                            var existing = await dbContext.Languages.Where(v => v.Title.ToLower() == e.Name.ToLower()).FirstOrDefaultAsync();
                            if (existing==null)
                            {
                                existing = new Language()
                                {
                                    Title = e.Name,
                                    Code = e.Charset
                                };
                                existing.SystemName = e.Name.GenerateSlug();
                                await dbContext.Languages.AddAsync(existing);
                            }
                            string[] parts = e.Name.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string part in parts)
                            {
                                var k = part.Trim().ToLower();
                                if (k.Count()>2) {
                                    var country = await dbContext.Countries.Where(x => x.Name.ToLower().Contains(k)).FirstOrDefaultAsync();
                                    if (country!=null) existing.Countries.Add(country);
                                }
                            }
                        }
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task SetupLanguagesAsync(ApplicationDbContext dbContext)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var t = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            using (Stream stream = assembly.GetManifestResourceStream("HenwoniDataModifierAPI.Data.languages.json"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    List<Data.External.Languages.ExLanguage> jsonResponse = JsonSerializer.Deserialize<List<Data.External.Languages.ExLanguage>>(json);
                    int c = 0;
                    foreach (Data.External.Languages.ExLanguage exLanguage in jsonResponse)
                    {
                        String systemName = exLanguage.Name.GenerateSlug();

                        Language c2 = await dbContext.Languages.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
                        if (c2 == null)
                        {
                            c2 = new Language { Title = exLanguage.Name, SystemName = systemName };
                            dbContext.Languages.Add(c2);
                        }
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
            await SetupLanguages2Async(dbContext);
            await SetupLanguages3Async(dbContext);
            await SetupLanguages4Async(dbContext);
        }

        public async Task SetupJobIndustriesAsync(ApplicationDbContext dbContext)
        {
            JobIndustry y0 = await dbContext.JobIndustries.Where(x => x.SystemName == "unknown").FirstOrDefaultAsync();
            if (y0 == null)
            {
                y0 = new JobIndustry { Title = "Unknown", SystemName = "unknown" };
                dbContext.JobIndustries.Add(y0);
            }
            JobIndustry y1 = await dbContext.JobIndustries.Where(x => x.SystemName == "office-administrative-support").FirstOrDefaultAsync();
            if (y1 == null)
            {
                y1 = new JobIndustry { Title = "Office and administrative support", SystemName = "office-administrative-support" };
                dbContext.JobIndustries.Add(y1);
            }
            JobIndustry y2 = await dbContext.JobIndustries.Where(x => x.SystemName == "management").FirstOrDefaultAsync();
            if (y2 == null)
            {
                y2 = new JobIndustry { Title = "Management", SystemName = "management" };
                dbContext.JobIndustries.Add(y2);
            }
            JobIndustry y3 = await dbContext.JobIndustries.Where(x => x.SystemName == "business-and-financial").FirstOrDefaultAsync();
            if (y3 == null)
            {
                y3 = new JobIndustry { Title = "Business and financial", SystemName = "business-and-financial" };
                dbContext.JobIndustries.Add(y3);
            }
            JobIndustry y4 = await dbContext.JobIndustries.Where(x => x.SystemName == "architecture-and-engineering").FirstOrDefaultAsync();
            if (y4 == null)
            {
                y4 = new JobIndustry { Title = "Architecture and engineering", SystemName = "architecture-and-engineering" };
                dbContext.JobIndustries.Add(y4);
            }
            JobIndustry y5 = await dbContext.JobIndustries.Where(x => x.SystemName == "arts-and-design").FirstOrDefaultAsync();
            if (y5 == null)
            {
                y5 = new JobIndustry { Title = "Arts and design", SystemName = "arts-and-design" };
                dbContext.JobIndustries.Add(y5);
            }
            JobIndustry y6 = await dbContext.JobIndustries.Where(x => x.SystemName == "computing-information-technology").FirstOrDefaultAsync();
            if (y6 == null)
            {
                y6 = new JobIndustry { Title = "Computing & Information Technology", SystemName = "computing-information-technology" };
                dbContext.JobIndustries.Add(y6);
            }
            JobIndustry y7 = await dbContext.JobIndustries.Where(x => x.SystemName == "education-training-and-library").FirstOrDefaultAsync();
            if (y7 == null)
            {
                y7 = new JobIndustry { Title = "Education, training and library", SystemName = "education-training-and-library" };
                dbContext.JobIndustries.Add(y7);
            }
            JobIndustry y8 = await dbContext.JobIndustries.Where(x => x.SystemName == "healthcare").FirstOrDefaultAsync();
            if (y8 == null)
            {
                y8 = new JobIndustry { Title = "Healthcare", SystemName = "healthcare" };
                dbContext.JobIndustries.Add(y8);
            }
            JobIndustry y9 = await dbContext.JobIndustries.Where(x => x.SystemName == "entertainment-and-sports").FirstOrDefaultAsync();
            if (y9 == null)
            {
                y9 = new JobIndustry { Title = "Entertainment and sports", SystemName = "entertainment-and-sports" };
                dbContext.JobIndustries.Add(y9);
            }
            JobIndustry y10 = await dbContext.JobIndustries.Where(x => x.SystemName == "legal").FirstOrDefaultAsync();
            if (y10 == null)
            {
                y10 = new JobIndustry { Title = "Legal", SystemName = "legal" };
                dbContext.JobIndustries.Add(y10);
            }
            JobIndustry y11 = await dbContext.JobIndustries.Where(x => x.SystemName == "life-physical-and-social-science").FirstOrDefaultAsync();
            if (y11 == null)
            {
                y11 = new JobIndustry { Title = "Life, physical and social science", SystemName = "life-physical-and-social-science" };
                dbContext.JobIndustries.Add(y11);
            }
            JobIndustry y12 = await dbContext.JobIndustries.Where(x => x.SystemName == "transportation-and-material-moving").FirstOrDefaultAsync();
            if (y12 == null)
            {
                y12 = new JobIndustry { Title = "Transportation and material moving", SystemName = "transportation-and-material-moving" };
                dbContext.JobIndustries.Add(y12);
            }
            JobIndustry y13 = await dbContext.JobIndustries.Where(x => x.SystemName == "protective-service").FirstOrDefaultAsync();
            if (y13 == null)
            {
                y13 = new JobIndustry { Title = "Protective service", SystemName = "protective-service" };
                dbContext.JobIndustries.Add(y13);
            }
            JobIndustry y14 = await dbContext.JobIndustries.Where(x => x.SystemName == "community-and-social-services").FirstOrDefaultAsync();
            if (y14 == null)
            {
                y14 = new JobIndustry { Title = "Community and social services", SystemName = "community-and-social-services" };
                dbContext.JobIndustries.Add(y14);
            }
            JobIndustry y15 = await dbContext.JobIndustries.Where(x => x.SystemName == "sales").FirstOrDefaultAsync();
            if (y15 == null)
            {
                y15 = new JobIndustry { Title = "Sales", SystemName = "sales" };
                dbContext.JobIndustries.Add(y15);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
