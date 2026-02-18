

using System.Text.Json.Serialization;

namespace HenwoniDataModifierAPI.Models.Location
{
	public class Language
	{
        public Language()
        {
            Countries = new HashSet<Country>();
        }
        public long Id { get; set; }
		public string Title { get; set; }
		public string? Flag { get; set; }
        /// <summary>
        /// ISO 639-1 code
        /// </summary>
        public string? ISO6391 { get; set; }
        public string? ISO6392 { get; set; }
        public string? ISO6393 { get; set; }
        public string SystemName { get; set; }
        public string? Charset { get; set; }
        public string? NativeName { get; set; }
		public string? LocaleTitle { get; set; }
        public string? Code { get; set; }
        [JsonIgnore]
        public virtual ICollection<Country> Countries { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool Active { get; set; } = true;
	}
}