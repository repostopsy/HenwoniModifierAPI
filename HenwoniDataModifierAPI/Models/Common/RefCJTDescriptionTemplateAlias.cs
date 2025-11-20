using HenwoniDataModifierAPI.Models.Location;

namespace HenwoniDataModifierAPI.Models.Common
{
    public class RefCJTDescriptionTemplateAlias
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public virtual Language? Language { get; set; }
        public string Title { get; set; }
        public string SystemName { get; set; }
        public string? Excerpt { get; set; }
        public virtual ApplicationUser? Author { get; set; }
        public bool Approved { get; set; }
        public double Rating { get; set; }
        public virtual RefCJTDescriptionTemplate RefCJTDescriptionTemplate { get; }
    }
}
