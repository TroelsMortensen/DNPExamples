using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Range(0, Int32.MaxValue)]
        public int TotalPages { get; set; }
        public DateTime PublishDate { get; set; }
        [Required]
        public Author Author { get; set; }
        public Genre Genre { get; set; }
    }
}