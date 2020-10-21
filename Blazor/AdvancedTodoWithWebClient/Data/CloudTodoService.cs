using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AdvancedTodo.Models;

namespace AdvancedTodo.Data {
public class CloudTodoService : ITodosService {

    private string uri = "https://localhost:5003";
    private string uri1 = "http://jsonplaceholder.typicode.com";
    
    public async Task<IList<Todo>> GetTodosAsync() {
        HttpClient client = new HttpClient();
        string message = await client.GetStringAsync(uri+"/todos");
        List<Todo> result = JsonSerializer.Deserialize<List<Todo>>(message);
        return result;
    }

    public async Task AddTodoAsync(Todo todo) {
        HttpClient client = new HttpClient();
        string todoAsJson = JsonSerializer.Serialize(todo);
        HttpContent content = new StringContent(todoAsJson,
            Encoding.UTF8,
            "application/json");
        await client.PostAsync(uri+"/todos", content);
    }

    public async Task RemoveTodoAsync(int todoId) {
        HttpClient client = new HttpClient();
        await client.DeleteAsync($"{uri}/todos/{todoId}");
    }

    public async Task UpdateAsync(Todo todo) {
        HttpClient client = new HttpClient();
        string todoAsJson = JsonSerializer.Serialize(todo);
        HttpContent content = new StringContent(todoAsJson,
            Encoding.UTF8,
            "application/json");
        await client.PatchAsync($"{uri}/todos/{todo.TodoId}", content);
    }
}
}