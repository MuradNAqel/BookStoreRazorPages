using BookStoreRazorPages.Application.Dtos.PhotoDtos;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStoreRazorPages.Application.IService
{
    public interface IPhotoService
    {
        public Task<PhotoDto> Upload(IFormFile formFile, ModelStateDictionary modelState);
    }
}
