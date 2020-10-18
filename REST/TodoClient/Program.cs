using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TodoClient {
class Program {
    
    static async Task Main(string[] args) {
        string result = await new Program().PostData();
        Console.WriteLine(result);
    }

    private async Task<string> PostData() {
        HttpClient client = new HttpClient();
        Todo todo = new Todo {
            Title = "My title",
            IsCompleted = false,
            UserId = 1
        };
        string todoSerialized = JsonSerializer.Serialize(todo);

        StringContent content = new StringContent(
            todoSerialized,
            Encoding.UTF8,
            "application/json"
            );

        HttpResponseMessage response = await client.PostAsync("https://jsonplaceholder.typicode.com/todos", content);
        return response.ToString();
    } 
}
}