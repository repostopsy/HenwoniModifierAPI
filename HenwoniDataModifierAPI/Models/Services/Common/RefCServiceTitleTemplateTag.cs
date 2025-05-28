using HenwoniDataModifierAPI.Models.Location;

namespace HenwoniDataModifierAPI.Models.Services.Common
{
    public class RefCServiceTitleTemplateTag
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public virtual Language? Language { get; set; }
        public string Title { get; set; }
        public string SystemName { get; set; }
        public string? Excerpt { get; set; }
        public virtual ICollection<RefCServiceTitleTemplate> RefCServiceTitleTemplates { get; }
    }
}
