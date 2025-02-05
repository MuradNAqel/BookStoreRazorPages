﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreRazorPages.Application.Entities
{
    public class Photo : BaseEntity
    {

        public string Path { get; set; }

        [Column(TypeName = "VarChar(6)")]
        public string FileExtension { get; set; }

        [Column(TypeName = "decimal(10,2)")] //use MB
        public decimal Size { get; set; }

        //Navigation book has a photo
        public int BookId { get; set; }
        //public Book Book { get; set; }

        public Photo() { }
        public Photo(string path, string fileExtension, decimal size, int Bookid)
        {
            //SetFileExtension(fileExtension);
            //SetPath(path);
            //SetSize(size);
            Path = path;
            FileExtension = fileExtension;
            Size = size;
            BookId = Bookid;
        }

        public Photo(string name, string path, string fileExtension, decimal size)
        {
            //SetFileExtension(fileExtension);
            //SetPath(path);
            //SetSize(size);
            Name = name;
            Path = path;
            FileExtension = fileExtension;
            Size = size;
        }

        // string[] AllowedExtensions = { "jpg", "jpeg", "png", "gif", "bmp", "webp", "tiff" };

        public void SetPath(string path)
        {
            if (path == null)
            {
                throw new ArgumentException("Path is null", nameof(path));
            }
            Path = path;
        }



        //public void SetFileExtension(string fileExtension)
        //{
        //    if (!AllowedExtensions.Contains(fileExtension.ToLower()))
        //    {
        //        throw new ArgumentException("AllowedExtensions = { \"jpg\", \"jpeg\", \"png\", \"gif\", \"bmp\", \"webp\", \"tiff\" }", nameof(fileExtension));
        //    }
        //    FileExtension = fileExtension;
        //}

        public void SetSize(decimal size)
        {
            if (size > 16000000) //16000000Bit 2MB
            {
                throw new ArgumentException("size exceeded 2MB", nameof(size));
            }
            Size = size;
        }

    }

}
