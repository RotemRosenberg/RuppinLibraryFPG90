using ServerSideFG90.DAL;

namespace ServerSideFG90.BL
{
    public class BookReview
    {
        int userId;
        int bookId;
        double rating;
        string review;
        string userName;

        public BookReview() { }
        public BookReview(int userId, int bookId, double rating, string review, string userName)
        {
            UserId = userId;
            BookId = bookId;
            Rating = rating;
            Review = review;
            UserName = userName;
        }

        public int UserId { get => userId; set => userId = value; }
        public int BookId { get => bookId; set => bookId = value; }
        public double Rating { get => rating; set => rating = value; }
        public string Review { get => review; set => review = value; }
        public string UserName { get => userName; set => userName = value; }

        public bool AddBookReview()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertBookReview(this);
        }
        public static List<BookReview> ReadBookReview(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadBookReview(id);
        }
    }
}
