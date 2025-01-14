namespace BookStoreRazorPages.Application.Dtos.PhotoDtos
{
    public class PhotoDto
    {
        public string Path { get; set; }
        public string FileExtension { get; set; }
        public decimal Size { get; set; }
        public int BookId { get; set; }
    }
}
