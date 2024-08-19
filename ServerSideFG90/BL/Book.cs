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
        public Book() { }
        public Book(int id, string title, string subTitle, int[] authorsID, string publisher, string description, int pageCount, string categories, double averageRating, string smallPicURL, string picURL, string language, string preivewLink, bool isEbook, string webReaderLink, int price, int publishedDate)
        {
            this.Id = id;
            this.Title = title;
            this.SubTitle = subTitle;
            this.AuthorsID = authorsID;
            this.Publisher = publisher;
            this.Description = description;
            this.PageCount = pageCount;
            this.Categories = categories;
            this.AverageRating = averageRating;
            this.SmallPicURL = smallPicURL;
            this.PicURL = picURL;
            this.Language = language;
            this.PreivewLink = preivewLink;
            this.IsEbook = isEbook;
            this.WebReaderLink = webReaderLink;
            this.Price = price;
            this.PublishedDate = publishedDate;
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
        public static List<Book> ReadAll()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadAllBooks();
        }
        public static List<Book> Top5()
        {
            DBservices dbs = new DBservices();
            return dbs.Top5Books();
        }
    }
}
