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

    }
}
