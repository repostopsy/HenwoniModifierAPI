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
        public string Text { get; set; }
        public string EditorUserName { get; set; }
        public virtual Language Language { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
