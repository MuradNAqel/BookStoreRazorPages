using BookStoreRazorPages.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreRazorPages.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookContext(
            serviceProvider.GetRequiredService<DbContextOptions<BookContext>>()))
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
                {
                    Authors = {
                        new Author
                        {
                            Age = 32,
                            CreatedAt = DateTime.UtcNow,
                            CreatedBy = "Author Name",
                            EditedBy = "Not Edited",
                            Id = 1,
                            IsSoftDeleted = false,
                            Name = "Author Name",
                            Books = { },
                            Speciality = "Novels"
                        }
                    },
                    Id = 1,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "Author Name",
                    EditedBy = "Not Edited",
                    IsSoftDeleted = false,
                    Category = "Science fiction",
                    Name = "Book Name",
                    Description = "Some description on the book",
                    Photo = new Photo { },
                    Price = 10,
                });
        }
    }
}
