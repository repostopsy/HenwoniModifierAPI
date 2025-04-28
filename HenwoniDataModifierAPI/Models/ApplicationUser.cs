using HenwoniDataModifierAPI.Models.Location;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HenwoniDataModifierAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
    }
}
