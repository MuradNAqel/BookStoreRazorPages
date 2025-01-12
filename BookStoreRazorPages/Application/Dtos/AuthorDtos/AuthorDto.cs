using BookStoreRazorPages.Application.Entities;

namespace BookStoreRazorPages.Application.Dtos.AuthorDtos
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; }
        public string Biography { get; set; }
        public string ImagePath { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Nationality Nationality { get; set; }
        //public int BookId { get; set; }

        public Author MapToAuthor()
        {
            return new Author(
                    name: Name,
                    dateOfBirth: DateOfBirth,
                    biography: Biography,
                    nationality: Nationality,
                    speciality: Speciality
                );
        }

        public AuthorDto MapToAuthorDto(Author author)
        {
            return new AuthorDto
            {
                ImagePath = author.ImagePath,
                Biography = author.Biography,
                DateOfBirth = author.DateOfBirth,
                Nationality = author.Nationality,
                Speciality = author.Speciality,
                Name = author.Name,
                Id = author.Id,
            };
        }

    }
}
