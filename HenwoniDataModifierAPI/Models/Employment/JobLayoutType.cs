using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HenwoniDataModifierAPI.Models.Location;

namespace HenwoniDataModifierAPI.Models.Employment
{
	public class JobLayoutType
	{
		public long Id { get; set; }
		public string SystemName { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
        [JsonIgnore]
        public virtual Language? Language { get; set; }
        public virtual JobLayoutType? Parent { get; set; }
        public double Rating { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
