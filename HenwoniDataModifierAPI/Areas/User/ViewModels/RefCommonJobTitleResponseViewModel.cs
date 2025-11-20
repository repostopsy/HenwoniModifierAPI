using HenwoniDataModifierAPI.Common.Models;
using HenwoniDataModifierAPI.Models.Common;
using HenwoniDataModifierAPI.Models.Employment;
using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Models.Skills;
using HenwoniDataModifierAPI.Utilities;

namespace HenwoniDataModifierAPI.Areas.User.ViewModels
{
    public class RefCommonJobTitleResponseViewModel
    {
        public RefCommonJobTitleResponseViewModel() { }
        public long ServerId { get; set; }
        public string ParentSystemName { get; set; }
        public string SystemName { get; set; }
        public string? Code { get; set; }
        public string Title { get; set; }
        public bool IsOriginal { get; set; }
        public string? JobDescriptionTemplate { get; set; }
        public string? PluralTitle { get; set; }
        public string? Excerpt { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public ApplicationUserResponseViewModel Author { get; set; }
        public bool Approved { get; set; }
        public double Rating { get; set; }
        public string Language { get; set; }

        public static RefCommonJobTitleResponseViewModel From(RefCommonJobTitle g)
        {
            RefCommonJobTitleResponseViewModel m = new RefCommonJobTitleResponseViewModel();
            if (g.Language != null) m.Language = g.Language.SystemName;
            m.CopyPropertiesFrom(g);
            m.ServerId = g.Id;
            return m;
        }
    }
}
