using System.Collections.Generic;
using System.Threading.Tasks;
using AdvancedTodoWebAPIDB.Models;

namespace AdvancedTodoWebAPIDB.Data {
public interface ITodosService {
    Task<IList<Todo>> GetTodosAsync();
    Task<Todo>   AddTodoAsync(Todo todo);
    Task   RemoveTodoAsync(int todoId);
    Task<Todo>   UpdateAsync(Todo todo);
}
}