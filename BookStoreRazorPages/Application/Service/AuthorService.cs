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

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AuthorDto> Create(CreateAuthorDto createAuthorDto)
        {
            Author author = createAuthorDto.MapToAuthor();

            await _context.Author.AddAsync(author);

            return author.MapToAuthorDto();
        }

        public async Task<AuthorDto> Delete(int id)
        {
            var author = await _context.Author.FindAsync(id);

            author.IsSoftDeleted = true;

            await _context.SaveChangesAsync();

            return author.MapToAuthorDto();
        }

        public async Task<List<AuthorDto>> GetAll()
        {
            List<Author> authors = await _context.Author.Where(a => !a.IsSoftDeleted).ToListAsync();

            List<AuthorDto> authorsDto = authors.Select(a => a.MapToAuthorDto()).ToList();

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

            return author.MapToAuthorDto();

        }
    }
}
