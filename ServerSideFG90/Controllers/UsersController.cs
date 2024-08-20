using Microsoft.AspNetCore.Mvc;
using ServerSideFG90.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerSideFG90.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            return Users.ReadAll();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public Users Get(int id)
        {
            return Users.GetUserById(id);
        }

        // POST api/<UserController>
        [HttpPost("register")]
        public Users Register([FromBody] Users user)
        {
            return user.RegisterUser();
        }
        // POST api/<UserController>/login
        [HttpPost("login")]
        public Users Login(string email, [FromBody] string password)
        {

            Users user = BL.Users.Login(email, password);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
