using BookStoreRazorPages.Application.Dtos.AuthorDtos;
using BookStoreRazorPages.Application.Entities;

namespace BookStoreRazorPages.Application.IService
{
    public interface IAuthorService
    {
        public Task<AuthorDto> Create(CreateAuthorDto createAuthorDto);
        public Task<AuthorDto> Update(int id, EditAuthorDto editAuthorDto);
        public Task<AuthorDto> Delete(int id);
        public Task<AuthorDto> Get(int id);
        public Task<List<AuthorDto>> GetAll();
        public Task<Author> GetObj(int id);
    }
}
