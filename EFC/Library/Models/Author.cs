using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }
        [StringLength(250)]
        public string Bio { get; set; }
    }
}