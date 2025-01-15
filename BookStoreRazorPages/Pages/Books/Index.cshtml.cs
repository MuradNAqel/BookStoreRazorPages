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
    //[BindProperty]
    //public EditBookDto editBookVM { get; set; }
    [BindProperty]
    public List<BookDto> Book { get; set; }
    [BindProperty]
    public List<AuthorDto> Author { get; set; }
    //[BindProperty]
    //public BufferedMultipleFileUploadPhysical FileUpload { get; set; }

    public async void OnGet()
    {
        Book = await _bookService.GetAll();
        Author = await _authorService.GetAll();



    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return new JsonResult(new { success = false, message = "Invalid data received" });
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

        var book = _bookService.Create(cBookDto, ModelState);


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
        EditBookDto editDto = new EditBookDto
        {
            Name = VM.Name,
            Description = VM.Description,
            Category = VM.Category,
            Price = VM.Price,
            Authors = VM.Authors
        };

        if (!ModelState.IsValid)
        {
            return new JsonResult(new { success = false, message = "Invalid data received" });
        }

        var result = await _bookService.Update(id, editDto);

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
        //public int Id { get; set; }
        public List<int>? Authors { get; set; }
        //public List<PhotoDto> Photos { get; set; }
        public List<IFormFile>? FormFiles { get; set; }

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
