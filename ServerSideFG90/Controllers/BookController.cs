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
        // GET api/<BookController>/5
        [HttpGet("BookedBooks")]
        public IEnumerable<Book> BookedBooks()
        {
            return Book.BookedBooks();
        }
        // GET api/<BookController>/5
        [HttpGet("BookedAuthors")]
        public IEnumerable<AuthorBooked> BookedAuthors()
        {
            return Book.BookedAuthors();
        }
        // GET api/<BookController>/5
        [HttpGet("BookedUsers")]
        public IEnumerable<UsersBooked> BookedUsers()
        {
            return Book.BookedUsers();
        }
        // GET api/<BookController>/5
        [HttpGet("Admin")]
        public IEnumerable<Book> ReadAdmin()
        {
            return Book.ReadAllAdmin();
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
            else if (type == "category")
            {
                List<Book> books = Book.readBooksByCategory(query);
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
        public Book Post([FromBody] Book book)
        {
            return book.AddBook();
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Book book)
        {
            return book.Update();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (Book.Delete(id))
                return Ok(id);
            else return NotFound("There is no Course with this id:" + id);
        }
    }
}
