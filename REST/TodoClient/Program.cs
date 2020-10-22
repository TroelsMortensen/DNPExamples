using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TodoClient {
class Program {
    
    static async Task Main(string[] args) {
        //string result = await new Program().PostData();
       /* IList<Todo> todosAsync = await new Program().GetTodosAsync();
        foreach (Todo todo in todosAsync) {

            Console.WriteLine(todo);
        }*/

       Task data = new Program().GetData();
       await data;

       //Console.WriteLine(result);
    }

    private async Task GetData() {
        HttpClient client = new HttpClient();
        string stringAsync = await client.GetStringAsync("https://jsonplaceholder.typicode.com/todos");
        Console.WriteLine(stringAsync);
    }
    
    private string uri = "http://jsonplaceholder.typicode.com";

    public async Task<IList<Todo>> GetTodosAsync() {
        HttpClient client = new HttpClient();
        Task<string> stringAsync = client.GetStringAsync(uri+"/todos");
        string message = await stringAsync;
        List<Todo> result = JsonSerializer.Deserialize<List<Todo>>(message);
        return result;
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