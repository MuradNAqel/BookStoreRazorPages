using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreRazorPages.Models
{
    public class Photo : BaseEntity
    {
        //[Column(TypeName = "varbinary(102400)")] //100KB
        [Column(TypeName = "varbinary(max)")]
        public byte[] Bytes { get; set; }

        [Column(TypeName = "VarChar(250)")]
        public string Description { get; set; }

        [Column(TypeName = "VarChar(4)")]
        public string FileExtension { get; set; }

        [Column(TypeName = "decimal(10,2)")] //use MB
        public decimal Size { get; set; }

        //Navigation book has a photo
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
