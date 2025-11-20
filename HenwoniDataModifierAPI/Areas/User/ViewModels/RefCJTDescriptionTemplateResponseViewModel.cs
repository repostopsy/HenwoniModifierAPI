using HenwoniDataModifierAPI.Models.Common;
using HenwoniDataModifierAPI.Utilities;

namespace HenwoniDataModifierAPI.Areas.User.ViewModels
{
    public class RefCJTDescriptionTemplateResponseViewModel
    {
        public long ServerId { get; set; }
        public long? ServerParentId { get; set; }
        public String SystemName { get; set; }
        public RefCommonJobTitleResponseViewModel JobTitle { get; set; }
        public ApplicationUserResponseViewModel? Author { get; set; }
        public String Title { get; set; }
        public String? Excerpt { get; set; }
        public String Template { get; set; }
        public String Notes { get; set; }
        public String Language { get; set; }

        public static RefCJTDescriptionTemplateResponseViewModel From(RefCJTDescriptionTemplate c)
        {
            RefCJTDescriptionTemplateResponseViewModel m = new RefCJTDescriptionTemplateResponseViewModel();
            if (c.Language != null) m.Language = c.Language.SystemName;
            m.CopyPropertiesFrom(c);
            m.ServerParentId = c.ParentId;
            m.JobTitle = RefCommonJobTitleResponseViewModel.From(c.RefCommonJobTitle);
            if (c.Author != null) m.Author = ApplicationUserResponseViewModel.From(c.Author);
            m.ServerId = c.Id;
            return m;
        }
    }
}
