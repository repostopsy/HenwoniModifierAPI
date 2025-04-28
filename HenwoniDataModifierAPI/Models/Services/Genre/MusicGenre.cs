
namespace HenwoniDataModifierAPI.Models.Services.Genre
{
	public class MusicGenre
    {
        public long Id { get; set; }
		public string Title { get; set; }
        public string SystemName { get; set; }
		public string? Excerpt { get; set; }
		public string? Content { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
