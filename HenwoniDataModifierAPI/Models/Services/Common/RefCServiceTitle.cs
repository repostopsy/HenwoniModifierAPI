using HenwoniDataModifierAPI.Models.Common;
using HenwoniDataModifierAPI.Models.Employment;
using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Models.Skills;
using System.Text.Json.Serialization;

namespace HenwoniDataModifierAPI.Models.Services.Common
{
    public class RefCServiceTitle
    {
        public RefCServiceTitle()
        {

            JobIndustries = new HashSet<JobIndustry>();
            Descriptions = new HashSet<RefCJTDescriptionTemplate>();
            CandidateSkills = new HashSet<CandidateSkill>();
        }
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string SystemName { get; set; }
        public string Title { get; set; }
        public string? PluralTitle { get; set; }
        public double? AverageSalary { get; set; }
        public string? Excerpt { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; }
        public virtual Language Language { get; set; }

        [JsonIgnore]
        public virtual ICollection<JobIndustry> JobIndustries { get; set; }

        [JsonIgnore]
        public virtual ICollection<RefCServiceTitleTemplate> RefCServiceTitleTemplates { get; set; }

        [JsonIgnore]
        public virtual ServiceCategory? ServiceCategory { get; set; }

        [JsonIgnore]
        public virtual ICollection<CandidateSkill> CandidateSkills { get; set; }

        [JsonIgnore]
        public virtual ICollection<CandidateSoftSkill> CandidateSoftSkills { get; set; }

        [JsonIgnore]
        public virtual ICollection<RefCJTDescriptionTemplate> Descriptions { get; set; }
    }
}
