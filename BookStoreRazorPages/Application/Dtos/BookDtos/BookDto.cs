using BookStoreRazorPages.Application.Dtos.AuthorDtos;
using BookStoreRazorPages.Application.Dtos.PhotoDtos;
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
        public List<PhotoDto> Photos { get; set; }

        public BookDto MapToBookDto(Book book)
        {
            AuthorDto _author = new AuthorDto();

            List<AuthorDto> authorDto = new List<AuthorDto>();
            if (book.Authors != null && book.Authors.Count > 0)
            {
                foreach (var author in book.Authors)
                {
                    var a = _author.MapToAuthorDto(author);
                    authorDto.Add(a);
                }
            }

            List<PhotoDto> photos = new List<PhotoDto>();
            if (book.Photos != null && book.Photos.Count > 0)
            {
                foreach (var photo in book.Photos)
                {
                    PhotoDto photoDto = new PhotoDto
                    {
                        BookId = photo.BookId,
                        FileExtension = photo.FileExtension,
                        Path = photo.Path,
                        Size = photo.Size,
                    };
                    photos.Add(photoDto);
                }
            }
            return new BookDto
            {
                Name = book.Name,
                Category = book.Category,
                Description = book.Description,
                Price = book.Price,
                Id = book.Id,
                Authors = authorDto,
                Photos = photos,
            };
        }

        public Book MapToBook()
        {

            return new Book(
                name: Name,
                description: Description,
                category: Category,
                price: Price,
                authors: Authors.Select(author => author.MapToAuthor()).ToList(),
                photos: Photos.Select(
                    photo => new Photo
                    {
                        BookId = photo.BookId ?? -1,
                        FileExtension = photo.FileExtension,
                        Path = photo.Path,
                        Size = photo.Size,
                        Id = photo.Id ?? -1
                    }).ToList()
                );
        }

    }
}
