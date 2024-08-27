using Microsoft.AspNetCore.Mvc;
using ServerSideFG90.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerSideFG90.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookReviewController : ControllerBase
    {
        // GET: api/<BookReviewController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BookReviewController>/5
        [HttpGet("{id}")]
        public IEnumerable<BookReview> Get(int id)
        {
            return BookReview.ReadBookReview(id);
        }

        // POST api/<BookReviewController>
        [HttpPost]
        public bool Post([FromBody] BookReview bookReview)
        {
            return bookReview.AddBookReview();
        }

        // PUT api/<BookReviewController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookReviewController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
