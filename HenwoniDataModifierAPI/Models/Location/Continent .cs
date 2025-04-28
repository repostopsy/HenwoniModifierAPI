


namespace HenwoniDataModifierAPI.Models.Location
{
    public class Continent : ILocation
	{
		public long Id { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
		public long JobsCount { get; set; }
		public virtual ICollection<ContinentRegion> Regions { get; set;}
		public virtual ICollection<Country> Countries { get; set; }
        public virtual Language? Language { get; set; }
    }
}
