


namespace HenwoniDataModifierAPI.Models.Location
{
    public class CountryTimeZone
	{
        public CountryTimeZone() {
			Countries = new HashSet<Country>();
		}
		public long Id { get; set; }
        public string ZoneName { get; set; }

        public long GmtOffset { get; set; }

        public string GmtOffsetName { get; set; }

        public string Abbreviation { get; set; }

        public string TzName { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
	}
}
