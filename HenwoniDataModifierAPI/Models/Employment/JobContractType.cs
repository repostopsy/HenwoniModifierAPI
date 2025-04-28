using System.ComponentModel.DataAnnotations;

namespace HenwoniDataModifierAPI.Models.Employment
{
	public class JobContractType
	{
		public long Id { get; set; }
		public string SystemName { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public bool IsDeleted { get; set; } = false;
    }
}
