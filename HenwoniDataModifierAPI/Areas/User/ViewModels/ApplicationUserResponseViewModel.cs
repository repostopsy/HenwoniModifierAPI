using HenwoniDataModifierAPI.Models;
using HenwoniDataModifierAPI.Utilities;

namespace HenwoniDataModifierAPI.Areas.User.ViewModels
{
    public class ApplicationUserResponseViewModel
    {
        public string Id { get; internal set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        // public string Email { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public static ApplicationUserResponseViewModel From(ApplicationUser fromUser)
        {
            ApplicationUserResponseViewModel auvm = new ApplicationUserResponseViewModel();
            auvm.CopyPropertiesFrom(fromUser);
            return auvm;
        }
    }
}
