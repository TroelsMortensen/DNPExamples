using System.Collections.Generic;
using AdvancedTodo.Models;

namespace AdvancedTodo.Data {
public interface ITodosService {
    IList<Todo> GetTodos();
    void        AddTodo(Todo todo);
    void        RemoveTodo(int todoId);
    void        Update(Todo todo);
}
}