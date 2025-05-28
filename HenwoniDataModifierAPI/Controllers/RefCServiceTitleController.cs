using HenwoniDataModifierAPI.Data;
using HenwoniDataModifierAPI.Models.Common;
using HenwoniDataModifierAPI.Models.Services.Common;
using HenwoniDataModifierAPI.Utilities;
using HenwoniDataModifierAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HenwoniDataModifierAPI.Controllers
{
    [Route("api/refcservicetitles")]
    [ApiController]
    public class RefCServiceTitleController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public RefCServiceTitleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{systemName}")]
        public async Task<ActionResult<RefCServiceTitle>> GetRefCServiceTitleAsync(string systemName)
        {
            RefCServiceTitle title = await _context.RefCServiceTitles.Where(x => x.SystemName == systemName).FirstOrDefaultAsync();
            if (title == null) return NotFound();
            return title;
        }

        [HttpPost()]
        public async Task<ActionResult<RefCServiceTitle>> UpdateRefCServiceTitleTemplateAsync(RefCServiceTitleRequest request)
        {
            RefCServiceTitle existing = await _context.RefCServiceTitles.Where(x => x.SystemName == request.SystemName).FirstOrDefaultAsync();
            if (existing == null)
            {
                // Create it.
                existing = new RefCServiceTitle();
                await _context.RefCServiceTitles.AddAsync(existing);
            }
            existing.CopyPropertiesFrom(request);
            existing.ServiceCategory = await _context.ServiceCategories.Where(x => x.SystemName == request.ServiceCategory).FirstOrDefaultAsync();
            existing.DateUpdated = DateTime.UtcNow;
            if (!String.IsNullOrEmpty(request.Language))
            {
                existing.Language = await _context.Languages.Where(x => x.SystemName == request.Language).FirstOrDefaultAsync();
            }
            if (!String.IsNullOrEmpty(request.Parent))
            {
                var parent = await _context.RefCJTDescriptionTemplates.Where(x => x.SystemName == request.Parent).FirstOrDefaultAsync();
                if (parent != null)
                {
                    existing.ParentId = parent.Id;
                }
                else
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Description parent not found"
                    });
                }
            }
            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpPost("templates")]
        public async Task<ActionResult<RefCServiceTitleTemplate>> UpdateRefCServiceTitleTemplateAsync(RefCServiceTitleTemplateRequest request)
        {
            RefCServiceTitleTemplate existing = await _context.RefCServiceTitleTemplates.Where(x => x.SystemName == request.SystemName).FirstOrDefaultAsync();
            if (existing == null)
            {
                // Create it.
                existing = new RefCServiceTitleTemplate();
                _context.RefCServiceTitleTemplates.Add(existing);
            }
            existing.CopyPropertiesFrom(request);
            existing.RefCServiceTitle = await _context.RefCServiceTitles.Where(x => x.SystemName == request.RefCServiceTitle).FirstOrDefaultAsync();
            if (existing.RefCServiceTitle==null)
            {
                existing.RefCServiceTitle = new RefCServiceTitle()
                {

                };
            }
            if (!String.IsNullOrEmpty(request.Language))
            {
                existing.Language = await _context.Languages.Where(x => x.SystemName == request.Language).FirstOrDefaultAsync();
            }
            if (!String.IsNullOrEmpty(request.Parent))
            {
                var parent = await _context.RefCJTDescriptionTemplates.Where(x => x.SystemName == request.Parent).FirstOrDefaultAsync();
                if (parent != null)
                {
                    existing.ParentId = parent.Id;
                }
                else
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Description parent not found"
                    });
                }
            }
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
