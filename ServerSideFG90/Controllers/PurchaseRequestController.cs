using Microsoft.AspNetCore.Mvc;
using ServerSideFG90.BL;
using ServerSideFG90.DAL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerSideFG90.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseRequestController : ControllerBase
    {
        // GET: api/<PurchaseRequestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PurchaseRequestsController>/5
        [HttpGet("{userID}")]
        public List<BookWithOwner> GetPurchaseRequestsOfUser(int userID)
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllPurchaseRequests(userID);

        }
        [HttpPost("request")]
        public bool RequestPurchase([FromBody] PurchaseRequestModel model)
        {
            var newRequest = new PurchaseRequestModel
            {
                BookID = model.BookID,
                SenderID = model.SenderID,
                ReceiverID = model.ReceiverID
            };

            DBservices dbs = new DBservices();
            return dbs.sendPurchaseRequest(newRequest);
        }

        // PUT api/<PurchaseRequestsController>/5
        [HttpPut("{bookPrice}")]
        public bool Put([FromBody] PurchaseRequestModel model, int bookPrice)
        {
            int bookID = model.BookID;
            int ownerID = model.ReceiverID;
            DBservices dbs = new DBservices();
            return dbs.ApprovePurchaseRequest(ownerID, bookID, bookPrice);
        }

        // DELETE api/<PurchaseRequestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
