namespace BookStoreRazorPages.Application.Dtos.BookDtos
{
    public class BookDto
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } //for now 
        public string Name { get; set; }

        public BookDto()
        {
        }

        public BookDto MapToBookDto()
        {
            return new BookDto
            {
                Category = Category,
                Name = Name,
                Price = Price,
                Description = Description,
                Id = Id
            };
        }

    }
}
