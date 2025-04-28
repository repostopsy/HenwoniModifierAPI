using DotLiquid;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HenwoniDataModifierAPI.Controllers
{
    [Route("api/jobtitles")]
    [ApiController]
    public class JobTitlesController : ControllerBase
    {
        public JobTitlesController() { }

        [HttpGet]
        public string Get()
        {
            Template template = Template.Parse("hi {{name}}"); // Parses and compiles the template
            string v = template.Render(Hash.FromAnonymousObject(new { name = "tobi" }));
            return v;
        }

        // GET api/<JobTitlesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<JobTitlesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<JobTitlesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<JobTitlesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
