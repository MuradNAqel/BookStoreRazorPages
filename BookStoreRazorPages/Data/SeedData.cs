using BookStoreRazorPages.Application.Entities;
using Microsoft.EntityFrameworkCore;
namespace BookStoreRazorPages.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
        {
            //Check if existant
            if (context == null || context.Book == null)
            {
                throw new ArgumentNullException("Null DbContext Book");
            }
            //Checked if initiallized 
            if (context.Book.Any())
            {
                return;
            }

            context.Book.AddRange(
                new Book
                (
                    name: "Coding Miracles",
                    description: "All included zero to hero supervan",
                    category: "Programming",
                    price: 19
                ));
            context.Book.FirstOrDefault().AddPhoto(
                new Photo(
                    path: "path to image",
                    fileExtension: "png",
                    size: 87000
                ));
            context.Book.FirstOrDefault().AddAuthor(
                new Author(
                    name: "Qais",
                    dateOfBirth: DateTime.Today.AddYears(-32),
                    biography: "A good long journey of creating",
                    nationality: Nationality.Palestenian,
                    speciality: "Thriller Codeing Novels"
                ));
            context.SaveChanges();
        }
    }
}
