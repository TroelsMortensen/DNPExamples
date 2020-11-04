using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        public string HairColor { get; set; }
        [Required] public string EyeColor { get; set; }
        [Required, Range(0, 125)] public int Age { get; set; }
        [Required, Range(1, 250)] public decimal Weight { get; set; }
        [Required, Range(30, 250)] public int Height { get; set; }
        [Required] public string Sex { get; set; }
    }
}