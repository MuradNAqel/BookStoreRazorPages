using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreRazorPages.Application.Entities
{
    public class Photo : BaseEntity
    {

        public string Path { get; set; }

        [Column(TypeName = "VarChar(4)")]
        public string FileExtension { get; set; }

        [Column(TypeName = "decimal(10,2)")] //use MB
        public decimal Size { get; set; }

        //Navigation book has a photo
        public int BookId { get; set; }
        //public Book Book { get; set; }

        string[] AllowedExtensions = { "jpg", "jpeg", "png", "gif", "bmp", "webp", "tiff" };

        public void SetPath(string path)
        {
            if (path == null)
            {
                throw new ArgumentException("Path is null", nameof(path));
            }
            Path = path;
        }



        public void SetFileExtension(string fileExtension)
        {
            if (!AllowedExtensions.Contains(fileExtension.ToLower()))
            {
                throw new ArgumentException("AllowedExtensions = { \"jpg\", \"jpeg\", \"png\", \"gif\", \"bmp\", \"webp\", \"tiff\" }", nameof(fileExtension));
            }
            FileExtension = fileExtension;
        }

        public void SetSize(decimal size)
        {
            if (size > 2000000)
            {
                throw new ArgumentException("size exceeded 2MB", nameof(size));
            }
            Size = size;
        }

    }

}
