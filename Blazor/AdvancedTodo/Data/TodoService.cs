using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AdvancedTodo.Models;

namespace AdvancedTodo.Data {
public class TodoService : ITodosService {

    private string todoFile = "todos.json";
    private IList<Todo> todos;

    public TodoService() {
        if (!File.Exists(todoFile)) {
            Seed();
            WriteTodosToFile();
        } else {
            string content = File.ReadAllText(todoFile);
            todos = JsonSerializer.Deserialize<List<Todo>>(content);
        }
    }

    public IList<Todo> GetTodos() {
        List<Todo> tmp = new List<Todo>(todos);
        return tmp;
    }

    public void AddTodo(Todo todo) {
        int max = todos.Max(todo => todo.TodoId);
        todo.TodoId = (++max);
        todos.Add(todo);
        WriteTodosToFile();
    }

    public void RemoveTodo(int todoId) {
        Todo toRemove = todos.First(t => t.TodoId == todoId);
        todos.Remove(toRemove);
        WriteTodosToFile();
    }

    public void Update(Todo todo) {
        Todo toUpdate = todos.First(t => t.TodoId == todo.TodoId);
        toUpdate.IsCompleted = todo.IsCompleted;
        WriteTodosToFile();
    }

    private void WriteTodosToFile() {
        string productsAsJson = JsonSerializer.Serialize(todos);
        File.WriteAllText(todoFile, productsAsJson);
    }

    private void Seed() {
        Todo[] ts = {
            new Todo {
                UserId = 1,
                TodoId = 1,
                Title = "Do dishes",
                IsCompleted = false
            },
            new Todo {
                UserId = 1,
                TodoId = 2,
                Title = "Walk the dog",
                IsCompleted = false
            },
            new Todo {
                UserId = 2,
                TodoId = 3,
                Title = "Do DNP homework",
                IsCompleted = true
            },
            new Todo {
                UserId = 3,
                TodoId = 4,
                Title = "Eat breakfast",
                IsCompleted = false
            },
            new Todo {
                UserId = 4,
                TodoId = 5,
                Title = "Mow lawn",
                IsCompleted = true
            },
        };
        todos = ts.ToList();
    }
}
}