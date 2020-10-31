using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace AdvancedTodoWebAPIDB.Models {
public class Todo {
    [Required]
    [NotNull, Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    
    [JsonPropertyName("id")]
    public int TodoId { get; set; }
    
    [Required, MaxLength(128)]
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("completed")]
    public bool IsCompleted { get; set; }
}
}