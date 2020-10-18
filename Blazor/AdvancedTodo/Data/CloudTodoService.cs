using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AdvancedTodo.Models;

namespace AdvancedTodo.Data {
public class CloudTodoService : ITodosService{
    
    public async Task<IList<Todo>> GetTodosAsync() {
        HttpClient client = new HttpClient();
        string message = await client.GetStringAsync("https://jsonplaceholder.typicode.com/todos");
        Console.WriteLine(message);
        List<Todo> result = JsonSerializer.Deserialize<List<Todo>>(message);
        return result;
    }

    public async Task AddTodoAsync(Todo todo) {
        
    }

    public async Task RemoveTodoAsync(int todoId) {
        
    }

    public async Task UpdateAsync(Todo todo) {
        
    }
}
}