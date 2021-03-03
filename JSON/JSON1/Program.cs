using System;
using System.Text.Json;
using System.Threading;

namespace JSON1 {
    class Program {
        static void Main(string[] args) {
            MyJsonObject mjo = new MyJsonObject {
                B = true,
                Number = 42,
                Text = "thismytext",
                ManyStrings = new[] {"this", "my", "text"},
                ThisIsIgnored = "youll never see this"
            };

            string jsonFormatted = JsonSerializer.Serialize(mjo, new JsonSerializerOptions {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            Console.WriteLine(jsonFormatted);

            MyJsonObject myJsonObject = JsonSerializer.Deserialize<MyJsonObject>(jsonFormatted);
        }
    }
}