namespace BookStoreRazorPages.Models
{
    public class Photo : BaseEntity
    {
        public byte[] Bytes { get; set; }
        public string Description { get; set; }
        public string FileExtension { get; set; }
        public decimal Size { get; set; }

        //Navigation book has a photo
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
