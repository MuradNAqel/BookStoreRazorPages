namespace BookStoreRazorPages.Application.Dtos.PhotoDtos
{
    public class EditPhotoDto
    {
        public int PhotoId { get; set; }
        public string ExistingPhotoUrl { get; set; } // Display current photo
        public List<IFormFile> NewPhotos { get; set; } // For replacing the photo
    }
}
