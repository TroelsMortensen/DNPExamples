using System;
using System.Collections.Generic;
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
        string[] words = ("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque condimentum a libero ultrices congue. Vestibulum tellus lectus, gravida non fringilla at, scelerisque nec neque. Proin non nulla et nisl porta pharetra ut id massa. Pellentesque sit amet ante quam. Proin a maximus urna, at sagittis odio. Ut auctor urna ac nibh tempus porta. In eu orci felis. Nullam tempus justo ligula, ut dignissim mauris dictum ullamcorper. Morbi fermentum dictum tempor. Sed dictum enim quis pharetra consequat. Sed tempus quis lorem sit amet facilisis. Suspendisse sollicitudin lacus ac ultricies viverra. Etiam efficitur imperdiet mi. Praesent non dapibus lectus, venenatis congue urna. Praesent egestas hendrerit semper." +
                       "Phasellus ut laoreet neque. In dictum luctus libero vitae aliquet. Nulla eu velit orci. Vivamus libero ante, condimentum eget felis vel, aliquam viverra enim. Nulla nibh odio, rhoncus sed ex a, efficitur ullamcorper dolor. Ut tempor semper est, in lobortis nisi bibendum non. Nullam interdum, elit nec vehicula lobortis, nulla erat consequat ipsum, ac dapibus nisi nibh ut lectus." +
                       "Nam mattis nulla tincidunt tempor tincidunt. Sed vitae luctus neque. Duis nunc turpis, condimentum et enim nec, malesuada vehicula leo. Nullam in ligula feugiat, viverra nibh et, ultricies ipsum. Proin rhoncus congue nisi, eu interdum est. Etiam ornare, leo ac imperdiet malesuada, nisl quam dapibus justo, eu convallis neque metus nec urna. Fusce hendrerit dolor ut risus consequat semper. Sed sed enim lacus. Sed eget lorem lobortis, mollis urna vel, feugiat magna. Ut cursus erat massa, interdum cursus neque vulputate at. Aliquam luctus dictum augue a convallis." +
                       "Quisque molestie faucibus nisi a efficitur. Nam accumsan porta tellus, vel dictum nunc vulputate nec. In consectetur pulvinar sem, sit amet pharetra justo varius in. Nulla placerat pretium ultrices. Curabitur eu ex ultricies, commodo enim dapibus, bibendum purus. Aliquam suscipit eget quam eu imperdiet. Proin iaculis sollicitudin ante, quis porttitor nisl euismod ut. Proin lacinia ipsum quis elementum lobortis. Proin luctus mi eu congue tempus. Morbi commodo porta lacus a pharetra. Pellentesque ullamcorper sem arcu, a bibendum mi feugiat maximus. Suspendisse potenti." +
                       "Proin commodo felis tempor, maximus sem ac, volutpat ante. Nullam sed sollicitudin eros. Nulla nec lacus nibh. Quisque bibendum neque eu neque condimentum, eleifend mollis arcu sagittis. Etiam volutpat nunc orci, sit amet laoreet dolor commodo quis. Duis a tellus ultrices, consequat dui et, gravida dolor. Proin eu mi ac lacus malesuada ullamcorper. Duis pulvinar tortor quis bibendum blandit. Sed auctor porttitor risus sodales molestie. Cras quam nibh, tincidunt quis augue a, dapibus efficitur diam. Aliquam id tortor eget ante aliquet commodo non a nulla. Quisque mattis augue id urna pharetra rutrum. Nulla ipsum ante, molestie eu imperdiet in, semper et massa. Aenean velit ipsum, feugiat quis est nec, gravida fermentum lorem.")
            .Split(' ');
        
        List<Todo> ts = new List<Todo>();
        Random r = new Random();
        for (int i = 0; i < 15; i++)
        {
            int start = r.Next(0, words.Length - 11);
            int end = r.Next(start+5, start + 10);
            string title = "";
            for (int j = start; j < end; j++)
            {
                title += words[j] + " ";
            }
            Todo todo = new Todo
            {
                UserId = r.Next(0, 10)+1,
                IsCompleted = r.Next(0,2) == 0,
                Title = title.Trim()
            };
            todoContext.Add(todo);
        }

        todoContext.SaveChanges();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}
}