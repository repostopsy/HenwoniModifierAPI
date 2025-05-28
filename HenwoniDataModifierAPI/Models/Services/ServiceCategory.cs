
using System.ComponentModel;
using System.Text.Json.Serialization;
using HenwoniDataModifierAPI.Models.Employment;

namespace HenwoniDataModifierAPI.Models.Services
{
    public class ServiceCategory
	{
        public long Id { get; set; }

        public string Title { get; set; }

        public string SystemName { get; set; }
        private string _excerpt { get; set; }
		public string? Excerpt { get; set; }
		public string? Content { get; set; }
        public long Count { get; set; }
        [JsonIgnore]
        public virtual ServiceCategory? Parent { get; set; }
        [JsonIgnore]
        public virtual ICollection<ServiceCategory> SubServiceCategories { get; set; }
        [JsonIgnore]
        public virtual JobIndustry? JobIndustryRef { get; set; }
        public bool IsDeleted { get; set; } = false;
        [JsonIgnore]
        public virtual ICollection<CommonServiceReference> CommonServiceReferences { get; set; }
    }
}
