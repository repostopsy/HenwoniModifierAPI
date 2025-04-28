


namespace HenwoniDataModifierAPI.Models.Location
{
    public class ContinentRegion
	{
		public long Id { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
        public virtual Continent? Continent { get; set; }
        public long JobsCount { get; set; }
        public virtual Language? Language { get; set; }
    }
}
