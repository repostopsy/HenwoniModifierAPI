﻿

namespace HenwoniDataModifierAPI.Models.Employment
{
	public class CommonJobBenefit
	{
		public long Id { get; set; }
		public string SystemName { get; set; }
		public string? Code { get; set; }
		public string Title { get; set; }
		public string? Excerpt { get; set; }
		public string? Description { get; set; }
		public bool IsDeleted { get; set; } = false;

    }
}
