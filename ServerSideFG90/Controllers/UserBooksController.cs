using Microsoft.AspNetCore.Mvc;
using ServerSideFG90.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerSideFG90.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBooksController : ControllerBase
    {
        // GET api/<UserBooksController/wishlist/{userID}
        [HttpGet("wishlist/{userID}")]
        public IEnumerable<Book> GetAllBooksOfWishlist(int userID)
        {
            return UserBook.GetAllBooks(userID);
        }
        // GET: api/<UserBooksController/readed/{id}>
        [HttpGet("readed/{userID}")]
        public IEnumerable<Book> GetAllReadBooks(int userID)
        {
            return UserBook.GetAllReadedBooks(userID);
        }
        // GET: api/<UserBooksController/readed/{id}>
        [HttpGet("sale/{userID}")]
        public List<BookWithOwner> GetAllBooksForSale(int userID)
        {
            return UserBook.GetAllSaleBooks(userID);
        }
        // GET api/UserBooks/recommend/{userID}
        [HttpGet("recommend/{userID}")]
        public IEnumerable<Book> GetRecommandedBooks(int userID)
        {
            return UserBook.GetAllRecommandedBooks(userID);
        }

        // POST api/UserBooks
        [HttpPost]
        public bool Post([FromQuery] int bookID, [FromQuery] int userID, [FromQuery] int bookPrice)
        {
            return UserBook.addBookToUser(userID, bookID, bookPrice);
        }
        // PUT api/<UserBooksController>/5
        [HttpPut("{userID}")]
        public bool Put(int userID, [FromBody] int bookID)
        {
            return UserBook.markAsReaded(userID, bookID);
        }


        // DELETE api/<UserBooksController>/5
        [HttpDelete]
        public bool Delete([FromQuery] int bookID, [FromQuery] int userID)
        {
            return UserBook.deleteBookOfUser(userID, bookID);
        }
    }
}
