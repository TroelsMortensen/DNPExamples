using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AdvancedTodoWithWebClientAndLogin.Models;

namespace AdvancedTodoWithWebClientAndLogin.Data {
public class CloudTodoService : ITodosService {

    private string uri = "https://localhost:5003";
    // private string uri = "http://jsonplaceholder.typicode.com";
    private readonly HttpClient client;

    public CloudTodoService() {
        
        client = new HttpClient();
    }

    public async Task<IList<Todo>> GetTodosAsync() {
        Task<string> stringAsync = client.GetStringAsync(uri+"/todos");
        string message = await stringAsync;
        List<Todo> result = JsonSerializer.Deserialize<List<Todo>>(message);
        return result;
    }

    public async Task AddTodoAsync(Todo todo) {
        string todoAsJson = JsonSerializer.Serialize(todo);
        HttpContent content = new StringContent(todoAsJson,
            Encoding.UTF8,
            "application/json");
        await client.PostAsync(uri+"/todos", content);
    }

    public async Task RemoveTodoAsync(int todoId) {
        await client.DeleteAsync($"{uri}/todos/{todoId}");
    }

    public async Task UpdateAsync(Todo todo) {
        string todoAsJson = JsonSerializer.Serialize(todo);
        HttpContent content = new StringContent(todoAsJson,
            Encoding.UTF8,
            "application/json");
        await client.PatchAsync($"{uri}/todos/{todo.TodoId}", content);
    }
}
}