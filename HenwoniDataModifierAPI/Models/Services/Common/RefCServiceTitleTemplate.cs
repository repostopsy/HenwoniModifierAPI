using HenwoniDataModifierAPI.Models.Location;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HenwoniDataModifierAPI.Models.Services.Common
{
    public class RefCServiceTitleTemplate
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string SystemName { get; set; }
        public string Title { get; set; }
        public string? Excerpt { get; set; }
        [Column(TypeName = "text")]
        public string Template { get; set; }
        [JsonIgnore]
        [Column(TypeName = "text")]
        public string? Notes { get; set; }
        public virtual RefCServiceTitle RefCServiceTitle { get; set; }
        public virtual Language Language { get; set; }
        public virtual ICollection<RefCServiceTitleTemplateTag> Tags { get; }
        public virtual ICollection<RefCServiceTitleTemplateAlias> Aliases { get; }
    }
}
