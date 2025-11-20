using HenwoniDataModifierAPI.Models.Employment;
using HenwoniDataModifierAPI.Models.Location;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HenwoniDataModifierAPI.Models.Common
{
	public class RefCJTDescriptionTemplate
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
		public String SystemName { get; set; }
		public String Title { get; set; }
		public String? Excerpt { get; set; }
        [Column(TypeName = "text")]
        public String Template { get; set; }
        [Column(TypeName = "text")]
        public String? Notes { get; set; }

        [JsonIgnore]
        public virtual RefCommonJobTitle RefCommonJobTitle { get; set; }
        public virtual JobLevel? JobLevel { get; set; }
        public virtual Language? Language { get; set; }
        public virtual ApplicationUser? Author { get; set; }
        public bool Approved { get; set; }
        public double Rating { get; set; }
        /// <summary>
        /// If is original ParentId is null
        /// </summary>
        public long? ParentId { get; set; }
        public virtual ICollection<RefCJTDescriptionTemplateTag> Tags { get; }
        public virtual ICollection<RefCJTDescriptionTemplateAlias> Aliases { get; }
        public virtual ICollection<RefCJTDTemplateResponsibility> Responsibilities { get; set; }
        public virtual ICollection<RefCJTDTemplateSkillExperience> SkillsExperiences { get; set; }
        public virtual ICollection<RefCJTDTemplateIntro> Intros { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; }
    }
}
