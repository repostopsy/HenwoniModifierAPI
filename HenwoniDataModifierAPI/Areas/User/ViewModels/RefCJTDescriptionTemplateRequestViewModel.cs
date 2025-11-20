namespace HenwoniDataModifierAPI.Areas.User.ViewModels
{
    public class RefCJTDescriptionTemplateRequestViewModel
    {
        public long? ServerId { get; set; }
        public long? ServerParentId { get; set; }
        public String Title { get; set; }
        public String? Excerpt { get; set; }
        public String Template { get; set; }
        public String? Notes { get; set; }
        public String Language { get; set; }
        public long JobTitle { get; set; }
    }
}
