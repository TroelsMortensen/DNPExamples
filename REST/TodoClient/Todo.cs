using System.Text.Json.Serialization;

namespace TodoClient {
public class Todo {
    public int UserId { get; set; }
    [JsonPropertyName("id")]
    public int TodoId { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}
}