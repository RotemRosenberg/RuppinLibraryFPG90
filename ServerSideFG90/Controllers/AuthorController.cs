using Microsoft.AspNetCore.Mvc;
using ServerSideFG90.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerSideFG90.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        // GET: api/<AuthorController>
        [HttpGet]
        public IEnumerable<Author> GetAll()
        {
            return Author.ReadAll();
        }

        // GET api/<AuthorController>/5
        [HttpGet("{name}")]
        public IEnumerable<Author> Get(string name)
        {
            return Author.ReadAuthorsByName(name);
        }

        // POST api/<AuthorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
