using ServerSideFG90.DAL;

namespace ServerSideFG90.BL
{
    public class Users
    {
        int id;
        string name;
        string email;
        string password;
        bool isAdmin;
        int balance;

        public Users(int id, string name, string email, string password, bool isAdmin, int balance)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.IsAdmin = isAdmin;
            this.Balance = balance;
        }
        public Users() { }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public int Balance { get => balance; set => balance = value; }

        public static List<Users> ReadAll()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadAllUsers();
        }
        public static Users GetUserById(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.GetUserById(id);
        }
        public static Users Login(string email, string password)
        {
            DBservices dbs = new DBservices();
            return dbs.LogInUser(email, password);
        }
        public Users RegisterUser()
        {
            DBservices dbs = new DBservices();
            return dbs.RegisterUserDB(this);
        }

    }
}
