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
using DotLiquid;

namespace HenwoniDataModifierAPI.Automatic
{
    public partial class AutomaticSetup : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _period = TimeSpan.FromSeconds(5);
        private bool _stopTask = false;
        ApplicationDbContext dbContext;
        private readonly IConfiguration _config;

        public AutomaticSetup (IServiceProvider serviceProvider, IConfiguration config)
        {
            _serviceProvider = serviceProvider;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            await using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


            var titles = await context.RefCommonJobTitles.ToListAsync();
            foreach (var title in titles)
            {
                var duplicates = await context.RefCommonJobTitles.Where(x => x.Id != title.Id && x.Title == title.Title).ToListAsync();
                if (duplicates.Count > 0)
                {
                    context.RefCommonJobTitles.RemoveRange(duplicates);
                }
                var sameSystemNames = await context.RefCommonJobTitles.Where(x => x.Id != title.Id && x.SystemName == title.SystemName).ToListAsync();
                foreach (var ssn in sameSystemNames)
                {
                    ssn.SystemName = ssn.SystemName + "2";
                }
            }
            await context.SaveChangesAsync();


            await SetupLanguagesAsync(context);
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
            var assembly = Assembly.GetExecutingAssembly();
            var t = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            using (Stream stream = assembly.GetManifestResourceStream("HenwoniDataModifierAPI.Data.spotterful.txt"))
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
                    System.Diagnostics.Debug.WriteLine(titles.Count);
                    if (await dbContext.RefCommonJobTitles.CountAsync() < titles.Count)
                    {
                        int i = 0;
                        foreach (string title in titles)
                        {
                            string b = title.Trim();
                            if (await dbContext.RefCommonJobTitles.Where(x => x.Title == b).FirstOrDefaultAsync() == null)
                            {
                                RefCommonJobTitle jobTitle = new RefCommonJobTitle();
                                jobTitle.SystemName = title.GenerateSlug();
                                jobTitle.Title = title;
                                jobTitle.Description = title;
                                jobTitle.DateUpdated = DateTime.UtcNow.AddDays(i);
                                jobTitle.DateCreated = DateTime.UtcNow.AddDays(i);
                                jobTitle.PluralTitle = title + "s";
                                //@TODO: Create an event that will be used to update the UI status text
                                /*Dispatcher.Invoke(() =>
								{
									LoadStatus.Text = "Inserting " + jobTitle.Title;
								});*/
                                dbContext.RefCommonJobTitles.Add(jobTitle);
                            }
                            i++;
                            if (i > 50) i = 0;
                        }
                        await dbContext.SaveChangesAsync();
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

        public async Task SetupLanguagesAsync(ApplicationDbContext dbContext)
        {
            HttpClient httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("https://gist.githubusercontent.com/jrnk/8eb57b065ea0b098d571/raw/936a6f652ebddbe19b1d100a60eedea3652ccca6/ISO-639-1-language.json");
            response.EnsureSuccessStatusCode();
            List<Data.External.Languages.ExLanguage>? jsonResponse = await response.Content.ReadFromJsonAsync<List<Data.External.Languages.ExLanguage>>();
            if (jsonResponse != null)
            {
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
            }
            await dbContext.SaveChangesAsync();
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
