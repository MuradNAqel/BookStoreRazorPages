using BookStoreRazorPages.Application.Dtos.BookDtos;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStoreRazorPages.Application.IService
{
    public interface IBookServices
    {
        public Task<BookDto> Create(CreateBookDto createBookDto, ModelStateDictionary modelState);
        public Task<BookDto> Update(int id, EditBookDto editBookDto, ModelStateDictionary modelState);
        public Task<BookDto> Delete(int id);
        public Task<List<BookDto>> GetAll();
        public Task<BookDto> Get(int id);
    }
}
