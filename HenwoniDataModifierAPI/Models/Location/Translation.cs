using System.ComponentModel.DataAnnotations.Schema;

namespace HenwoniDataModifierAPI.Models.Location
{
    public class Translation
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        /// <summary>
        /// Between 0 and 1
        /// </summary>
        public double Rating { get; set; }
        /// <summary>
        /// WhatIsWhat?<-WhereTo
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        public string? DefaultLanguageText { get; set; }
        public string SystemContextIdentity { get; set; }
        public string? SystemName { get; set; }
        /// <summary>
        /// The name of the object being targeted
        /// </summary>
        public string? DestinSystemName { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? Title { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? Excerpt { get; set; }
        // [Column(TypeName = "text")]
        [Column(TypeName = "nvarchar(max)")]
        public string Text { get; set; }
        public virtual Language Language { get; set; }
        public virtual ApplicationUser? Author { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; }
    }
}
