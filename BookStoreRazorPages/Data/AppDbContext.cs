using BookStoreRazorPages.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreRazorPages.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Book { get; set; }
    public DbSet<Author> Author { get; set; }
    public DbSet<Photo> Photo { get; set; }

}
