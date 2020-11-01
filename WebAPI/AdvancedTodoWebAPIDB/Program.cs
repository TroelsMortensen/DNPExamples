using System.Linq;
using AdvancedTodoWebAPIDB.Models;
using AdvancedTodoWebAPIDB.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AdvancedTodoWebAPIDB {
public class Program {
    public static void Main(string[] args) {
        using (TodoContext todoContext = new TodoContext())
        {
            if (!todoContext.Todos.Any())
            {
                Seed(todoContext);
            }
        }
        CreateHostBuilder(args).Build().Run();
    }

    private static void Seed(TodoContext todoContext)
    {
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
        foreach (Todo todo in ts)
        {
            todoContext.Add(todo);
        }

        todoContext.SaveChanges();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}
}