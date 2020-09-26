using System.ComponentModel.DataAnnotations;

namespace AdvancedTodo.Models {
public class Todo {
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public int UserId { get; set; }
    public int TodoId { get; set; }
    [Required, MaxLength(128)]
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}
}