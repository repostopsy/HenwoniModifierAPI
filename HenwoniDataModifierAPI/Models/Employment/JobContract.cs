
using HenwoniDataModifierAPI.Models.Employment;



namespace HenwoniDataModifierAPI.Models.Employment
{
    public class JobContract
	{
		public long Id { get; set; }
        public long Duration { get; set; } // Seconds
        public DateTime StartDate { get; set; } // Seconds
        public DateTime EndDate { get; set; } // Seconds
        public bool ContractCompleted { get; set; }
		public virtual JobContractType JobContractType { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
