using Microsoft.AspNetCore.Identity;

namespace HenwoniDataModifierAPI
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
    }
}
