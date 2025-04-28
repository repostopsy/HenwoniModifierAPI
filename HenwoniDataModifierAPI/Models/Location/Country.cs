

using System.Text.Json.Serialization;

namespace HenwoniDataModifierAPI.Models.Location
{
    public class Country : ILocation
	{
		public long Id { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
        public string ISO3 { get; set; }
        public string ISO2 { get; set; }
        public string? NumericCode { get; set; }
        public string? PhoneCode { get; set; }
        public string? Capital { get; set; }
		public virtual Pricing.Currency DefaultCurrency { get; set; }
		public virtual long DefaultCurrencyId { get; set; }
		public string? TLD { get; set; }
        public string? Native { get; set; }
		public virtual Continent? Continent { get; set; }
		public virtual ContinentRegion? ContinentRegion { get; set; }
        public string Nationality { get; set; }
		public virtual ICollection<CountryTranslations> Translations { get; set; }
        public virtual ICollection<CountryTimeZone> TimeZones { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string? Emoji { get; set; }
        public string? EmojiU { get; set; }
        public virtual ICollection<State> States { get; set; }
        public string? TopologyId { get; set; }
		public long JobsCount { get; set; }
        public virtual Language? Language { get; set; }
    }
}
