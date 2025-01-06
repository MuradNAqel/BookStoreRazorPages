using System.ComponentModel.DataAnnotations;

namespace BookStoreRazorPages.Application.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
    }
}
