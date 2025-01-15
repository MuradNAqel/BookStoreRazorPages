using BookStoreRazorPages.Application.Dtos.PhotoDtos;
using BookStoreRazorPages.Application.IService;
using BookStoreRazorPages.Application.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStoreRazorPages.Application.Service
{
    public class PhotoService : IPhotoService
    {
        private readonly long _fileSizeLimit = 4000000;
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".tiff" };
        private readonly string _targetFilePath = "C:\\Users\\User\\source\\repos\\BookStoreRazorPages\\BookStoreRazorPages\\wwwroot\\assets\\images\\";

        public async Task<PhotoDto> Upload(IFormFile FormFile, ModelStateDictionary modelState)
        {
            var fileExtension = Path.GetExtension(FormFile.FileName).ToLowerInvariant();
            if (!_permittedExtensions.Contains(fileExtension))
            {
                modelState.AddModelError("File", "Invalid file extension.");
                return new PhotoDto();
            }
            if (FormFile.Length > _fileSizeLimit)
            {
                modelState.AddModelError("File", $"File size exceeds {_fileSizeLimit / (1024 * 1024)} MB.");
                return new PhotoDto();
            }

            var formFileContent =
                await FileHelpers.ProcessFormFile<PhotoDto>(
                    FormFile, modelState, _permittedExtensions,
                    _fileSizeLimit);

            if (!modelState.IsValid)
            {
                //msg = "Please correct the form.";

                return new PhotoDto();
            }

            // For the file name of the uploaded file stored
            // server-side, use Path.GetRandomFileName to generate a safe
            // random file name.
            var trustedFileNameForFileStorage = FormFile.FileName;
            var filePath = Path.Combine(
                _targetFilePath, trustedFileNameForFileStorage);

            // **WARNING!**
            // In the following example, the file is saved without
            // scanning the file's contents. In most production
            // scenarios, an anti-virus/anti-malware scanner API
            // is used on the file before making the file available
            // for download or for use by other systems. 
            // For more information, see the topic that accompanies 
            // this sample.

            using (var fileStream = File.Create(filePath))
            {
                await fileStream.WriteAsync(formFileContent);

                return new PhotoDto
                {
                    Name = FormFile.FileName,
                    FileExtension = fileExtension,
                    Path = filePath,
                    Size = formFileContent.Length,
                };

                // To work directly with a FormFile, use the following
                // instead:
                //await FileUpload.FormFile.CopyToAsync(fileStream);
            }
        }
    }
}
