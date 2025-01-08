using BookStoreRazorPages.Application.Entities;

namespace BookStoreRazorPages.Application.Dtos.AuthorDtos
{
    public class CreateAuthorDto
    {
        public string Name { get; set; }
        public string Speciality { get; set; }
        public string Biography { get; set; }
        public string ImagePath { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Nationality Nationality { get; set; }

        public Author MapToAuthor()
        {
            Author author = new Author
                (
                    name: Name,
                    dateOfBirth: DateOfBirth,
                    nationality: Nationality,
                    biography: Biography,
                    speciality: Speciality
                );
            author.ImagePath = ImagePath;
            return author;
        }
    }
}
