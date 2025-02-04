using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JAT.XModuleManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModulesController : ControllerBase
{
    // GET: api/<ModulesController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<ModulesController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<ModulesController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<ModulesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ModulesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
