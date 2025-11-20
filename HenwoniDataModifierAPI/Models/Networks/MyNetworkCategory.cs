using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Models.Platform;
using HenwoniDataModifierAPI.Models.Project;

namespace HenwoniDataModifierAPI.Models.Networks
{
    public class MyNetworkCategory
    {
        public MyNetworkCategory()
        {
            SubCategories = new HashSet<MyNetworkCategory>();
        }
        public int Id { get; set; }
        public virtual Language? Language { get; set; }
        public string Title { get; set; }
        public string SystemName { get; set; }
        public string? Excerpt { get; set; }
        public string? Content { get; set; }
        public virtual MyNetworkCategory? Parent { get; set; }
        public int? ParentId { get; set; }
        public virtual ICollection<MyNetworkCategory> SubCategories { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
