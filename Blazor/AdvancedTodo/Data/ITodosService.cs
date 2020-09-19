using System.Collections.Generic;
using AdvancedTodo.Models;

namespace AdvancedTodo.Data {
public interface ITodosService {
    IList<Todo> GetTodos();
    Todo        GetTodoById(int id);
    IList<Todo> GetTodosByUserId(int id);
    void        AddTodo(Todo todo);
    void        RemoveTodo(int todoId);
    void        Update(Todo todo);
}
}