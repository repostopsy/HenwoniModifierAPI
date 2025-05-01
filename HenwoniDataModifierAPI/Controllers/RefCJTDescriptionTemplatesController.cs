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
            var existing = await _context.RefCJTDescriptionTemplates.Where(x => x.SystemName == request.SystemName).FirstOrDefaultAsync();
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
            await _context.SaveChangesAsync();
            return existing;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RefCJTDescriptionTemplatesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT api/<RefCJTDescriptionTemplatesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RefCJTDescriptionTemplatesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
