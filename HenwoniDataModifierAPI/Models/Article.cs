
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HenwoniDataModifierAPI.Models
{
	public class Article
	{

		[JsonIgnore]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Name { get; set; }
		public string Permalink { get; set; }
		public string? Content { get; set; }
		public string? Excerpt { get; set; }
		public string? Notes { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
