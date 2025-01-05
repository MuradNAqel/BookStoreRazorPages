using System.ComponentModel.DataAnnotations;

namespace BookStoreRazorPages.Models
{
    public class Author : BaseEntityAudit
    {
        [Range(0, 88, ErrorMessage = "Price must be between 1 and 500.")]
        public int Age { get; set; }

        [MaxLength(50)]
        public string Speciality { get; set; }
        //Navigation property, An author could have many books.
        public List<Book> Books { get; set; }
    }
}
