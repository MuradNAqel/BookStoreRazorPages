﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreRazorPages.Application.Entities
{
    [Table("Book")]
    public class Book : BaseEntityAudit
    {
        [MinLength(3)]
        [MaxLength(100)]
        public string Description { get; protected set; }

        [Range(1, 500, ErrorMessage = "Price must be between 1 and 500.")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; protected set; }

        [MaxLength(20)]
        public string Category { get; protected set; } //for now 

        //Navigation properties A Book can have many authors and Atmost one photo(for now).

        public List<Author> Authors { get; protected set; }
        public List<Photo> Photos { get; protected set; }

        public Book() { }

        public Book(string name, string description, string category, decimal price, List<Author> authors)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = Authors?.Any() == true
                        ? string.Join(", ", Authors
                                        .Where(author => !string.IsNullOrEmpty(author.Name))
                                        .Select(author => author.Name))
                        : "Not registered";

            SetName(name);
            SetDescription(description);
            SetCategory(category);
            SetPrice(price);
            SetAuthors(authors);
        }
        public Book(string name, string description, string category, decimal price, List<Author> authors, List<Photo> photos)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = Authors?.Any() == true
                        ? string.Join(", ", Authors
                                        .Where(author => !string.IsNullOrEmpty(author.Name))
                                        .Select(author => author.Name))
                        : "Not registered";

            SetName(name);
            SetDescription(description);
            SetCategory(category);
            SetPrice(price);
            SetAuthors(authors);
            SetPhotos(photos);
        }

        public Book(string name, string description, string category, decimal price)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = Authors?.Any() == true
                        ? string.Join(", ", Authors
                                        .Where(author => !string.IsNullOrEmpty(author.Name))
                                        .Select(author => author.Name))
                        : "Not registered";

            SetName(name);
            SetDescription(description);
            SetCategory(category);
            SetPrice(price);

        }

        public void SetPhotos(List<Photo> photos)
        {
            if (photos.Count < 1)
            {
                throw new ArgumentException("Cannot append an empty photos list.", nameof(photos));
            }
            Photos = photos;
        }

        public void AddPhoto(Photo photo)
        {
            if (photo == null)
            {
                throw new ArgumentException("photo obj in null", nameof(photo));
            }
            Photos.Add(photo);
        }

        public async void SetAuthors(List<Author> authors)
        {
            //check if authors are changed or not :: to avoid duplicate rows in DB
            if (authors.Count == 0)
            {
                throw new ArgumentException("Book must have an author", nameof(authors));
            }
            Authors = authors;
        }

        public void AddAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }
            Authors.Add(author);
        }

        public void SetName(string name)
        {
            if (name == null || name.Length <= 3 || name.Length >= 50)
            {
                throw new ArgumentException("Name cannot be less than 3 characters or exceed 50 characters.", nameof(name));
            }
            Name = name;
        }

        public void SetDescription(string description)
        {
            if (description == null || description.Length <= 3 || description.Length >= 100)
            {
                throw new ArgumentException("Name cannot belee than 3 characters or exceed 100 characters.", nameof(description));
            }
            Description = description;
        }

        public void SetPrice(decimal price)
        {
            if (price < 1 || price > 500)
            {
                throw new ArgumentException("Price should be between 1 and 500.", nameof(price));
            }
            Price = price;
        }

        public void SetCategory(string category)
        {
            if (category.Length > 20)
            {
                throw new ArgumentException("Category cannot be more than 20 characters.", nameof(category));
            }
            Category = category;
        }
    }
}
