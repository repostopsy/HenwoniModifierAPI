using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Models.Pricing;

namespace HenwoniDataModifierAPI.Models.Common
{
    public class RefCommonJobTitleSalary
    {
        [Key]
        public long Id { get; set; }
        public string LocationDomain { get; set; }
        public virtual Currency Currency { get; set; }
        [JsonIgnore]
        public virtual Country Country { get; set; }
        [JsonIgnore]
        public virtual State? State { get; set; }
        [JsonIgnore]
        public virtual City? City { get; set; }
        [JsonIgnore]
        public virtual RefCommonJobTitle JobTitle { get; set; }
    }
}
