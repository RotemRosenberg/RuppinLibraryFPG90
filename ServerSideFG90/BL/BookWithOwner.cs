namespace ServerSideFG90.BL
{
    public class BookWithOwner
    {
        public Book Book { get; set; }
        public int Owner { get; set; }

        public BookWithOwner(int ownerId)
        {
            Owner = ownerId;
        }
    }
}
