using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreRazorPages.Models
{
    [Table("Book")]
    public class Book : BaseEntityAudit
    {
        [MinLength(3)]
        public string Title { get; set; }
        [MinLength(3)]
        public string Description { get; set; }
        [Range(0.01, 500, ErrorMessage = "Price must be between 0.01 and 500.")]
        public decimal Price { get; set; }

        //Navigation properties A Book can have many authors and Atmost one photo(for now).
        public List<Author> Authors { get; set; }
        public Photo Photo { get; set; }
    }
}
