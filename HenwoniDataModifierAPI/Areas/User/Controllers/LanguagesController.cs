using HenwoniDataModifierAPI.Areas.User.SystemServices;
using HenwoniDataModifierAPI.Areas.User.ViewModels;
using HenwoniDataModifierAPI.Common;
using HenwoniDataModifierAPI.Data;
using HenwoniDataModifierAPI.Models;
using HenwoniDataModifierAPI.Models.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HenwoniDataModifierAPI.Areas.User.Controllers
{
    [Route("user/languages")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Area("User")]
    public class LanguagesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IJWTTokenService _jwttokenservice;
        public LanguagesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IJWTTokenService jWTTokenServices)
        {
            _userManager = userManager;
            _context = context;
            _jwttokenservice = jWTTokenServices;
        }

        [HttpGet("get")]
        public async Task<ActionResult<List<LanguageResponseViewModel>>> GetLanguagesAsync()
        {
            var userName = User.Identity?.Name;
            var e = await _context.Languages.ToListAsync();
            List<LanguageResponseViewModel> el = new List<LanguageResponseViewModel>();
            foreach (var c in e)
            {
                c.Active = true;
                el.Add(LanguageResponseViewModel.From(c));
            }
            await _context.SaveChangesAsync();
            return Ok(el);
        }
    }
}
