namespace BookStoreRazorPages.Application.Dtos.PhotoDtos
{
    public class PhotoDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string FileExtension { get; set; }
        public decimal Size { get; set; }
        public int? BookId { get; set; }
    }
}
