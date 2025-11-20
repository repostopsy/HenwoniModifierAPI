using HenwoniDataModifierAPI.Models.Employment;
using HenwoniDataModifierAPI.Models.Skills;
using HenwoniDataModifierAPI.Models.Employment;
using HenwoniDataModifierAPI.Models.Skills;
using System.Text.Json.Serialization;
using HenwoniDataModifierAPI.Common.Models;


using System.ComponentModel.DataAnnotations;
using HenwoniDataModifierAPI.Models.Location;

namespace HenwoniDataModifierAPI.Models.Common
{
	/// <summary>
	/// Will be used to store a database of common job roles.
	/// </summary>
	public class RefCommonJobTitle
    {
        public RefCommonJobTitle() { }
        [JsonIgnore]
        [Key]
		public long Id { get; set; }
		public long ParentId { get; set; }
		public string SystemName { get; set; }
        [JsonIgnore]
        public string? Code { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public string? JobDescriptionTemplate { get; set; }
        [JsonIgnore]
        public string? PluralTitle { get; set; }
        [JsonIgnore]
        public double? AverageSalary { get; set; }
        [JsonIgnore]
        public string? Excerpt { get; set; }
        [JsonIgnore]
        public string? Description { get; set; }
        [JsonIgnore]
        public DateTime DateCreated { get; set; }
        [JsonIgnore]
        public DateTime DateUpdated { get; set; }
        public virtual ApplicationUser? Author { get; set; }
        public bool Approved { get; set; }
        public double Rating { get; set; }
        [JsonIgnore]
		public virtual ICollection<JobIndustry> JobIndustries { get; set; }
        [JsonIgnore]
        public virtual ICollection<CandidateSkill> CandidateSkills { get; set; }
        [JsonIgnore]
        public virtual ICollection<RefCJTDescriptionTemplate> Descriptions { get; set; }
        [JsonIgnore]
        public virtual ICollection<RefCommonJobTitleBenefit> Benefits { get; set; }
        [JsonIgnore]
        public virtual JobLevel? JobLevel { get; set; }
        [JsonIgnore]
        public virtual Language? Language { get; set; }
        [JsonIgnore]
        public virtual ICollection<RefCommonJobTitleSalary> Salaries { get; set; }
    }
}
