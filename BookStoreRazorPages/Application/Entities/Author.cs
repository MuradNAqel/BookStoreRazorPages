using System.ComponentModel.DataAnnotations;

namespace BookStoreRazorPages.Application.Entities
{
    public class Author : BaseEntityAudit
    {
        [MaxLength(50)]
        public string Speciality { get; protected set; }

        [MaxLength(200)]
        public string Biography { get; protected set; }

        public string ImagePath { get; set; }
        public DateTime DateOfBirth { get; protected set; }
        public Nationality Nationality { get; protected set; }

        //Navigation property, An author could have many books.
        public List<Book> Books { get; set; }


        public Author(string name, DateTime dateOfBirth, string biography, Nationality nationality, string speciality)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = name;

            SetName(name);
            SetBiography(biography);
            SetNationality(nationality);
            SetDateOfBirth(dateOfBirth);
        }

        public void SetImagePath(string imagePath)
        {
            if (imagePath == null)
            {
                throw new ArgumentException("Impage path null.", nameof(imagePath));
            }
            ImagePath = imagePath;
        }

        public void SetName(string name)
        {
            if (name == null || name.Length <= 3 || name.Length >= 50)
            {
                throw new ArgumentException("Name cannot be less than 3 characters or exceed 50 characters.", nameof(name));
            }
            Name = name;
        }

        public void SetSpeciality(string speciality)
        {
            if (speciality.Length > 50)
            {
                throw new ArgumentException("Speciality name can be maximum 50 characters long.", nameof(speciality));
            }
            Speciality = speciality;
        }

        public void SetBiography(string biography)
        {
            if (biography.Length > 50)
            {
                throw new ArgumentException("Biography should be less than 200 characters.", nameof(biography));
            }
            Biography = biography;
        }

        public void SetDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth.Year < 1940)
            {
                throw new ArgumentException("Date of birth can't be more than 100 years, 1940 and above allowed.", nameof(dateOfBirth));
            }
            DateOfBirth = dateOfBirth;
        }

        public void SetNationality(Nationality nationality)
        {
            if (nationality == null)
            {
                throw new ArgumentException("Nationality can't be empty.", nameof(nationality));
            }
            Nationality = nationality;
        }
    }
}
