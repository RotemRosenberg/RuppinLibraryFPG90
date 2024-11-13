using ServerSideFG90.DAL;

namespace ServerSideFG90.BL
{
    public class UserBook
    {
        int userID;
        int bookID;

        public UserBook(int userID, int bookID)
        {
            UserID = userID;
            BookID = bookID;
        }
        public int UserID { get => userID; set => userID = value; }
        public int BookID { get => bookID; set => bookID = value; }

        public static bool addBookToUser(int userID, int bookID, int bookPrice)
        {
            DBservices dbs = new DBservices();
            return dbs.addBookToUserDB(userID, bookID,bookPrice);
        }

        public static List<Book> GetAllBooks(int userID)
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllBooksDB(userID);

        }
        public static List<Book> GetAllReadedBooks(int userID)
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllReadedBooksDB(userID);
        }
        public static List<Book> GetAllRecommandedBooks(int userID)
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllRecommandedBooksDB(userID);
        }
            public static bool markAsReaded(int userID, int bookID)
        {
            DBservices dbs = new DBservices();
            return dbs.markAsReadedDB(userID, bookID);
        }
        public static bool deleteBookOfUser(int userID, int bookID)
        {
            DBservices dbs = new DBservices();
            return dbs.deleteBookOfUserDB(userID, bookID);
        }
        public static List<BookWithOwner> GetAllSaleBooks(int userID)
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllSaleBooksDB(userID);

        }
    }
}
