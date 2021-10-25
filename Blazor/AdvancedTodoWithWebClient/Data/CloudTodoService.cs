using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AdvancedTodo.Models;

namespace AdvancedTodo.Data
{
    public class CloudTodoService : ITodosService
    {
        private string uri = "https://localhost:5003";

        // private string uri = "http://jsonplaceholder.typicode.com";
        private readonly HttpClient client;

        public CloudTodoService()
        {
            client = new HttpClient();
        }

        public async Task<IList<Todo>> GetTodosAsync()
        {
            HttpResponseMessage reponse = await client.GetAsync(uri + "/todos");
            if (!reponse.IsSuccessStatusCode)
            {
                throw new Exception("Error or whatever");
            }

            string message = await reponse.Content.ReadAsStringAsync();
            List<Todo> result = JsonSerializer.Deserialize<List<Todo>>(message);
            return result;
        }

        public async Task AddTodoAsync(Todo todo)
        {
            string todoAsJson = JsonSerializer.Serialize(todo);
            HttpContent content = new StringContent(todoAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PostAsync(uri + "/todos", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task RemoveTodoAsync(int todoId)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{uri}/todos/{todoId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task UpdateAsync(Todo todo)
        {
            string todoAsJson = JsonSerializer.Serialize(todo);
            HttpContent content = new StringContent(todoAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PatchAsync($"{uri}/todos/{todo.TodoId}", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}