namespace AdvancedTodo.Models {
public class Todo {
    public int UserId { get; set; }
    public int TodoId { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}
}