

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenwoniDataModifierAPI.Models.Platform
{
	public class PlatformSubscriptionPlanFeature
	{
		public long Id { get; set; }
		public int? Order { get; set; }
		public bool Active { get; set; } = false;
		public string Title { get; set; }
        public string SystemName { get; set; }
		public string Excerpt { get; set; }
		public string? Content { get; set; }
		public long? PlatformSubscriptionPlanId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<PlatformSubscriptionPlan> Plans { get; set; }
    }
}
