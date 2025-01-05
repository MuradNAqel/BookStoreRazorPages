using BookStoreRazorPages.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreRazorPages.Data;

public class BookContext : DbContext
{

    public BookContext(DbContextOptions<BookContext> options) : base(options)
    {
    }

    public DbSet<Book> Book { get; set; }

}
