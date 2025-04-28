using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenwoniDataModifierAPI.Models.HelpSupport
{
    public class SupportTicketDepartment
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string SystemName { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}
