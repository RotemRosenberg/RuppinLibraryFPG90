using Microsoft.AspNetCore.Mvc;
using ServerSideFG90.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerSideFG90.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        // GET: api/<BookController>
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return Book.ReadAll();
        }

        // GET api/<BookController>/5
        [HttpGet("Top5")]
        public IEnumerable<Book> Top5()
        {
            return Book.Top5();
        }

        // GET api/<BookController>/id/5
        [HttpGet("id/{id}")]
        public Book GetSpecificBook(int id)
        {
            return Book.GetBookById(id);
        }

        // GET api/<BookController>/search
        [HttpGet("{query}")]
        public ActionResult<IEnumerable<Book>> SearchBooks(string query, string type)
        {
            if (type == "title")
            {
                List<Book> books = Book.readBooksByTitle(query);
                if (books == null || books.Count == 0)
                {
                        return NotFound();
                }
                    return Ok(books);
                }
            else if (type == "author")
            {
                List<Book> books = Book.readBooksByAuthor(query);
                if (books == null || books.Count == 0)
                {
                    return NotFound();
                }
                return Ok(books);
            }
            else if (type == "text")
            {
                List<Book> books = Book.readBooksByText(query);
                if (books == null || books.Count == 0)
                {
                    return NotFound();
                }
                return Ok(books);
            }
            return BadRequest("Invalid type");
        }

        // POST api/<BookController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
