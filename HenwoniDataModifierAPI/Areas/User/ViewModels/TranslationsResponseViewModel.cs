using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Models;
using HenwoniDataModifierAPI.Utilities;

namespace HenwoniDataModifierAPI.Areas.User.ViewModels
{
    public class TranslationsResponseViewModel
    {
        public long ServerId { get; set; }
        public long? ParentId { get; set; }
        /// <summary>
        /// WhatIsWhat?<-WhereTo
        /// </summary>
        public string SystemContextIdentity { get; set; }
        /// <summary>
        /// The name of the object being targeted
        /// </summary>
        public string? DestinSystemName { get; set; }
        public string? Title { get; set; }
        public string? Excerpt { get; set; }
        public string Text { get; set; }
        public virtual ApplicationUserResponseViewModel? Author { get; set; }
        public virtual LanguageResponseViewModel Language { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public static TranslationsResponseViewModel From(Translation g)
        {
            TranslationsResponseViewModel m = new TranslationsResponseViewModel();
            if (g.Language != null) m.Language = LanguageResponseViewModel.From(g.Language);
            if (g.Author != null) m.Author = ApplicationUserResponseViewModel.From(g.Author);
            m.CopyPropertiesFrom(g);
            m.ServerId = g.Id;
            return m;
        }
    }
}
