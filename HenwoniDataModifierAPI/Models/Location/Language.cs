

namespace HenwoniDataModifierAPI.Models.Location
{
	public class Language
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string? Flag { get; set; }
		public string SystemName { get; set; }
		public string? LocaleTitle { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}
