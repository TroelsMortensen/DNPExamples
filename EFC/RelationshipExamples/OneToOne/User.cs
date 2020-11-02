using System.ComponentModel.DataAnnotations;

namespace OneToOne.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public ProfileInfo Info { get; set; }
    }
}