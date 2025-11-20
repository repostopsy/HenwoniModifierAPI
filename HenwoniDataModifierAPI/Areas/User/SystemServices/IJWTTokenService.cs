using HenwoniDataModifierAPI.Areas.User.Models;
using HenwoniDataModifierAPI.Models;
using Newtonsoft.Json.Linq;

namespace HenwoniDataModifierAPI.Areas.User.SystemServices
{
    public interface IJWTTokenService
    {
        JWTToken Authenticate(ApplicationUser users);
        string CreateToken(ApplicationUser user);
        string CreateToken2(ApplicationUser userInDb);
    }
}
