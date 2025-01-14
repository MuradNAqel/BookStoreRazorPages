using BookStoreRazorPages.Application.Dtos.AuthorDtos;
using BookStoreRazorPages.Application.Dtos.BookDtos;
using BookStoreRazorPages.Application.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoreRazorPages.Pages.Books;

public class IndexModel : PageModel
{
    private readonly IBookServices _bookService;
    private readonly IAuthorService _authorService;

    public IndexModel(IBookServices bookServices, IAuthorService authorService)
    {
        _bookService = bookServices;
        _authorService = authorService;
    }

    [BindProperty]
    public IndexVM VM { get; set; }
    //[BindProperty]
    //public EditBookDto editBookVM { get; set; }
    [BindProperty]
    public List<BookDto> Book { get; set; }
    [BindProperty]
    public List<AuthorDto> Author { get; set; }

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
        CreateBookDto cbDto = new CreateBookDto
        {
            Name = VM.Name,
            Description = VM.Description,
            Category = VM.Category,
            Price = VM.Price,
            Authors = VM.Authors
        };

        // Call the service to create the book
        var book = _bookService.Create(cbDto);


        if (book == null)
        {
            return new JsonResult(new { success = false, message = "Failed to create the book." });
        }


        return new JsonResult(new { success = true, message = "Book created successfully!", data = book });
    }

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

    public class IndexVM
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } //for now 
        public string Name { get; set; }
        //public int Id { get; set; }
        public List<int>? Authors { get; set; }
    }
}
