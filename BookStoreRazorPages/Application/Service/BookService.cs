using BookStoreRazorPages.Application.Dtos.BookDtos;
using BookStoreRazorPages.Application.Entities;
using BookStoreRazorPages.Application.IService;
using BookStoreRazorPages.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BookStoreRazorPages.Application.Service
{
    public class BookService : IBookServices
    {
        private static BookDto bookDto = new BookDto();
        private readonly AppDbContext _context;
        private readonly IAuthorService _authorService;
        private readonly IPhotoService _photoService;

        public BookService(AppDbContext context, IAuthorService authorService, IPhotoService photoService)
        {
            _context = context;
            _authorService = authorService;
            _photoService = photoService;
        }

        public async Task<BookDto> Create(CreateBookDto createBookDto, ModelStateDictionary modelState)
        {
            try
            {
                Book book = createBookDto.MapToBook();

                var listAuthers = new List<Author>();
                foreach (var id in createBookDto.Authors)
                {
                    var auth = await _authorService.GetObj(id);

                    listAuthers.Add(auth);
                }

                book.SetAuthors(listAuthers);


                List<Photo> photos = new List<Photo>();
                foreach (var file in createBookDto.Photos)
                {
                    var photoDto = await _photoService.Upload(file.FormFile, modelState);
                    photos.Add(
                            new Photo(
                                photoDto.Name,
                                photoDto.Path,
                                photoDto.FileExtension,
                                photoDto.Size
                                ));
                }

                book.SetPhotos(photos);

                var entity = await _context.Book.AddAsync(book);
                await _context.SaveChangesAsync();

                return bookDto.MapToBookDto(book);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured creating the book", ex);
            }

        }



        public async Task<BookDto> Delete(int id)
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

                return bookDto.MapToBookDto(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured deleting the book", ex);
            }
        }
        // returns null on async await  connection started but...
        public Task<List<BookDto>> GetAll()
        {
            List<Book> books = _context.Book.Where(b => b.IsSoftDeleted == false)
                .Include(book => book.Authors)
                .ToList();

            List<BookDto> booksDto = books.Select(bookDto.MapToBookDto).ToList();

            return Task.FromResult(booksDto);
        }

        public async Task<BookDto> Get(int id)
        {
            var book = await _context.Book.FindAsync(id);

            return bookDto.MapToBookDto(book);
        }

        public async Task<BookDto> Update(int id, EditBookDto editBookDto)
        {
            try
            {
                var entity = await _context.Book
                    .Where(b => b.Id == id)
                    .Include(b => b.Authors) // Include related authors
                    .FirstOrDefaultAsync(); // Get the specific book or null if not found

                if (entity == null)
                {
                    throw new ArgumentException("The book with the id " + id + " was not found.", nameof(editBookDto));
                }

                entity.SetName(editBookDto.Name);
                entity.SetDescription(editBookDto.Description);
                entity.SetPrice(editBookDto.Price);
                entity.SetCategory(editBookDto.Category);

                // Retrieve the original existing authors as a list
                var existingAuthors = entity.Authors.ToList();
                var newAuthors = new List<Author>();

                foreach (int authorId in editBookDto.Authors)
                {
                    var auth = await _authorService.GetObj(authorId);
                    if (auth != null)
                        newAuthors.Add(auth);
                }

                // Find authors to add (in newAuthors but not in existingAuthors)
                var authorsToAdd = newAuthors
                    .Where(newAuth => !existingAuthors.Any(existingAuth => existingAuth.Id == newAuth.Id))
                    .ToList();

                // Find authors to remove (in existingAuthors but not in newAuthors)
                var authorsToRemove = existingAuthors
                    .Where(existingAuth => !newAuthors.Any(newAuth => newAuth.Id == existingAuth.Id))
                    .ToList();

                // Add new authors to the book
                foreach (var author in authorsToAdd)
                {
                    entity.Authors.Add(author);
                }

                // Remove authors from the book
                foreach (var author in authorsToRemove)
                {
                    entity.Authors.Remove(author);
                }

                // Update the book's authors
                entity.SetAuthors(entity.Authors.ToList());

                await _context.SaveChangesAsync();

                return bookDto.MapToBookDto(entity);
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
