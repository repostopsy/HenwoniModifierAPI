
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenwoniDataModifierAPI.Models.Services
{
	public class ServiceTemplateEntertainmentForm
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string SystemName { get; set; }
		public string? Excerpt { get; set; }
		public string? Content { get; set; }
		public bool IsDeleted { get; set; } = false;
		public virtual ServiceTemplateEntertainmentType EntertainmentType { get; set; }
		public long EntertainmentTypeId { get; set; }
	}
}
