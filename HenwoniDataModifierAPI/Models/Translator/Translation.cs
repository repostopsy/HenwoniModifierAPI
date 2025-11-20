using HenwoniDataModifierAPI.Models.Location;

namespace HenwoniDataModifierAPI.Models.Translator
{
    public class Translation
    {
        public long Id { get; set; }
        /// <summary>
        /// WhatIsWhat?<-WhereTo
        /// </summary>
        public string SystemContextIdentity { get; set; }
        public string DestinSystemName { get; set; }
        public string Title { get; set; }
        public string? Excerpt { get; set; }
        public string? DefaultLanguageText { get; set; }
        public string Text { get; set; }
        public virtual ApplicationUser? Author { get; set; }
        public bool Approved { get; set; }
        public double Rating { get; set; }
        public virtual Language Language { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; }
    }
}
