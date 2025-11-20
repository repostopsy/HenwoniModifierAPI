using DotLiquid;
using HenwoniDataModifierAPI.Areas.User.SystemServices;
using HenwoniDataModifierAPI.Areas.User.ViewModels;
using HenwoniDataModifierAPI.Common;
using HenwoniDataModifierAPI.Data;
using HenwoniDataModifierAPI.Models;
using HenwoniDataModifierAPI.Models.Common;
using HenwoniDataModifierAPI.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace HenwoniDataModifierAPI.Areas.User.Controllers
{
    [Route("user/jobtitles")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Area("User")]
    public class JobTitlesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IJWTTokenService _jwttokenservice;
        public JobTitlesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IJWTTokenService jWTTokenServices)
        {
            _userManager = userManager;
            _context = context;
            _jwttokenservice = jWTTokenServices;
        }

        [HttpGet("get")]
        public async Task<ActionResult<List<RefCommonJobTitleResponseViewModel>>> GetJobTitlesAsync([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery(Name ="s")] string? search)
        {
            var userName = User.Identity?.Name;
            // var user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            if (page == null || page==0) page = 1;
            if (pageSize == null) pageSize = 10;
            var titles = from s in _context.RefCommonJobTitles.Where(x=>x.ParentId==null || x.ParentId==0)
                           select s;
            if (!string.IsNullOrEmpty(search))
            {
                titles = titles.Where(s => s.Title.ToLower().Contains(search.ToLower()) || s.Excerpt.ToLower().Contains(search.ToLower()));
            }
            int from = (page.Value * pageSize.Value) - pageSize.Value;
            PaginatedList<RefCommonJobTitle> data = await PaginatedList<RefCommonJobTitle>.CreateAsync(titles, page ?? 1, pageSize ?? 10); // .AsNoTracking()
            List<RefCommonJobTitleResponseViewModel> t = new List<RefCommonJobTitleResponseViewModel>();
            foreach (var g in data.Content)
            {
                var b = RefCommonJobTitleResponseViewModel.From(g);
                if (g.ParentId!=null && g.ParentId>0)
                {
                    var parent = await _context.RefCommonJobTitles.Where(x => x.Id == g.ParentId).FirstOrDefaultAsync();
                    if (parent != null) b.ParentSystemName = parent.SystemName;
                }
                t.Add(b);
            }
            return Ok(t);
        }

        [HttpGet("{jobtitle}/get")]
        public async Task<ActionResult<List<RefCommonJobTitleResponseViewModel>>> GetJobtitleAsync([FromRoute] long id)
        {
            var jobtitle = await _context.RefCommonJobTitles.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (jobtitle == null) return NotFound();
            var f = RefCommonJobTitleResponseViewModel.From(jobtitle);
            if (jobtitle.Author != null) f.Author = ApplicationUserResponseViewModel.From(jobtitle.Author);
            return Ok(f);
        }

        [HttpGet("{jobtitle}/descriptions/get")]
        public async Task<ActionResult<List<RefCJTDescriptionTemplateResponseViewModel>>> GetJobTitlesAsync([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery(Name ="s")] string? search, [FromRoute] string? jobtitle)
        {
            var userName = User.Identity?.Name;
            if (page == null) page = 1;
            if (pageSize == null) pageSize = 10;
            var descriptions = from s in _context.RefCJTDescriptionTemplates.Where(x => x.ParentId == null || x.ParentId == 0)
                               select s;
            if (!string.IsNullOrEmpty(search))
            {
                descriptions = descriptions.Where(s => s.Title.ToLower().Contains(search.ToLower()) || s.Excerpt.ToLower().Contains(search.ToLower()));
            }
            if (!string.IsNullOrEmpty(jobtitle) && jobtitle!="all")
            {
                descriptions = descriptions.Where(s => s.RefCommonJobTitle.SystemName==jobtitle);
            }
            int from = (page.Value * pageSize.Value) - pageSize.Value;
            PaginatedList<RefCJTDescriptionTemplate> data = await PaginatedList<RefCJTDescriptionTemplate>.CreateAsync(descriptions, page ?? 1, pageSize ?? 10); // .AsNoTracking()
            List<RefCJTDescriptionTemplateResponseViewModel> k = new List<RefCJTDescriptionTemplateResponseViewModel>();
            foreach (var c in data.Content)
            {
                var f = RefCJTDescriptionTemplateResponseViewModel.From(c);
                k.Add(f);
            }
            return Ok(k);
        }

        [HttpGet("templates/{id}/get")]
        public async Task<ActionResult<List<RefCJTDescriptionTemplateResponseViewModel>>> GetRefCJTDescriptionTemplateAsync([FromRoute] long id)
        {
            var template = await _context.RefCJTDescriptionTemplates.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (template == null) return NotFound();
            var f = RefCJTDescriptionTemplateResponseViewModel.From(template);
            f.JobTitle = RefCommonJobTitleResponseViewModel.From(template.RefCommonJobTitle);
            if (template.Author != null) f.Author = ApplicationUserResponseViewModel.From(template.Author);
            return Ok(f);
        }

        [HttpGet("templates/bylanguage/get")]
        public async Task<ActionResult<List<RefCJTDescriptionTemplateResponseViewModel>>> GetRefCJTDescriptionTemplateByLanguageAsync([FromQuery] string language, [FromQuery] long serverId)
        {
            var userName = User.Identity?.Name;
            RefCJTDescriptionTemplate template = null;
            RefCJTDescriptionTemplate m = await _context.RefCJTDescriptionTemplates.Where(x => x.Id == serverId).FirstOrDefaultAsync();
            if (m == null) return NotFound();
            if (m.Language != null && m.Language.SystemName == language) template = m;
            if (template==null) template = await _context.RefCJTDescriptionTemplates.Where(x => x.ParentId == serverId && x.Language.SystemName == language && x.Author.UserName == userName).FirstOrDefaultAsync();
            if (template == null) return NotFound();
            var f = RefCJTDescriptionTemplateResponseViewModel.From(template);
            f.JobTitle = RefCommonJobTitleResponseViewModel.From(template.RefCommonJobTitle);
            if (template.Author != null) f.Author = ApplicationUserResponseViewModel.From(template.Author);
            return Ok(f);
        }

        [HttpPut("templates/update")]
        public async Task<ActionResult<RefCJTDescriptionTemplateResponseViewModel>> UpdateTemplateAsync(RefCJTDescriptionTemplateRequestViewModel request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var userName = User.Identity?.Name;
            var k = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            RefCJTDescriptionTemplate existing = null;
            if (request.ServerId!=null) existing = await _context.RefCJTDescriptionTemplates.Where(x => x.Id == request.ServerId).FirstOrDefaultAsync();
            if (existing==null)
            {
                existing = new RefCJTDescriptionTemplate()
                {
                    SystemName = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    Language = await _context.Languages.Where(z => z.SystemName == request.Language).FirstOrDefaultAsync(),
                    RefCommonJobTitle = await _context.RefCommonJobTitles.Where(x=>x.Id==request.JobTitle).FirstOrDefaultAsync(),
                    Author = k
                };
                if (request.ServerParentId!=null)
                {
                    var parent = await _context.RefCJTDescriptionTemplates.Where(x => x.Id == request.ServerParentId).FirstOrDefaultAsync();
                    if (parent != null) existing.ParentId = parent.Id;
                }
                await _context.RefCJTDescriptionTemplates.AddAsync(existing);
            }
            existing.DateUpdated = DateTime.UtcNow;
            existing.CopyPropertiesFrom(request);
            await _context.SaveChangesAsync();
            return RefCJTDescriptionTemplateResponseViewModel.From(existing);
        }

    }
}
