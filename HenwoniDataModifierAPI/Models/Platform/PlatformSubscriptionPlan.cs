using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Models.Skills;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenwoniDataModifierAPI.Models.Platform
{
	public class PlatformSubscriptionPlan
	{
		public PlatformSubscriptionPlan()
		{
			Features = new HashSet<PlatformSubscriptionPlanFeature>();
			Pricing = new HashSet<PlatformSubscriptionPlanPrice>();
		}
		public long Id { get; set; }
		public string Title { get; set; }
		public bool Active { get; set; } = false;
		public int? Order { get; set; }
        public string SystemName { get; set; }
		public string Excerpt { get; set; }
		public string? Content { get; set; }
		public string? Notes { get; set; }
        public virtual  ICollection<PlatformSubscriptionPlanFeature> Features { get; set; }
		public virtual ICollection<PlatformSubscriptionPlanPrice> Pricing { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}
