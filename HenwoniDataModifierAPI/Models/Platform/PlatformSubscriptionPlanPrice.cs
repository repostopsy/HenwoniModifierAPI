using HenwoniDataModifierAPI.Models.Location;



namespace HenwoniDataModifierAPI.Models.Platform
{
	public class PlatformSubscriptionPlanPrice
	{
		public long Id { get; set; }
		public decimal Price { get; set; }
		public virtual Pricing.Currency Currency { get; set; }
		public long CurrencyId { get; set; }
		public virtual PlatformSubscriptionPlan Plan { get; set; }
		public long PlanId { get; set; }
	}
}
