
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HenwoniDataModifierAPI.Models.Location;

namespace HenwoniDataModifierAPI.Models.Employment
{
    public class JobLevel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string SystemName { get; set; }
        public string? Excerpt { get; set; }
        [JsonIgnore]
        public virtual Language? Language { get; set; }
        public virtual JobLevel? Parent { get; set; }
        public double Rating { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
