using ServerSideFG90.BL;
using System.Data.SqlClient;
using System.Data;

namespace ServerSideFG90.DAL
{
    public class DBservices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBservices()
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
