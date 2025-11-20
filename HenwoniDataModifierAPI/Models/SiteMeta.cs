using HenwoniDataModifierAPI.Models.Location;
using System.ComponentModel.DataAnnotations.Schema;

namespace HenwoniDataModifierAPI.Models
{
    public class SiteMeta
    {
        public long Id { get; set; }
        public string SystemName { get; set; }
        //[Column(TypeName = "text")]
        [Column(TypeName = "nvarchar(max)")]
        public string Value { get; set; }
        public virtual Language Language { get; set; }
        public long? ParentId { get; set; }
        /// <summary>
        /// Between 0 and 1
        /// </summary>
        public double Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
