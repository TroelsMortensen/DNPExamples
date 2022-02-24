using System.Collections.Generic;
using System.Threading.Tasks;
using AdvancedTodoWebAPIDB.Models;

namespace AdvancedTodoWebAPIDB.Data {
public interface ITodosService {
    Task<Todo>   AddTodoAsync(Todo todo);
    Task   RemoveTodoAsync(int todoId);
    Task<Todo>   UpdateAsync(Todo todo);
    Task<IList<Todo>> GetTodosAsync(int? userId, bool? isCompleted);
}
}