using HenwoniDataModifierAPI.Models.Location;

namespace HenwoniDataModifierAPI.Models.Common
{
    public class RefCJTDTemplateIntro
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public virtual Language? Language { get; set; }
        public string Content { get; set; }
        public string? ReferenceId { get; set; }
        public virtual ApplicationUser? Author { get; set; }
        public bool Approved { get; set; }
        public double Rating { get; set; }
        public virtual RefCJTDescriptionTemplate RefCJTDescriptionTemplate { get; set; }
    }
}
