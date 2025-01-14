using BookStoreRazorPages.Application.Dtos.BookDtos;

namespace BookStoreRazorPages.Application.IService
{
    public interface IBookServices
    {
        public Task<BookDto> Create(CreateBookDto createBookDto);
        public Task<BookDto> Update(int id, EditBookDto editBookDto);
        public Task<BookDto> Delete(int id);
        public Task<List<BookDto>> GetAll();
        public Task<BookDto> Get(int id);
    }
}
