using BookStoreRazorPages.Application.Entities;

namespace BookStoreRazorPages.Application.Dtos.AuthorDtos
{
    public class EditAuthorDto
    {
        public string Name { get; set; }
        public string Speciality { get; set; }
        public string Biography { get; set; }
        public string ImagePath { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Nationality Nationality { get; set; }
    }
}
