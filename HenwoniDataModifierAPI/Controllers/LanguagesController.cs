using HenwoniDataModifierAPI.Data;
using HenwoniDataModifierAPI.Models.Common;
using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Utilities;
using HenwoniDataModifierAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HenwoniDataModifierAPI.Controllers
{
    [Route("api/languages")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public LanguagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Language>> UpdateTemplateDescriptionAsync(LanguagePostRequest request)
        {
            Language existing = await _context.Languages.Where(x => x.SystemName == request.SystemName).FirstOrDefaultAsync();
            if (existing == null)
            {
                // Create it.
                existing = new Language();
                _context.Languages.Add(existing);
            }
            existing.CopyPropertiesFrom(request);
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
