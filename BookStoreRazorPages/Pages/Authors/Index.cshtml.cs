using BookStoreRazorPages.Application.Dtos.AuthorDtos;
using BookStoreRazorPages.Application.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoreRazorPages.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly IAuthorService _authorService;

        public IndexModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [BindProperty]
        public List<AuthorDto> Author { get; set; }

        public async Task OnGet()
        {
            Author = await _authorService.GetAll();
        }


    }
}
