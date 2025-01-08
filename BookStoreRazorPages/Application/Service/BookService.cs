using BookStoreRazorPages.Application.Dtos.BookDtos;
using BookStoreRazorPages.Application.Entities;
using BookStoreRazorPages.Application.IService;
using BookStoreRazorPages.Data;

namespace BookStoreRazorPages.Application.Service
{
    public class BookService : IBookServices
    {
        private readonly BookContext _context;

        public BookService(BookContext context)
        {
            _context = context;
        }

        public async Task<BookDto> Create(CreateBookDto createBookDto)
        {
            try
            {
                Book book = createBookDto.MapToBook();

                var entity = await _context.Book.AddAsync(book);
                await _context.SaveChangesAsync();

                return new BookDto
                {
                    Description = entity.Entity.Description,
                    Price = entity.Entity.Price,
                    Category = entity.Entity.Category,
                    Name = entity.Entity.Name,
                    Id = entity.Entity.Id,
                };

            }
            catch (Exception ex)
            {
                throw new Exception("An error occured creating the book", ex);
            }

        }

        public async Task<BookDto> DeleteBook(int id)
        {
            try
            {
                var entity = await _context.Book.FindAsync(id);
                if (entity == null)
                {
                    throw new ArgumentException("The book with the id " + id + " was not found.", nameof(id));
                }
                entity.IsSoftDeleted = true;
                await _context.SaveChangesAsync();

                return entity.MapToBookDto();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured deleting the book", ex);
            }
        }

        public Task<List<BookDto>> GetAllBooks()
        {
            List<Book> books = _context.Book.Where(b => b.IsSoftDeleted == false).ToList();

            List<BookDto> booksDto = books.Select(book => book.MapToBookDto()).ToList();

            return Task.FromResult(booksDto);
        }

        public async Task<BookDto> UpdateBook(int id, EditBookDto editBookDto)
        {
            try
            {
                var entity = await _context.Book.FindAsync(id);
                if (entity == null)
                {
                    throw new ArgumentException("The book with the id " + id + " was not found.", nameof(editBookDto));
                }
                entity.SetName(editBookDto.Name);
                entity.SetDescription(editBookDto.Description);
                entity.SetPrice(editBookDto.Price);
                entity.SetCategory(editBookDto.Category);
                //entity.SetAuthors
                await _context.SaveChangesAsync();

                return entity.MapToBookDto();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured editing the book", ex);
            }
        }

        //public async Task<ResponseDto<CreateBookDto>> Create(CreateBookDto createBookDto)
        //{
        //    ResponseDto<CreateBookDto> responseDto = new ResponseDto<CreateBookDto>();

        //    try
        //    {
        //        Book book = createBookDto.MapToBook(createBookDto);
        //        await _context.Book.AddAsync(book);

        //        responseDto.SetSuccessWithPayload(createBookDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        responseDto.SetFailureWithError("Error on Creating Book", ex.Message);
        //    }

        //    return responseDto;
        //}

    }
}
