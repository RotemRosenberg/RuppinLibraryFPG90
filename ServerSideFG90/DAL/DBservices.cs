using ServerSideFG90.BL;
using System.Data.SqlClient;
using System.Data;

namespace ServerSideFG90.DAL
{
    public class DBservices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBservices() // first push
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        //--------------------------------------------------------------------------------------------------
        // Books
        //--------------------------------------------------------------------------------------------------

        //get all books without physical that someone hold 
        public List<Book> ReadAllBooks()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedure("SP_ReadAllBooksF", con, null);  // create the command
            List<Book> BooksList = new List<Book>();
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dataReader["id"]);
                    book.Title = dataReader["title"].ToString();
                    book.SubTitle = dataReader["subTitle"].ToString();
                    for (int i = 0; i < book.AuthorsID.Length; i++)
                    {
                        book.AuthorsID[i] = Convert.ToInt32(dataReader["authorsid" + (i + 1)]);
                    }
                    book.Publisher = dataReader["publisher"].ToString();
                    book.Description = dataReader["description"].ToString();
                    book.PageCount = Convert.ToInt32(dataReader["pageCount"]);
                    book.Categories = dataReader["categories"].ToString();
                    book.AverageRating = Convert.ToDouble(dataReader["averageRating"]);
                    book.SmallPicURL = dataReader["smallThumbnail"].ToString();
                    book.PicURL = dataReader["bigThumbnail"].ToString();
                    book.Language = dataReader["language"].ToString();
                    book.PreivewLink = dataReader["previewLink"].ToString();
                    book.IsEbook = Convert.ToBoolean(dataReader["isEbook"]);
                    book.WebReaderLink = dataReader["webReaderLink"].ToString();
                    book.Price = Convert.ToInt32(dataReader["price"]);
                    book.PublishedDate = Convert.ToInt32(dataReader["publishedDate"]);
                    for (int j = 0; j < book.AuthorNames.Length; j++)
                    {
                        book.AuthorNames[j] = dataReader["authorName" + (j + 1)].ToString();
                    }
                    book.IsBooked = Convert.ToInt32(dataReader["isBooked"]);
                    BooksList.Add(book);
                }
                return BooksList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //admin books
        public List<Book> ReadAllBooksAdmin()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedure("SP_ReadAllBooks", con, null);  // create the command
            List<Book> BooksList = new List<Book>();
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dataReader["id"]);
                    book.Title = dataReader["title"].ToString();
                    book.SubTitle = dataReader["subTitle"].ToString();
                    for (int i = 0; i < book.AuthorsID.Length; i++)
                    {
                        book.AuthorsID[i] = Convert.ToInt32(dataReader["authorsid" + (i + 1)]);
                    }
                    book.Publisher = dataReader["publisher"].ToString();
                    book.Description = dataReader["description"].ToString();
                    book.PageCount = Convert.ToInt32(dataReader["pageCount"]);
                    book.Categories = dataReader["categories"].ToString();
                    book.AverageRating = Convert.ToDouble(dataReader["averageRating"]);
                    book.SmallPicURL = dataReader["smallThumbnail"].ToString();
                    book.PicURL = dataReader["bigThumbnail"].ToString();
                    book.Language = dataReader["language"].ToString();
                    book.PreivewLink = dataReader["previewLink"].ToString();
                    book.IsEbook = Convert.ToBoolean(dataReader["isEbook"]);
                    book.WebReaderLink = dataReader["webReaderLink"].ToString();
                    book.Price = Convert.ToInt32(dataReader["price"]);
                    book.PublishedDate = Convert.ToInt32(dataReader["publishedDate"]);
                    for (int j = 0; j < book.AuthorNames.Length; j++)
                    {
                        book.AuthorNames[j] = dataReader["authorName" + (j + 1)].ToString();
                    }
                    book.IsBooked = Convert.ToInt32(dataReader["isBooked"]);
                    BooksList.Add(book);
                }
                return BooksList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //get top 5 rating books
        public List<Book> Top5Books()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedure("SP_Top5Books", con, null);  // create the command
            List<Book> BooksList = new List<Book>();
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dataReader["id"]);
                    book.Title = dataReader["title"].ToString();
                    book.SubTitle = dataReader["subTitle"].ToString();
                    for (int i = 0; i < book.AuthorsID.Length; i++)
                    {
                        book.AuthorsID[i] = Convert.ToInt32(dataReader["authorsid" + (i + 1)]);
                    }
                    book.Publisher = dataReader["publisher"].ToString();
                    book.Description = dataReader["description"].ToString();
                    book.PageCount = Convert.ToInt32(dataReader["pageCount"]);
                    book.Categories = dataReader["categories"].ToString();
                    book.AverageRating = Convert.ToDouble(dataReader["averageRating"]);
                    book.SmallPicURL = dataReader["smallThumbnail"].ToString();
                    book.PicURL = dataReader["bigThumbnail"].ToString();
                    book.Language = dataReader["language"].ToString();
                    book.PreivewLink = dataReader["previewLink"].ToString();
                    book.IsEbook = Convert.ToBoolean(dataReader["isEbook"]);
                    book.WebReaderLink = dataReader["webReaderLink"].ToString();
                    book.Price = Convert.ToInt32(dataReader["price"]);
                    book.PublishedDate = Convert.ToInt32(dataReader["publishedDate"]);
                    for (int i = 0; i < book.AuthorNames.Length; i++)
                    {
                        book.AuthorNames[i] = dataReader["authorName" + (i + 1)].ToString();
                    }
                    book.IsBooked = Convert.ToInt32(dataReader["isBooked"]);
                    BooksList.Add(book);
                }
                return BooksList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //get Booked Books
        public List<Book> GetBookedBooks()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedure("SP_GetBookedBooks", con, null);  // create the command
            List<Book> BooksList = new List<Book>();
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dataReader["id"]);
                    book.Title = dataReader["title"].ToString();
                    book.SubTitle = dataReader["subTitle"].ToString();
                    for (int i = 0; i < book.AuthorsID.Length; i++)
                    {
                        book.AuthorsID[i] = Convert.ToInt32(dataReader["authorsid" + (i + 1)]);
                    }
                    book.Publisher = dataReader["publisher"].ToString();
                    book.Description = dataReader["description"].ToString();
                    book.PageCount = Convert.ToInt32(dataReader["pageCount"]);
                    book.Categories = dataReader["categories"].ToString();
                    book.AverageRating = Convert.ToDouble(dataReader["averageRating"]);
                    book.SmallPicURL = dataReader["smallThumbnail"].ToString();
                    book.PicURL = dataReader["bigThumbnail"].ToString();
                    book.Language = dataReader["language"].ToString();
                    book.PreivewLink = dataReader["previewLink"].ToString();
                    book.IsEbook = Convert.ToBoolean(dataReader["isEbook"]);
                    book.WebReaderLink = dataReader["webReaderLink"].ToString();
                    book.Price = Convert.ToInt32(dataReader["price"]);
                    book.PublishedDate = Convert.ToInt32(dataReader["publishedDate"]);
                    for (int i = 0; i < book.AuthorNames.Length; i++)
                    {
                        book.AuthorNames[i] = dataReader["authorName" + (i + 1)].ToString();
                    }
                    book.IsBooked = Convert.ToInt32(dataReader["isBooked"]);
                    BooksList.Add(book);
                }
                return BooksList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //get Authors Books
        public List<AuthorBooked> GetBookedAuthors()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedure("SP_GetAuthorsAndBookedCount", con, null);  // create the command
            List<AuthorBooked> AuthorsList = new List<AuthorBooked>();
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    AuthorBooked authorBooked = new AuthorBooked();
                    authorBooked.Author = dataReader["Author"].ToString();
                    authorBooked.Booked = Convert.ToInt32(dataReader["TotalBookedCount"]);              
                    AuthorsList.Add(authorBooked);
                }
                return AuthorsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //get User Books
        public List<UsersBooked> GetBookedUsers()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedure("sp_GetUserBookCounts", con, null);  // create the command
            List<UsersBooked> UsersList = new List<UsersBooked>();
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    UsersBooked userBooked = new UsersBooked();
                    userBooked.UserName = dataReader["UserName"].ToString();
                    userBooked.Booked = Convert.ToInt32(dataReader["BookCount"]);
                    UsersList.Add(userBooked);
                }
                return UsersList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        // delete book
        public bool DeleteBook(int id)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@BookId", id);

            cmd = CreateCommandWithStoredProcedure("SP_DeleteBook", con, paramDic);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                if (numEffected > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //get book by id
        public Book BookById(int id)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@id", id);
            cmd = CreateCommandWithStoredProcedure("SP_GetBookByIdF", con, paramDic);  // create the command

            try
            {
                object result = cmd.ExecuteScalar(); // execute the command and get the result
                int returnValue = Convert.ToInt32(result);
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dataReader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dataReader["id"]);
                    book.Title = dataReader["title"].ToString();
                    book.SubTitle = dataReader["subTitle"].ToString();
                    for (int i = 0; i < book.AuthorsID.Length; i++)
                    {
                        book.AuthorsID[i] = Convert.ToInt32(dataReader["authorsid" + (i + 1)]);
                    }
                    book.Publisher = dataReader["publisher"].ToString();
                    book.Description = dataReader["description"].ToString();
                    book.PageCount = Convert.ToInt32(dataReader["pageCount"]);
                    book.Categories = dataReader["categories"].ToString();
                    book.AverageRating = Convert.ToDouble(dataReader["averageRating"]);
                    book.SmallPicURL = dataReader["smallThumbnail"].ToString();
                    book.PicURL = dataReader["bigThumbnail"].ToString();
                    book.Language = dataReader["language"].ToString();
                    book.PreivewLink = dataReader["previewLink"].ToString();
                    book.IsEbook = Convert.ToBoolean(dataReader["isEbook"]);
                    book.WebReaderLink = dataReader["webReaderLink"].ToString();
                    book.Price = Convert.ToInt32(dataReader["price"]);
                    book.PublishedDate = Convert.ToInt32(dataReader["publishedDate"]);
                    for (int i = 0; i < book.AuthorNames.Length; i++)
                    {
                        book.AuthorNames[i] = dataReader["authorName" + (i + 1)].ToString();
                    }
                    book.IsBooked = Convert.ToInt32(dataReader["isBooked"]);

                    return book;

                }
                else return null;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //insert book
        public Book InsertBook(Book book)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Title", book.Title);
            paramDic.Add("@SubTitle", book.SubTitle);
            paramDic.Add("@Authorsid1", book.AuthorsID[0]);
            paramDic.Add("@Authorsid2", book.AuthorsID[1]);
            paramDic.Add("@Authorsid3", book.AuthorsID[2]);
            paramDic.Add("@Publisher", book.Publisher);
            paramDic.Add("@Description", book.Description);
            paramDic.Add("@PageCount", book.PageCount);
            paramDic.Add("@Categories", book.Categories);
            paramDic.Add("@AverageRating", book.AverageRating);
            paramDic.Add("@Image", book.SmallPicURL);
            paramDic.Add("@Language", book.Language);
            paramDic.Add("@PreviewLink", book.PreivewLink);
            paramDic.Add("@WebReaderLink", book.WebReaderLink);
            paramDic.Add("@IsEbook", book.IsEbook);
            paramDic.Add("@Price", book.Price);
            paramDic.Add("@PublishedDate", book.PublishedDate);
            paramDic.Add("@isBooked", book.IsBooked);
            paramDic.Add("@AuthorName1", book.AuthorNames[0]);
            paramDic.Add("@AuthorName2", book.AuthorNames[1]);
            paramDic.Add("@AuthorName3", book.AuthorNames[2]);
            cmd = CreateCommandWithStoredProcedure("SP_CreateBook", con, paramDic);  // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                if (numEffected > 0)
                {
                    return LastBook();
                }
                return null;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //get lasted insert book
        public Book LastBook()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedure("SP_GetLastInsertedBook", con, null);  // create the command

            try
            {
                object result = cmd.ExecuteScalar(); // execute the command and get the result
                int returnValue = Convert.ToInt32(result);
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dataReader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dataReader["id"]);
                    book.Title = dataReader["title"].ToString();
                    book.SubTitle = dataReader["subTitle"].ToString();
                    for (int i = 0; i < book.AuthorsID.Length; i++)
                    {
                        book.AuthorsID[i] = Convert.ToInt32(dataReader["authorsid" + (i + 1)]);
                    }
                    book.Publisher = dataReader["publisher"].ToString();
                    book.Description = dataReader["description"].ToString();
                    book.PageCount = Convert.ToInt32(dataReader["pageCount"]);
                    book.Categories = dataReader["categories"].ToString();
                    book.AverageRating = Convert.ToDouble(dataReader["averageRating"]);
                    book.SmallPicURL = dataReader["smallThumbnail"].ToString();
                    book.PicURL = dataReader["bigThumbnail"].ToString();
                    book.Language = dataReader["language"].ToString();
                    book.PreivewLink = dataReader["previewLink"].ToString();
                    book.IsEbook = Convert.ToBoolean(dataReader["isEbook"]);
                    book.WebReaderLink = dataReader["webReaderLink"].ToString();
                    book.Price = Convert.ToInt32(dataReader["price"]);
                    book.PublishedDate = Convert.ToInt32(dataReader["publishedDate"]);
                    for (int i = 0; i < book.AuthorNames.Length; i++)
                    {
                        book.AuthorNames[i] = dataReader["authorName" + (i + 1)].ToString();
                    }
                    book.IsBooked = Convert.ToInt32(dataReader["isBooked"]);

                    return book;

                }
                else return null;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //update book
        public bool UpdateBook(Book book)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@BookId", book.Id);
            paramDic.Add("@Title", book.Title);
            paramDic.Add("@SubTitle", book.SubTitle);
            paramDic.Add("@Description", book.Description);
            paramDic.Add("@Categories", book.Categories);
            paramDic.Add("@IsEbook", book.IsEbook);
            paramDic.Add("@Price", book.Price);
            cmd = CreateCommandWithStoredProcedure("SP_EditBook", con, paramDic);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                if (numEffected == 0) return false;
                else return true;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //get Books By Title
        public List<Book> BooksByTitle(string title)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@searchText", title);

            cmd = CreateCommandWithStoredProcedure("SP_GetBooksByTitleF", con, paramDic);  // create the command
            List<Book> BooksList = new List<Book>();
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dataReader["id"]);
                    book.Title = dataReader["title"].ToString();
                    book.SubTitle = dataReader["subTitle"].ToString();
                    for (int i = 0; i < book.AuthorsID.Length; i++)
                    {
                        book.AuthorsID[i] = Convert.ToInt32(dataReader["authorsid" + (i + 1)]);
                    }
                    book.Publisher = dataReader["publisher"].ToString();
                    book.Description = dataReader["description"].ToString();
                    book.PageCount = Convert.ToInt32(dataReader["pageCount"]);
                    book.Categories = dataReader["categories"].ToString();
                    book.AverageRating = Convert.ToDouble(dataReader["averageRating"]);
                    book.SmallPicURL = dataReader["smallThumbnail"].ToString();
                    book.PicURL = dataReader["bigThumbnail"].ToString();
                    book.Language = dataReader["language"].ToString();
                    book.PreivewLink = dataReader["previewLink"].ToString();
                    book.IsEbook = Convert.ToBoolean(dataReader["isEbook"]);
                    book.WebReaderLink = dataReader["webReaderLink"].ToString();
                    book.Price = Convert.ToInt32(dataReader["price"]);
                    book.PublishedDate = Convert.ToInt32(dataReader["publishedDate"]);
                    for (int i = 0; i < book.AuthorNames.Length; i++)
                    {
                        book.AuthorNames[i] = dataReader["authorName" + (i + 1)].ToString();
                    }
                    book.IsBooked = Convert.ToInt32(dataReader["isBooked"]);

                    BooksList.Add(book);
                }
                return BooksList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //get Books By Text
        public List<Book> BooksByText(string text)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@searchText", text);

            cmd = CreateCommandWithStoredProcedure("SP_GetBooksByTextF", con, paramDic);  // create the command
            List<Book> BooksList = new List<Book>();
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dataReader["id"]);
                    book.Title = dataReader["title"].ToString();
                    book.SubTitle = dataReader["subTitle"].ToString();
                    for (int i = 0; i < book.AuthorsID.Length; i++)
                    {
                        book.AuthorsID[i] = Convert.ToInt32(dataReader["authorsid" + (i + 1)]);
                    }
                    book.Publisher = dataReader["publisher"].ToString();
                    book.Description = dataReader["description"].ToString();
                    book.PageCount = Convert.ToInt32(dataReader["pageCount"]);
                    book.Categories = dataReader["categories"].ToString();
                    book.AverageRating = Convert.ToDouble(dataReader["averageRating"]);
                    book.SmallPicURL = dataReader["smallThumbnail"].ToString();
                    book.PicURL = dataReader["bigThumbnail"].ToString();
                    book.Language = dataReader["language"].ToString();
                    book.PreivewLink = dataReader["previewLink"].ToString();
                    book.IsEbook = Convert.ToBoolean(dataReader["isEbook"]);
                    book.WebReaderLink = dataReader["webReaderLink"].ToString();
                    book.Price = Convert.ToInt32(dataReader["price"]);
                    book.PublishedDate = Convert.ToInt32(dataReader["publishedDate"]);
                    for (int i = 0; i < book.AuthorNames.Length; i++)
                    {
                        book.AuthorNames[i] = dataReader["authorName" + (i + 1)].ToString();
                    }
                    book.IsBooked = Convert.ToInt32(dataReader["isBooked"]);

                    BooksList.Add(book);
                }
                return BooksList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //get Books By Author
        public List<Book> BooksByAuthor(string author)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@author", author);

            cmd = CreateCommandWithStoredProcedure("SP_GetBooksByAuthorFF", con, paramDic);  // create the command
            List<Book> BooksList = new List<Book>();
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dataReader["id"]);
                    book.Title = dataReader["title"].ToString();
                    book.SubTitle = dataReader["subTitle"].ToString();
                    for (int i = 0; i < book.AuthorsID.Length; i++)
                    {
                        book.AuthorsID[i] = Convert.ToInt32(dataReader["authorsid" + (i + 1)]);
                    }
                    book.Publisher = dataReader["publisher"].ToString();
                    book.Description = dataReader["description"].ToString();
                    book.PageCount = Convert.ToInt32(dataReader["pageCount"]);
                    book.Categories = dataReader["categories"].ToString();
                    book.AverageRating = Convert.ToDouble(dataReader["averageRating"]);
                    book.SmallPicURL = dataReader["smallThumbnail"].ToString();
                    book.PicURL = dataReader["bigThumbnail"].ToString();
                    book.Language = dataReader["language"].ToString();
                    book.PreivewLink = dataReader["previewLink"].ToString();
                    book.IsEbook = Convert.ToBoolean(dataReader["isEbook"]);
                    book.WebReaderLink = dataReader["webReaderLink"].ToString();
                    book.Price = Convert.ToInt32(dataReader["price"]);
                    book.PublishedDate = Convert.ToInt32(dataReader["publishedDate"]);
                    for (int i = 0; i < book.AuthorNames.Length; i++)
                    {
                        book.AuthorNames[i] = dataReader["authorName" + (i + 1)].ToString();
                    }
                    book.IsBooked = Convert.ToInt32(dataReader["isBooked"]);

                    BooksList.Add(book);
                }
                return BooksList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        public List<Book> BooksByCategory(string category)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@category", category);

            cmd = CreateCommandWithStoredProcedure("SP_GetBooksByCategoryF", con, paramDic);  // create the command
            List<Book> BooksList = new List<Book>();
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dataReader["id"]);
                    book.Title = dataReader["title"].ToString();
                    book.SubTitle = dataReader["subTitle"].ToString();
                    for (int i = 0; i < book.AuthorsID.Length; i++)
                    {
                        book.AuthorsID[i] = Convert.ToInt32(dataReader["authorsid" + (i + 1)]);
                    }
                    book.Publisher = dataReader["publisher"].ToString();
                    book.Description = dataReader["description"].ToString();
                    book.PageCount = Convert.ToInt32(dataReader["pageCount"]);
                    book.Categories = dataReader["categories"].ToString();
                    book.AverageRating = Convert.ToDouble(dataReader["averageRating"]);
                    book.SmallPicURL = dataReader["smallThumbnail"].ToString();
                    book.PicURL = dataReader["bigThumbnail"].ToString();
                    book.Language = dataReader["language"].ToString();
                    book.PreivewLink = dataReader["previewLink"].ToString();
                    book.IsEbook = Convert.ToBoolean(dataReader["isEbook"]);
                    book.WebReaderLink = dataReader["webReaderLink"].ToString();
                    book.Price = Convert.ToInt32(dataReader["price"]);
                    book.PublishedDate = Convert.ToInt32(dataReader["publishedDate"]);
                    for (int i = 0; i < book.AuthorNames.Length; i++)
                    {
                        book.AuthorNames[i] = dataReader["authorName" + (i + 1)].ToString();
                    }
                    book.IsBooked = Convert.ToInt32(dataReader["isBooked"]);

                    BooksList.Add(book);
                }
                return BooksList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //--------------------------------------------------------------------------------------------------
        // Users
        //--------------------------------------------------------------------------------------------------

        //get all users
        public List<Users> ReadAllUsers()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedure("SP_ReadAllUsersF", con, null);  // create the command
            List<Users> UsersList = new List<Users>();
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    Users user = new Users();
                    user.Id = Convert.ToInt32(dataReader["UserId"]);
                    user.Name = dataReader["userName"].ToString();
                    user.Email = dataReader["Email"].ToString();
                    user.Password = dataReader["password"].ToString();
                    user.IsAdmin = Convert.ToBoolean(dataReader["isAdmin"]);
                    user.Balance = Convert.ToInt32(dataReader["balance"]);
                    UsersList.Add(user);
                }
                return UsersList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //login user
        public Users LogInUser(string email, string password)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Email", email);
            paramDic.Add("@Password", password);


            cmd = CreateCommandWithStoredProcedure("SP_LoginUserF", con, paramDic);             // create the command

            try
            {
                object result = cmd.ExecuteScalar(); // execute the command and get the result
                int returnValue = Convert.ToInt32(result);
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dataReader.Read())
                {
                    Users user = new Users();
                    user.Id = Convert.ToInt32(dataReader["UserId"]);
                    user.Name = dataReader["userName"].ToString();
                    user.Email = dataReader["Email"].ToString();
                    user.Password = dataReader["password"].ToString();
                    user.IsAdmin = Convert.ToBoolean(dataReader["isAdmin"]);
                    user.Balance = Convert.ToInt32(dataReader["balance"]);
                    return user;
                }
                else return null;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //get user by id     
        public Users GetUserById(int id)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@id", id);

            cmd = CreateCommandWithStoredProcedure("SP_GetUserByIdF", con, paramDic);             // create the command

            try
            {
                object result = cmd.ExecuteScalar(); // execute the command and get the result
                int returnValue = Convert.ToInt32(result);
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dataReader.Read())
                {
                    Users user = new Users();
                    user.Id = Convert.ToInt32(dataReader["UserId"]);
                    user.Name = dataReader["userName"].ToString();
                    user.Email = dataReader["Email"].ToString();
                    user.Password = dataReader["password"].ToString();
                    user.IsAdmin = Convert.ToBoolean(dataReader["isAdmin"]);
                    user.Balance = Convert.ToInt32(dataReader["balance"]);
                    return user;
                }
                else return null;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //register user
        public Users RegisterUserDB(Users user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Name", user.Name);
            paramDic.Add("@Email", user.Email);
            paramDic.Add("@Password", user.Password);
            cmd = CreateCommandWithStoredProcedure("SP_RegisterUserF", con, paramDic);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                if (numEffected != 0)
                {
                    return LogInUser(user.Email, user.Password);

                }
                else return null;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------------------------------------
        // Authors
        //--------------------------------------------------------------------------------------------------
        public List<Author> ReadAllAuthors()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedure("SP_ReadAllAuthorsF", con, null);  // create the command
            List<Author> authorsList = new List<Author>();
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Author author = new Author();
                    author.AuthorID = Convert.ToInt32(dataReader["id"]);
                    author.AuthorName = dataReader["Author"].ToString();
                    author.Gender = dataReader["Gender"].ToString();
                    author.YearBirth = Convert.ToInt32(dataReader["Yearbirth"]);
                    author.Description = dataReader["Description"].ToString();

                    authorsList.Add(author);
                }
                return authorsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        public List<Author> AuthorsByName(string name)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@name", name);

            cmd = CreateCommandWithStoredProcedure("SP_GetAuthorsByNameF", con, paramDic);  // create the command
            List<Author> authorsList = new List<Author>();
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Author author = new Author();
                    author.AuthorID = Convert.ToInt32(dataReader["id"]);
                    author.AuthorName = dataReader["Author"].ToString();
                    author.Gender = dataReader["Gender"].ToString();
                    author.YearBirth = Convert.ToInt32(dataReader["Yearbirth"]);
                    author.Description = dataReader["Description"].ToString();

                    authorsList.Add(author);
                }
                return authorsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------------------------------------
        // Book Review
        //--------------------------------------------------------------------------------------------------
        public List<BookReview> ReadBookReview(int id)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@id", id);

            cmd = CreateCommandWithStoredProcedure("SP_GetBookReviewF", con, paramDic);  // create the command
            List<BookReview> bookReviewList = new List<BookReview>();
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    BookReview bookReview = new BookReview();
                    bookReview.UserId = Convert.ToInt32(dataReader["userID"]);
                    bookReview.BookId = Convert.ToInt32(dataReader["bookID"]);
                    bookReview.Rating = Convert.ToDouble(dataReader["rating"]);
                    bookReview.Review = dataReader["review"].ToString();
                    bookReview.UserName = dataReader["userName"].ToString();
                    bookReviewList.Add(bookReview);
                }
                return bookReviewList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        public bool InsertBookReview(BookReview bookReview)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            //create Dictionary for sp
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@UserId", bookReview.UserId);
            paramDic.Add("@BookId", bookReview.BookId);
            paramDic.Add("@Rating", bookReview.Rating);
            paramDic.Add("@Review", bookReview.Review);
            paramDic.Add("@UserName", bookReview.UserName);

            cmd = CreateCommandWithStoredProcedure("SP_AddBookReview", con, paramDic);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                if (numEffected == 0) return false;
                else return true;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------------------------------------
        // User Book
        //--------------------------------------------------------------------------------------------------
        public bool addBookToUserDB(int userID, int bookID, int bookPrice)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@userID", userID);
            paramDic.Add("@bookID", bookID);
            paramDic.Add("@bookPrice", bookPrice);

            cmd = CreateCommandWithStoredProcedure("SP_addBookToUserFF", con, paramDic);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                if (numEffected == 0) return false;
                else return true;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        public List<Book> GetAllBooksDB(int userID) // returns all the books of a specific user
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            //create Dictionary for sp
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@userID", userID);
            cmd = CreateCommandWithStoredProcedure("SP_GetAllBooksOfUserF", con, paramDic);     // create the command
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                List<Book> booksList = new List<Book>();
                while (dataReader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dataReader["id"]);
                    book.Title = dataReader["title"].ToString();
                    book.SubTitle = dataReader["subTitle"].ToString();
                    for (int i = 0; i < book.AuthorsID.Length; i++)
                    {
                        book.AuthorsID[i] = Convert.ToInt32(dataReader["authorsid" + (i + 1)]);
                    }
                    book.Publisher = dataReader["publisher"].ToString();
                    book.Description = dataReader["description"].ToString();
                    book.PageCount = Convert.ToInt32(dataReader["pageCount"]);
                    book.Categories = dataReader["categories"].ToString();
                    book.AverageRating = Convert.ToDouble(dataReader["averageRating"]);
                    book.SmallPicURL = dataReader["smallThumbnail"].ToString();
                    book.PicURL = dataReader["bigThumbnail"].ToString();
                    book.Language = dataReader["language"].ToString();
                    book.PreivewLink = dataReader["previewLink"].ToString();
                    book.IsEbook = Convert.ToBoolean(dataReader["isEbook"]);
                    book.WebReaderLink = dataReader["webReaderLink"].ToString();
                    book.Price = Convert.ToInt32(dataReader["price"]);
                    book.PublishedDate = Convert.ToInt32(dataReader["publishedDate"]);
                    for (int i = 0; i < book.AuthorNames.Length; i++)
                    {
                        book.AuthorNames[i] = dataReader["authorName" + (i + 1)].ToString();
                    }
                    book.IsBooked = Convert.ToInt32(dataReader["isBooked"]);
                    booksList.Add(book);
                }
                return booksList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        public List<Book> GetAllReadedBooksDB(int userID) // returns all the readed books of a specific user
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            //create Dictionary for sp
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@userID", userID);
            cmd = CreateCommandWithStoredProcedure("SP_GetAllReadedBooksOfUserF", con, paramDic);     // create the command
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                List<Book> books = new List<Book>();
                while (dataReader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dataReader["id"]);
                    book.Title = dataReader["title"].ToString();
                    book.SubTitle = dataReader["subTitle"].ToString();
                    for (int i = 0; i < book.AuthorsID.Length; i++)
                    {
                        book.AuthorsID[i] = Convert.ToInt32(dataReader["authorsid" + (i + 1)]);
                    }
                    book.Publisher = dataReader["publisher"].ToString();
                    book.Description = dataReader["description"].ToString();
                    book.PageCount = Convert.ToInt32(dataReader["pageCount"]);
                    book.Categories = dataReader["categories"].ToString();
                    book.AverageRating = Convert.ToDouble(dataReader["averageRating"]);
                    book.SmallPicURL = dataReader["smallThumbnail"].ToString();
                    book.PicURL = dataReader["bigThumbnail"].ToString();
                    book.Language = dataReader["language"].ToString();
                    book.PreivewLink = dataReader["previewLink"].ToString();
                    book.IsEbook = Convert.ToBoolean(dataReader["isEbook"]);
                    book.WebReaderLink = dataReader["webReaderLink"].ToString();
                    book.Price = Convert.ToInt32(dataReader["price"]);
                    book.PublishedDate = Convert.ToInt32(dataReader["publishedDate"]);
                    for (int i = 0; i < book.AuthorNames.Length; i++)
                    {
                        book.AuthorNames[i] = dataReader["authorName" + (i + 1)].ToString();
                    }
                    book.IsBooked = Convert.ToInt32(dataReader["isBooked"]);
                    books.Add(book);
                }
                return books;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        public bool markAsReadedDB(int userID, int bookID)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@userID", userID);
            paramDic.Add("@bookID", bookID);

            cmd = CreateCommandWithStoredProcedure("SP_markBookAsReadedF", con, paramDic);   // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                if (numEffected == 0) return false;
                else return true;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        public List<BookWithOwner> GetAllSaleBooksDB(int userID) // returns all the available books for sale for specific user
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // Create Dictionary for stored procedure parameters
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@userID", userID);
            cmd = CreateCommandWithStoredProcedure("SP_GetAllSaleBooksF", con, paramDic); // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                List<BookWithOwner> booksWithOwners = new List<BookWithOwner>();

                while (dataReader.Read())
                {
                    // Check if the book already exists in the list
                    var existingBook = booksWithOwners.FirstOrDefault(b => b.Book.Id == Convert.ToInt32(dataReader["id"]));

                    if (existingBook == null)
                    {
                        // If not, create a new entry
                        BookWithOwner bookWithOwner = new BookWithOwner(Convert.ToInt32(dataReader["UserID"])); // Initialize with a single owner
                        bookWithOwner.Book = new Book
                        {
                            Id = Convert.ToInt32(dataReader["id"]),
                            Title = dataReader["title"].ToString(),
                            SubTitle = dataReader["subTitle"].ToString(),
                            AuthorsID = new int[3]
                            {
                    Convert.ToInt32(dataReader["authorsid1"]),
                    Convert.ToInt32(dataReader["authorsid2"]),
                    Convert.ToInt32(dataReader["authorsid3"])
                            },
                            Publisher = dataReader["publisher"].ToString(),
                            Description = dataReader["description"].ToString(),
                            PageCount = Convert.ToInt32(dataReader["pageCount"]),
                            Categories = dataReader["categories"].ToString(),
                            AverageRating = Convert.ToDouble(dataReader["averageRating"]),
                            SmallPicURL = dataReader["smallThumbnail"].ToString(),
                            PicURL = dataReader["bigThumbnail"].ToString(),
                            Language = dataReader["language"].ToString(),
                            PreivewLink = dataReader["previewLink"].ToString(),
                            IsEbook = Convert.ToBoolean(dataReader["isEbook"]),
                            WebReaderLink = dataReader["webReaderLink"].ToString(),
                            Price = Convert.ToInt32(dataReader["price"]),
                            PublishedDate = Convert.ToInt32(dataReader["publishedDate"]),
                            AuthorNames = new string[3]
                            {
                    dataReader["authorName1"].ToString(),
                    dataReader["authorName2"].ToString(),
                    dataReader["authorName3"].ToString()
                            },
                            IsBooked = Convert.ToInt32(dataReader["isBooked"])
                    };
                        booksWithOwners.Add(bookWithOwner);
                    }
                    else
                    {
                        // If it exists, replace the owner with the new one
                        existingBook.Owner = Convert.ToInt32(dataReader["UserID"]);
                    }
                }

                return booksWithOwners;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        public bool sendPurchaseRequest(PurchaseRequestModel model)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@bookID", model.BookID);
            paramDic.Add("@senderID", model.SenderID);
            paramDic.Add("@receiverID", model.ReceiverID);

            cmd = CreateCommandWithStoredProcedure("SP_sendPurchaseRequestF", con, paramDic);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                if (numEffected == 0) { return false; }
                else { return true; }
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();

                }
            }

        }
        public List<BookWithOwner> GetAllPurchaseRequests(int userID)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            //create Dictionary for sp
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@userID", userID);
            cmd = CreateCommandWithStoredProcedure("SP_GetAllPurchaseRequestsOfUserF", con, paramDic);     // create the command
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                List<BookWithOwner> booksWithOwners = new List<BookWithOwner>();

                while (dataReader.Read())
                {
                    //// Check if the book already exists in the list
                    //var existingBook = booksWithOwners.FirstOrDefault(b => b.Book.Id == Convert.ToInt32(dataReader["id"]));

                    //if (existingBook == null)
                    //{
                        // If not, create a new entry
                        BookWithOwner bookWithOwner = new BookWithOwner(Convert.ToInt32(dataReader["UserID"])); // Initialize with a single owner
                        bookWithOwner.Book = new Book
                        {
                            Id = Convert.ToInt32(dataReader["id"]),
                            Title = dataReader["title"].ToString(),
                            SubTitle = dataReader["subTitle"].ToString(),
                            AuthorsID = new int[3]
                            {
                    Convert.ToInt32(dataReader["authorsid1"]),
                    Convert.ToInt32(dataReader["authorsid2"]),
                    Convert.ToInt32(dataReader["authorsid3"])
                            },
                            Publisher = dataReader["publisher"].ToString(),
                            Description = dataReader["description"].ToString(),
                            PageCount = Convert.ToInt32(dataReader["pageCount"]),
                            Categories = dataReader["categories"].ToString(),
                            AverageRating = Convert.ToDouble(dataReader["averageRating"]),
                            SmallPicURL = dataReader["smallThumbnail"].ToString(),
                            PicURL = dataReader["bigThumbnail"].ToString(),
                            Language = dataReader["language"].ToString(),
                            PreivewLink = dataReader["previewLink"].ToString(),
                            IsEbook = Convert.ToBoolean(dataReader["isEbook"]),
                            WebReaderLink = dataReader["webReaderLink"].ToString(),
                            Price = Convert.ToInt32(dataReader["price"]),
                            PublishedDate = Convert.ToInt32(dataReader["publishedDate"]),
                            AuthorNames = new string[3]
                            {
                    dataReader["authorName1"].ToString(),
                    dataReader["authorName2"].ToString(),
                    dataReader["authorName3"].ToString()
                            },
                            IsBooked = Convert.ToInt32(dataReader["isBooked"])
                        };
                        booksWithOwners.Add(bookWithOwner);
                    //}
                    //else
                    //{
                    //    // If it exists, replace the owner with the new one
                    //    existingBook.Owner = Convert.ToInt32(dataReader["UserID"]);
                    //}
                }

                return booksWithOwners;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        public bool ApprovePurchaseRequest(int ownerID, int bookID, int bookPrice)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@ownerID", ownerID);
            paramDic.Add("@bookID", bookID);
            paramDic.Add("@bookPrice", bookPrice);
            cmd = CreateCommandWithStoredProcedure("SP_ApprovePurchaseRequestF", con, paramDic);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                if (numEffected == 0) return false;
                else return true;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        public List<Book> GetAllRecommandedBooksDB(int userID) // returns all the books of a specific user
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            //create Dictionary for sp
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@userID", userID);
            cmd = CreateCommandWithStoredProcedure("SP_GetAllRecommandedBooksOfUserF", con, paramDic);     // create the command
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                List<Book> booksList = new List<Book>();
                while (dataReader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dataReader["id"]);
                    book.Title = dataReader["title"].ToString();
                    book.SubTitle = dataReader["subTitle"].ToString();
                    for (int i = 0; i < book.AuthorsID.Length; i++)
                    {
                        book.AuthorsID[i] = Convert.ToInt32(dataReader["authorsid" + (i + 1)]);
                    }
                    book.Publisher = dataReader["publisher"].ToString();
                    book.Description = dataReader["description"].ToString();
                    book.PageCount = Convert.ToInt32(dataReader["pageCount"]);
                    book.Categories = dataReader["categories"].ToString();
                    book.AverageRating = Convert.ToDouble(dataReader["averageRating"]);
                    book.SmallPicURL = dataReader["smallThumbnail"].ToString();
                    book.PicURL = dataReader["bigThumbnail"].ToString();
                    book.Language = dataReader["language"].ToString();
                    book.PreivewLink = dataReader["previewLink"].ToString();
                    book.IsEbook = Convert.ToBoolean(dataReader["isEbook"]);
                    book.WebReaderLink = dataReader["webReaderLink"].ToString();
                    book.Price = Convert.ToInt32(dataReader["price"]);
                    book.PublishedDate = Convert.ToInt32(dataReader["publishedDate"]);
                    for (int i = 0; i < book.AuthorNames.Length; i++)
                    {
                        book.AuthorNames[i] = dataReader["authorName" + (i + 1)].ToString();
                    }
                    booksList.Add(book);
                }
                return booksList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        public bool deleteBookOfUserDB(int userID, int bookID)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@userID", userID);
            paramDic.Add("@bookID", bookID);

            cmd = CreateCommandWithStoredProcedure("SP_deleteBookOfUserF", con, paramDic);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                if (numEffected == 0) return false;
                else return true;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommandWithStoredProcedure(String spName, SqlConnection con, Dictionary<string, object> paramDic)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            //check if Dictionary not null and add to cmd
            if (paramDic != null)
                foreach (KeyValuePair<string, object> param in paramDic)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);

                }
            return cmd;
        }
    }
}
