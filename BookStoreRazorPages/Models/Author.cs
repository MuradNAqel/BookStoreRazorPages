namespace BookStoreRazorPages.Models
{
    public class Author
    {
        //Foreign Key property

        //Navigation property, An author could have many books.
        public List<Book> Books { get; set; }
    }
}
