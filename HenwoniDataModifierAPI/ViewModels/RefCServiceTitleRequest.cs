using System.ComponentModel.DataAnnotations.Schema;
using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Models.Services.Common;
using System.Text.Json.Serialization;
using HenwoniDataModifierAPI.Models.Common;
using HenwoniDataModifierAPI.Models.Employment;
using HenwoniDataModifierAPI.Models.Skills;

namespace HenwoniDataModifierAPI.ViewModels
{
    public class RefCServiceTitleRequest
    {
        public RefCServiceTitleRequest()
        {
            JobIndustries = new List<JobIndustry>();
            CandidateSkills = new List<CandidateSkill>();
            CandidateSoftSkills = new List<CandidateSoftSkill>();
        }
        public string SystemName { get; set; }
        public string Title { get; set; }
        public string? Parent { get; set; }
        public string? PluralTitle { get; set; }
        public string? ServiceCategory { get; set; }
        public string Language { get; set; }
        public double? AverageSalary { get; set; }
        public string? Excerpt { get; set; }
        public string? Description { get; set; }
        public List<JobIndustry> JobIndustries { get; set; }
        public List<CandidateSkill> CandidateSkills { get; set; }
        public List<CandidateSoftSkill> CandidateSoftSkills { get; set; }
    }
}
