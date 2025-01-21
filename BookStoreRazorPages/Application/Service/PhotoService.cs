using BookStoreRazorPages.Application.Dtos.PhotoDtos;
using BookStoreRazorPages.Application.IService;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStoreRazorPages.Application.Service
{
    public class PhotoService : IPhotoService
    {
        private readonly long _fileSizeLimit = 4 * 1024 * 1024; // 4 MB
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".tiff" };
        private readonly string _targetFolder = "assets/images";
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotoService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<PhotoDto> Upload(IFormFile formFile, ModelStateDictionary modelState)
        {
            if (formFile == null || formFile.Length == 0)
            {
                modelState.AddModelError("File", "The file is empty.");
                return new PhotoDto();
            }

            var fileExtension = Path.GetExtension(formFile.FileName).ToLowerInvariant();
            if (!_permittedExtensions.Contains(fileExtension))
            {
                modelState.AddModelError("File", "Invalid file extension.");
                return new PhotoDto();
            }

            if (formFile.Length > _fileSizeLimit)
            {
                modelState.AddModelError("File", $"File size exceeds {_fileSizeLimit / (1024 * 1024)} MB.");
                return new PhotoDto();
            }

            string wwwrootPath = _webHostEnvironment.WebRootPath;

            if (string.IsNullOrWhiteSpace(wwwrootPath))
            {
                modelState.AddModelError("File", "Server configuration error: 'wwwroot' directory not found.");
                return new PhotoDto();
            }

            // Generate a unique file name
            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(formFile.FileName)}_{Guid.NewGuid()}{fileExtension}";
            var targetFilePath = Path.Combine(wwwrootPath, _targetFolder, uniqueFileName);

            try
            {
                // Ensure the target folder exists
                var targetDirectory = Path.Combine(wwwrootPath, _targetFolder);
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                // Save the file to the target path
                using (var fileStream = new FileStream(targetFilePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                // Return the file details
                return new PhotoDto
                {
                    Name = formFile.FileName,
                    FileExtension = fileExtension,
                    Path = $"/{_targetFolder}/{uniqueFileName}".Replace("\\", "/"), // Relative path for client usage
                    Size = formFile.Length,
                };
            }
            catch (Exception ex)
            {
                modelState.AddModelError("File", $"An error occurred while saving the file: {ex.Message}");
                return new PhotoDto();
            }
        }
    }
}
