using BookStoreRazorPages.Application.Dtos.AuthorDtos;
using BookStoreRazorPages.Application.Dtos.BookDtos;
using BookStoreRazorPages.Application.Dtos.PhotoDtos;
using BookStoreRazorPages.Application.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoreRazorPages.Pages.Books;

public class IndexModel : PageModel
{
    private readonly IBookServices _bookService;
    private readonly IAuthorService _authorService;
    private readonly IPhotoService _photoService;
    public IndexModel(IBookServices bookServices, IAuthorService authorService, IPhotoService photoService)
    {
        _bookService = bookServices;
        _authorService = authorService;
        _photoService = photoService;
    }

    [BindProperty]
    public IndexVM VM { get; set; }
    [BindProperty]
    public EditPhotoVM editPhotoVM { get; set; }
    [BindProperty]
    public List<BookDto> Books { get; set; }
    [BindProperty]
    public List<AuthorDto> Authors { get; set; }
    [BindProperty]
    public string SearchText { get; set; }
    public int TotalPages { get; set; } = 1;
    public int Page { get; set; } = 1;
    public async Task OnGet()
    {
        var NumOfBooks = await _bookService.GetCount();
        TotalPages = NumOfBooks > 0
            ? (int)Math.Ceiling((double)NumOfBooks / 10)
            : 1;

        Books = await _bookService.GetAll(Page, "");

        Authors = await _authorService.GetAll();
    }

    public async Task<IActionResult> OnGetAllBooks([FromQuery] int page, [FromQuery] string searchText)
    {
        var NumOfBooks = await _bookService.GetCount();
        TotalPages = NumOfBooks > 0
            ? (int)Math.Ceiling((double)NumOfBooks / 10)
            : 1;
        Console.WriteLine("Before assign");
        Console.WriteLine($"page: {page.ToString()}, searchText: {searchText}");
        Console.WriteLine($"Page: {Page.ToString()}, SearchText: {SearchText}");

        Page = page;
        SearchText = searchText;

        Console.WriteLine("After assign");
        Console.WriteLine($"page: {page.ToString()}, searchText: {searchText}");
        Console.WriteLine($"Page: {Page.ToString()}, SearchText: {SearchText}");

        Books = await _bookService.GetAll(Page, SearchText);
        Authors = await _authorService.GetAll();

        return new JsonResult(new
        {
            success = true,
            message = "Books fetched successfully!",
            data = new
            {
                books = Books,
                totalPages = TotalPages
            }
        });

    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return new JsonResult(new { success = false, message = "Invalid data received", errors });
        }

        // Map IndexVM to CreateBookDto
        CreateBookDto cBookDto = new CreateBookDto
        {
            Name = VM.Name,
            Description = VM.Description,
            Category = VM.Category,
            Price = VM.Price,
            Authors = VM.Authors
        };

        //stor image 
        // info image that you need to send in object => photoDto 
        List<CreatePhotoDto> cPhotoDto = new List<CreatePhotoDto>();
        foreach (var file in VM.FormFiles)
        {
            CreatePhotoDto photoDto = new CreatePhotoDto
            {
                FormFile = file
            };
            cPhotoDto.Add(photoDto);
        }

        cBookDto.Photos = cPhotoDto;
        // Call the service to create the book

        var book = await _bookService.Create(cBookDto, ModelState);


        if (book == null)
        {
            return new JsonResult(new { success = false, message = "Failed to create the book." });
        }


        return new JsonResult(new { success = true, message = "Book created successfully!", data = book });
    }
    //create photo service

    [Route("Books/Edit/{id}")]
    public async Task<IActionResult> OnPostEdit(int id)
    {

        EditPhotoDto editPhotoDto = new EditPhotoDto
        {
            NewPhotos = editPhotoVM.NewPhotos,
        };

        EditBookDto editDto = new EditBookDto
        {
            Name = VM.Name,
            Description = VM.Description,
            Category = VM.Category,
            Price = VM.Price,
            Authors = VM.Authors,
            EditPhotoDto = editPhotoDto
        };

        if (!ModelState.IsValid)
        {
            return new JsonResult(new { success = false, message = "Invalid data received" });
        }

        var result = await _bookService.Update(id, editDto, ModelState);

        if (result == null)
        {
            return new JsonResult(new { success = false, message = "Failed to update the book." });
        }

        return new JsonResult(new { success = true, message = "Book updated successfully!", data = result });
    }

    [Route("Books/Delete/{id}")]
    public async Task<IActionResult> OnPostDelete(int id)
    {
        var isDeletedBook = await _bookService.Delete(id);

        if (isDeletedBook == null)
        {
            return new JsonResult(new { success = false, message = "Failed to delete the book." });
        }

        return new JsonResult(new { success = true, message = "Book deleted successfully!" });
    }

    //public async Task<IActionResult> OnPostUploadAsync()
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return new JsonResult(new { success = false, message = "Invalid data received" });
    //    }

    //    //Inject service Photo
    //    //MAp VM to dto 
    //    foreach (var photo in FileUpload.FormFiles)
    //    {
    //        PhotoDto photoDto = new PhotoDto
    //        {

    //        };
    //    }

    //    // _photoService.Upload(photoDto);


    //    return RedirectToPage("./Index");
    //}

    public class IndexVM
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } //for now 
        public string Name { get; set; }

        public List<int>? Authors { get; set; }

        public List<IFormFile>? FormFiles { get; set; }

    }
    public class EditPhotoVM
    {
        public List<IFormFile>? NewPhotos { get; set; } // For replacing the photo
    }
    //public class BufferedMultipleFileUploadPhysical
    //{
    //    [Required]
    //    [Display(Name = "File")]
    //    public List<IFormFile> FormFiles { get; set; }
    //    //public int BookId { get; set; }
    //    //link when the book is created in the book Service
    //}
}
