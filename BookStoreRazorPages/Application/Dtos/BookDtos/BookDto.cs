using BookStoreRazorPages.Application.Dtos.AuthorDtos;
using BookStoreRazorPages.Application.Entities;

namespace BookStoreRazorPages.Application.Dtos.BookDtos
{
    public class BookDto
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } //for now 
        public string Name { get; set; }
        public List<AuthorDto> Authors { get; set; }

        private AuthorDto _author = new AuthorDto();

        public BookDto MapToBookDto(Book book)
        {

            List<AuthorDto> authorDto = new List<AuthorDto>();
            if (book.Authors != null && book.Authors.Count > 0)
            {
                foreach (var author in book.Authors)
                {
                    var a = _author.MapToAuthorDto(author);
                    authorDto.Add(a);

                }
            }
            return new BookDto
            {
                Name = book.Name,
                Category = book.Category,
                Description = book.Description,
                Price = book.Price,
                Id = book.Id,
                Authors = authorDto
            };
        }

        public Book MapToBook()
        {

            return new Book(
                name: Name,
                description: Description,
                category: Category,
                price: Price,
                authors: Authors.Select(author => author.MapToAuthor()).ToList()
                );
        }

    }
}
