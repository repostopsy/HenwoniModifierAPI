using HenwoniDataModifierAPI.Models.Location;

namespace HenwoniDataModifierAPI.Models.Employment
{
    public class JobFinanceSchemeType
    {
        public static string SalaryWageCompensationName = "SalaryWageCompensation";
        public static string GeneralPartnershipName = "GeneralPartnership";
        public static string LimitedPartnershipName = "LimitedPartnership";
        public static string ProfitSharingPartnershipName = "ProfitSharingPartnership";
        public static string EmployeeStockOwnershipName = "EmployeeStockOwnership";
        public int Id { get; set; }
        public string SystemName { get; set; }
        public long? ParentId { get; set; }
        public virtual Language Language { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public string? Excerpt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
