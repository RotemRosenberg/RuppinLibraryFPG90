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

        //get all books
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
