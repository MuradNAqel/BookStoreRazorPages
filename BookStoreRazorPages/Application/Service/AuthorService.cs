using BookStoreRazorPages.Application.Dtos.AuthorDtos;
using BookStoreRazorPages.Application.Entities;
using BookStoreRazorPages.Application.IService;
using BookStoreRazorPages.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreRazorPages.Application.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;
        private static AuthorDto _authorDto = new AuthorDto();

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AuthorDto> Create(CreateAuthorDto createAuthorDto)
        {
            Author author = createAuthorDto.MapToAuthor();

            await _context.Author.AddAsync(author);

            return _authorDto.MapToAuthorDto(author);
        }

        public async Task<AuthorDto> Delete(int id)
        {
            var author = await _context.Author.FindAsync(id);

            author.IsSoftDeleted = true;

            await _context.SaveChangesAsync();

            return _authorDto.MapToAuthorDto(author);
        }

        public async Task<AuthorDto> Get(int id)
        {
            var author = await _context.Author.FindAsync(id);

            return _authorDto.MapToAuthorDto(author);
        }

        public async Task<Author> GetObj(int id)
        {
            var author = await _context.Author.SingleOrDefaultAsync(a => a.Id == id);

            return author;
        }

        public async Task<List<AuthorDto>> GetAll()
        {
            List<Author> authors = _context.Author.Where(a => !a.IsSoftDeleted).ToList();

            List<AuthorDto> authorsDto = authors.Select(author => _authorDto.MapToAuthorDto(author)).ToList();

            return authorsDto;

        }

        public async Task<AuthorDto> Update(int id, EditAuthorDto editAuthorDto)
        {
            var author = await _context.Author.FindAsync(id);

            if (author == null)
            {
                throw new ArgumentException("The author with the id " + id + " was not found.", nameof(editAuthorDto));
            }
            author.SetDateOfBirth(editAuthorDto.DateOfBirth);
            author.SetBiography(editAuthorDto.Biography);
            author.SetName(editAuthorDto.Name);
            author.SetImagePath(editAuthorDto.ImagePath);
            author.SetNationality(editAuthorDto.Nationality);
            author.SetSpeciality(editAuthorDto.Speciality);

            return _authorDto.MapToAuthorDto(author);

        }
    }
}
