using HenwoniDataModifierAPI.Areas.User.SystemServices;
using HenwoniDataModifierAPI.Areas.User.ViewModels;
using HenwoniDataModifierAPI.Common;
using HenwoniDataModifierAPI.Data;
using HenwoniDataModifierAPI.Models.Common;
using HenwoniDataModifierAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Utilities;

namespace HenwoniDataModifierAPI.Areas.User.Controllers
{
    [Route("user/translations")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Area("User")]
    public class TranslationsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IJWTTokenService _jwttokenservice;
        public TranslationsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IJWTTokenService jWTTokenServices)
        {
            _userManager = userManager;
            _context = context;
            _jwttokenservice = jWTTokenServices;
        }

        [HttpGet("get")]
        public async Task<ActionResult<List<TranslationsResponseViewModel>>> GetTranslationsAsync([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery(Name = "s")] string? search)
        {
            var userName = User.Identity?.Name;
            if (page == null || page == 0) page = 1;
            if (pageSize == null) pageSize = 10;
            var translations = from s in _context.Translations.Where(x=>x.ParentId==null || x.ParentId ==0)
                                select s;
            if (!string.IsNullOrEmpty(search))
            {
                translations = translations.Where(s => s.Title.ToLower().Contains(search.ToLower()) || s.Excerpt.ToLower().Contains(search.ToLower()));
            }
            int from = (page.Value * pageSize.Value) - pageSize.Value;
            PaginatedList<Translation> data = await PaginatedList<Translation>.CreateAsync(translations, page ?? 1, pageSize ?? 10); // .AsNoTracking()
            List<TranslationsResponseViewModel> t = new List<TranslationsResponseViewModel>();
            foreach (var g in data.Content)
            {
                var b = TranslationsResponseViewModel.From(g);
                if (g.ParentId != null && g.ParentId > 0)
                {
                    var parent = await _context.Translations.Where(x => x.Id == g.ParentId).FirstOrDefaultAsync();
                    if (parent != null) b.ParentId = parent.Id;
                }
                t.Add(b);
            }
            return Ok(t);
        }

        [HttpGet("{id}/get")]
        public async Task<ActionResult<List<TranslationsResponseViewModel>>> GetTranslationsAsync([FromRoute] long id)
        {
            var userName = User.Identity?.Name;
            var transilation = await _context.Translations.Where(x => x.Id == id && (x.ParentId==null || x.ParentId==0)).FirstOrDefaultAsync();
            if (transilation == null) return NotFound();
            var b = TranslationsResponseViewModel.From(transilation);
            if (transilation.ParentId != null && transilation.ParentId > 0)
            {
                var parent = await _context.Translations.Where(x => x.Id == transilation.ParentId).FirstOrDefaultAsync();
                if (parent != null) b.ParentId = parent.Id;
            }
            return Ok(b);
        }

        [HttpGet("bylanguage/get")]
        public async Task<ActionResult<List<TranslationsResponseViewModel>>> GetRefCJTDescriptionTemplateByLanguageAsync([FromQuery] string language, [FromQuery] long serverId)
        {
            var userName = User.Identity?.Name;
            Translation transilation = null;
            Translation m = await _context.Translations.Where(x => x.Id == serverId).FirstOrDefaultAsync();
            if (m == null) return NotFound();
            if (m.Language != null && m.Language.SystemName == language) transilation = m;
            var currentUser = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            if (transilation == null) transilation = await _context.Translations.Where(x => x.ParentId == serverId && x.Language.SystemName == language && x.Author == currentUser).FirstOrDefaultAsync();
            if (transilation == null)
            {
                // Create it
                transilation = new Translation();
                transilation.CopyPropertiesFrom(m);
                transilation.Text = "";
                transilation.Id = 0;
                transilation.ParentId = m.Id;
                transilation.Language = await _context.Languages.Where(x => x.SystemName == language).FirstOrDefaultAsync();
                transilation.Author = currentUser;
                transilation.DefaultLanguageText = m.DefaultLanguageText;
                if (transilation.Language == null) return NotFound("Language not found");
                await _context.Translations.AddAsync(transilation);
                await _context.SaveChangesAsync();
            }
            var f = TranslationsResponseViewModel.From(transilation);
            if (transilation.Author != null) f.Author = ApplicationUserResponseViewModel.From(transilation.Author);
            return Ok(f);
        }

        [HttpPut("update")]
        public async Task<ActionResult<TranslationsResponseViewModel>> UpdateTransilationAsync(TransilationRequestViewModel request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var userName = User.Identity?.Name;
            var k = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            Translation existing = null;
            if (request.ServerId != null) existing = await _context.Translations.Where(x => x.Id == request.ServerId).FirstOrDefaultAsync();
            if (existing == null)
            {
                existing = new Translation()
                {
                    SystemName = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    Language = await _context.Languages.Where(z => z.SystemName == request.Language).FirstOrDefaultAsync(),
                    Author = k
                };
                if (request.ServerParentId != null)
                {
                    var parent = await _context.Translations.Where(x => x.Id == request.ServerParentId).FirstOrDefaultAsync();
                    if (parent != null)
                    {
                        existing.ParentId = parent.Id;
                        existing.SystemContextIdentity = parent.SystemContextIdentity;
                    }
                }
                await _context.Translations.AddAsync(existing);
            }else if (existing.ParentId==null || existing.ParentId==0)
            {
                return BadRequest("Original copy cannot be modified");
            }
            existing.CopyPropertiesFrom(request);
            existing.DateUpdated = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return TranslationsResponseViewModel.From(existing);
        }

    }
}
