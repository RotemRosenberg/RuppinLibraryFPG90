using ServerSideFG90.DAL;

namespace ServerSideFG90.BL
{
    public class Author
    {
        int authorID;
        string authorName;
        string gender;
        int yearBirth;
        string description;
        public Author() { }
        public Author(int authorID, string authorName, string gender, int yearBirth, string description)
        {
            AuthorID = authorID;
            AuthorName = authorName;
            Gender = gender;
            YearBirth = yearBirth;
            Description = description;
        }

        public int AuthorID { get => authorID; set => authorID = value; }
        public string AuthorName { get => authorName; set => authorName = value; }
        public string Gender { get => gender; set => gender = value; }
        public int YearBirth { get => yearBirth; set => yearBirth = value; }
        public string Description { get => description; set => description = value; }
        public static List<Author> ReadAll()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadAllAuthors();
        }
        public static List<Author> ReadAuthorsByName(string name)
        {
            DBservices dbs = new DBservices();
            return dbs.AuthorsByName(name);
        }
    }
}
