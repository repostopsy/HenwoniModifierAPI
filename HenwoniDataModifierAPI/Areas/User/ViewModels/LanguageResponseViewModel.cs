using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Utilities;

namespace HenwoniDataModifierAPI.Areas.User.ViewModels
{
    public class LanguageResponseViewModel
    {
        public string Title { get; set; }
        public string? Flag { get; set; }
        public string? ISO6391 { get; set; }
        public string SystemName { get; set; }
        public string? LocaleTitle { get; set; }
        public string? ISO6392 { get; set; }
        public string? ISO6393 { get; set; }
        public string? Charset { get; set; }
        public string? NativeName { get; set; }
        public static LanguageResponseViewModel From(Language c)
        {
            LanguageResponseViewModel la = new LanguageResponseViewModel();
            la.CopyPropertiesFrom(c);
            return la;
        }
    }
}
