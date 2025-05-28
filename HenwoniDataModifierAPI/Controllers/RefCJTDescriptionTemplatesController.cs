using Azure.Core;
using DotLiquid.Util;
using HenwoniDataModifierAPI.Data;
using HenwoniDataModifierAPI.Models.Common;
using HenwoniDataModifierAPI.Utilities;
using HenwoniDataModifierAPI.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static HenwoniDataModifierAPI.Controllers.RefCJTDescriptionTemplatesController;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HenwoniDataModifierAPI.Controllers
{
    [Route("api/refcjtdescriptiontemplates")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RefCJTDescriptionTemplatesController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public RefCJTDescriptionTemplatesController(ApplicationDbContext context) {
            _context = context;
        }

        /// <summary>
        /// Method updates template or creates it if it does not exit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<RefCJTDescriptionTemplate>> UpdateTemplateDescriptionAsync(RefCJTDescriptionTemplateRequest request)
        {
            RefCJTDescriptionTemplate existing = await _context.RefCJTDescriptionTemplates.Where(x => x.SystemName == request.SystemName).FirstOrDefaultAsync();
            if (existing==null)
            {
                // Create it.
                existing = new RefCJTDescriptionTemplate();
                _context.RefCJTDescriptionTemplates.Add(existing);
            }
            existing.CopyPropertiesFrom(request);
            var v = await _context.RefCommonJobTitles.Where(x => x.SystemName == request.CommonJobTitle).ToListAsync();
            var r = v.FirstOrDefault();
            if (v.Count()>1 || v.Count()==0)
            {
                r = await _context.RefCommonJobTitles.Where(x => x.SystemName == request.CJTitle).FirstOrDefaultAsync();
            }
            existing.RefCommonJobTitle = r;
            if (!String.IsNullOrEmpty(request.Language))
            {
                existing.Language = await _context.Languages.Where(x => x.SystemName == request.Language).FirstOrDefaultAsync();
            }
            if (!String.IsNullOrEmpty(request.Parent))
            {
                var parent = await _context.RefCJTDescriptionTemplates.Where(x => x.SystemName == request.Parent).FirstOrDefaultAsync();
                if (parent != null) {
                    existing.ParentId = parent.Id;
                }else
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

        /*[HttpGet("")]
        public async Task<IEnumerable<RefCJTDescriptionTemplate>> GetAsync()
        {
            List<RefCJTDescriptionTemplate> templates = await _context.RefCJTDescriptionTemplates.ToListAsync();
            return templates;
        }*/

        [HttpGet("/{systemName}")]
        public async Task<ActionResult<RefCJTDescriptionTemplate>> GetTemplateAsync(string systemName)
        {
            RefCJTDescriptionTemplate template = await _context.RefCJTDescriptionTemplates.Where(x=>x.SystemName==systemName).FirstOrDefaultAsync();
            if (template != null) return NotFound();
            return template;
        }
    }
}
