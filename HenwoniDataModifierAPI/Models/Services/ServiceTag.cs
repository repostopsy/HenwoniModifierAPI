
namespace HenwoniDataModifierAPI.Models.Services
{
	public class ServiceTag
    {
        public long Id { get; set; }
		public string Title { get; set; }
		public string SystemName { get; set; }
		public string? Excerpt { get; set; }
		public long Count { get; set; }
        public bool IsDeleted { get; set; }
    }
}
