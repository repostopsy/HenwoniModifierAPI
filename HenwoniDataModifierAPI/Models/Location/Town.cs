


namespace HenwoniDataModifierAPI.Models.Location
{
    public class Town : ILocation
	{
		public long Id { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
		public virtual Continent? Continent { get; set; }
		public virtual ContinentRegion? ContinentRegion { get; set; }
		public virtual Country Country { get; set; }
		public virtual State? State { get; set; }
		public virtual City? City { get; set; }
		public long JobsCount { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
