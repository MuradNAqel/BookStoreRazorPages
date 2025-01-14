using BookStoreRazorPages.Application.Entities;

namespace BookStoreRazorPages.Application.Dtos.BookDtos
{
    public class EditBookDto
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } //for now 
        public string Name { get; set; }
        public List<int> Authors { get; set; }

        public Book MapToBook()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException(nameof(Name), "Book name cannot be null or empty.");
            if (string.IsNullOrEmpty(Description))
                throw new ArgumentNullException(nameof(Description), "Description cannot be null or empty.");
            if (string.IsNullOrEmpty(Category))
                throw new ArgumentNullException(nameof(Category), "Category cannot be null or empty.");
            if (Price <= 0)
                throw new ArgumentException("Price must be greater than 0.", nameof(Price));

            return new Book(
                name: Name,
                description: Description,
                category: Category,
                price: Price
            //authors: Authors.Select(a => a.MapToAuthor()).ToList()
            );
        }
    }
}
