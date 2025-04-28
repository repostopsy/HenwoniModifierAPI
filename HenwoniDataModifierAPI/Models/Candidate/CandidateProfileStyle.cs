using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenwoniDataModifierAPI.Models.Candidate
{
    public class CandidateProfileStyle
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string SystemName { get; set; }
        /// <summary>
        /// Short 1 line description
        /// </summary>
        public string? Excerpt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
