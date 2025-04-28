

using System.Text.Json.Serialization;

namespace HenwoniDataModifierAPI.Models.Location
{
    public class State : ILocation
	{
        public long Id { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
        public virtual Continent? Continent { get; set; }
        public virtual ContinentRegion? ContinentRegion { get; set; }
        public virtual Country? Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual Language? Language { get; set; }
    }
}
