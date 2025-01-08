using BookStoreRazorPages.Application.Dtos.BookDtos;

namespace BookStoreRazorPages.Application.IService
{
    public interface IBookServices
    {
        public Task<BookDto> Create(CreateBookDto createBookDto);
        public Task<BookDto> UpdateBook(int id, EditBookDto editBookDto);
        public Task<BookDto> DeleteBook(int id);
        public Task<List<BookDto>> GetAllBooks();
    }
}
