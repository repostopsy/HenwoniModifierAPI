using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Models.Skills;


using System.Text.Json.Serialization;

namespace HenwoniDataModifierAPI.Models.Employment
{
    public class JobIndustry
    {
		public long Id { get; set; }
        public string Title { get; set; }
        public string SystemName { get; set; }
        public string? Excerpt { get; set; }
        public string? Content { get; set; }
        public long JobsCount { get; set; }
        public virtual ApplicationUser? Author { get; set; }
        public bool Approved { get; set; }
        [JsonIgnore]
        public virtual Language? Language { get; set; }
        public virtual JobIndustry? Parent { get; set; }
        public double Rating { get; set; }
        public virtual ICollection<CandidateSkill> Skills { get; set; }
        public virtual ICollection<CandidateSoftSkill> SoftSkills { get; set; } 
        public bool IsDeleted { get; set; } = false;
    }
}
