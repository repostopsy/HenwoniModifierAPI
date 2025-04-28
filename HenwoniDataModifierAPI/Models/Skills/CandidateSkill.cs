using HenwoniDataModifierAPI.Models.Employment;
using HenwoniDataModifierAPI.Models.Location;


using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HenwoniDataModifierAPI.Models.Skills
{
    public class CandidateSkill
    {
        public CandidateSkill() { }
        public CandidateSkill(String _title, String _name) {
            Title = _title;
            SystemName = _name;
        }
		public int Id { get; set; }
        public string Title { get; set; }
        public string SourceTitle { get; set; }
        public string SystemName { get; set; }
        /// <summary>
        /// Short 1 line description
        /// </summary>
        public string? Excerpt { get; set; }
        public string? Content { get; set; }
        public string? SkillIcon { get; set; }
        public virtual ICollection<SkillCategory> Categories { get; set; }
        public long CategoryId { get; set; }
        public int TotalSkillCredits { get; set; }
        public virtual JobIndustry? PrimaryJobIndustry { get; set; }
        public long? PrimaryJobIndustryId { get; set; }
        public virtual ICollection<JobIndustry> JobIndustries { get; set; }
    }
}
