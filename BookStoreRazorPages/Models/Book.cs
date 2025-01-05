using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreRazorPages.Models
{
    [Table("Book")]
    public class Book : BaseEntityAudit
    {
        [MinLength(3)]
        [MaxLength(100)]
        public string Description { get; set; }

        [Range(1, 500, ErrorMessage = "Price must be between 1 and 500.")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        [MaxLength(20)]
        public string Category { get; set; }

        //Navigation properties A Book can have many authors and Atmost one photo(for now).
        public List<Author> Authors { get; set; }
        public Photo Photo { get; set; }
    }
}
