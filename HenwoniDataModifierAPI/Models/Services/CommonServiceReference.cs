using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HenwoniDataModifierAPI.Models.Services
{
	public class CommonServiceReference
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string SystemName { get; set; }
		public string? Excerpt { get; set; }
		public string? Content { get; set; }
		public bool IsDeleted { get; set; } = false;

		[JsonIgnore]
        public virtual ICollection<ServiceCategory> ServiceCategories { get; set; }
        public long ServiceCategoryId { get; set; }
    }
}
