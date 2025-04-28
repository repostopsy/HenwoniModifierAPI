namespace HenwoniDataModifierAPI.Data.External.ServiceCategories
{
    public class ExServiceCategory
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string SystemName { get; set; }
        public string? Excerpt { get; set; }
        public string? Content { get; set; }
        public List<ExServiceCategory> SubServiceCategories { get; set; } = new List<ExServiceCategory>();
    }
}
