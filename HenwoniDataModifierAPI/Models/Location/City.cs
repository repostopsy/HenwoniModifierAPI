using HenwoniDataModifierAPI.Models.Common;



namespace HenwoniDataModifierAPI.Models.Location
{
    public class City : ILocation
	{
		public long Id { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
		public virtual Continent? Continent { get; set; }
		public virtual ContinentRegion? ContinentRegion { get; set; }
		public virtual Country? Country { get; set; }
		public virtual State? State { get; set; }
        public long JobsCount { get; set; }
        public virtual Language? Language { get; set; }
	}
}
