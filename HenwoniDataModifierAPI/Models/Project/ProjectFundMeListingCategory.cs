namespace HenwoniDataModifierAPI.Models.Project
{
    public class ProjectFundMeListingCategory
    {
        public long Id { get; set; }
        public virtual ProjectFundMeListingCategory? Parent { get; set; }
        public long? ParentId { get; set; }
        public string Title { get; set; }
        public string SystemName { get; set; }
        /// <summary>
        /// Short 1 line description
        /// </summary>
        public string? Excerpt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateUpdated { get; set; }
    }
}
