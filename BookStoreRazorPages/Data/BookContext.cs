using BookStoreRazorPages.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreRazorPages.Data;

public class BookContext : DbContext
{

    public BookContext(DbContextOptions<BookContext> options) : base(options)
    {
    }

    public DbSet<Book> Book { get; set; }
    public DbSet<Author> Author { get; set; }
    public DbSet<Photo> Photo { get; set; }

}
