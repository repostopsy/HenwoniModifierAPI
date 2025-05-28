using HenwoniDataModifierAPI.Models.Location;

namespace HenwoniDataModifierAPI.Models.Services.Common
{
    public class RefCServiceTitleTemplateAlias
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public virtual Language? Language { get; set; }
        public string Title { get; set; }
        public string SystemName { get; set; }
        public string? Excerpt { get; set; }
        public virtual RefCServiceTitleTemplate RefCServiceTitleTemplate { get; }
    }
}
