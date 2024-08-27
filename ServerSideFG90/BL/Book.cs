using ServerSideFG90.DAL;

namespace ServerSideFG90.BL
{
    public class Book
    {
        int id;
        string title;
        string subTitle;
        int[] authorsID = new int[3];
        string publisher;
        string description;
        int pageCount;
        string categories;
        double averageRating;
        string smallPicURL;
        string picURL;
        string language;
        string preivewLink;
        bool isEbook;
        string webReaderLink;
        int price;
        int publishedDate;
        string[] authorNames = new string[3];
        int isBooked;

        public Book() { }

        public Book(int id, string title, string subTitle, int[] authorsID, string publisher, string description, int pageCount, string categories, double averageRating, string smallPicURL, string picURL, string language, string preivewLink, bool isEbook, string webReaderLink, int price, int publishedDate, string[] authorNames, int isBooked)
        {
            Id = id;
            Title = title;
            SubTitle = subTitle;
            AuthorsID = authorsID;
            Publisher = publisher;
            Description = description;
            PageCount = pageCount;
            Categories = categories;
            AverageRating = averageRating;
            SmallPicURL = smallPicURL;
            PicURL = picURL;
            Language = language;
            PreivewLink = preivewLink;
            IsEbook = isEbook;
            WebReaderLink = webReaderLink;
            Price = price;
            PublishedDate = publishedDate;
            AuthorNames = authorNames;
            IsBooked = isBooked;
        }

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string SubTitle { get => subTitle; set => subTitle = value; }
        public int[] AuthorsID { get => authorsID; set => authorsID = value; }
        public string Publisher { get => publisher; set => publisher = value; }
        public string Description { get => description; set => description = value; }
        public int PageCount { get => pageCount; set => pageCount = value; }
        public string Categories { get => categories; set => categories = value; }
        public double AverageRating { get => averageRating; set => averageRating = value; }
        public string SmallPicURL { get => smallPicURL; set => smallPicURL = value; }
        public string PicURL { get => picURL; set => picURL = value; }
        public string Language { get => language; set => language = value; }
        public string PreivewLink { get => preivewLink; set => preivewLink = value; }
        public bool IsEbook { get => isEbook; set => isEbook = value; }
        public string WebReaderLink { get => webReaderLink; set => webReaderLink = value; }
        public int Price { get => price; set => price = value; }
        public int PublishedDate { get => publishedDate; set => publishedDate = value; }
        public string[] AuthorNames { get => authorNames; set => authorNames = value; }
        public int IsBooked { get => isBooked; set => isBooked = value; }

        public static List<Book> ReadAll()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadAllBooks();
        }
        public static Book GetBookById(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.BookById(id);
        }
        public static List<Book> readBooksByTitle(string title)
        {
            DBservices dbs = new DBservices();
            return dbs.BooksByTitle(title);
        }
        public static List<Book> readBooksByText(string text)
        {
            DBservices dbs = new DBservices();
            return dbs.BooksByText(text);
        }
        public static List<Book> readBooksByAuthor(string author)
        {
            DBservices dbs = new DBservices();
            return dbs.BooksByAuthor(author);
        }
        public static List<Book> readBooksByCategory(string category)
        {
            DBservices dbs = new DBservices();
            return dbs.BooksByCategory(category);
        }
         public static List<Book> ReadAllAdmin()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadAllBooksAdmin();
        }
        public static List<Book> Top5()
        {
            DBservices dbs = new DBservices();
            return dbs.Top5Books();
        }
        public Book AddBook()
        {
            DBservices dbs = new DBservices();
             return dbs.InsertBook(this);

        }
        public bool Update()
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateBook(this);
        }
        public static bool Delete(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.DeleteBook(id);
        }
        public static List<Book> BookedBooks()
        {
            DBservices dbs = new DBservices();
            return dbs.GetBookedBooks();
        }
        public static List<AuthorBooked> BookedAuthors()
        {
            DBservices dbs = new DBservices();
            return dbs.GetBookedAuthors();
        }
        public static List<UsersBooked> BookedUsers()
        {
            DBservices dbs = new DBservices();
            return dbs.GetBookedUsers();
        }
    }
}
