using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        [Required]
        public string GenreName { get; set; }
    }
}