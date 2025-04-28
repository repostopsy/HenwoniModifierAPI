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
		public String Template { get; set; }
		public String Notes { get; set; }

        [JsonIgnore]
        public virtual RefCommonJobTitle RefCommonJobTitle { get; set; }
        public virtual JobLevel? JobLevel { get; set; }
        public virtual Language? Language { get; set; }
    }
}
