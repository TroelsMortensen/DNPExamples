using System;
using System.Text.Json;
using System.Threading;

namespace JSON1 {
    class Program {
        static void Main(string[] args) {
            MyJsonObject mjo = new MyJsonObject {
                b = true,
                number = 42,
                text = "thismytext",
                manyStrings = new[] {"this", "my", "text"},
                thisIsIgnored = "youll never see this"
            };

            string jsonFormatted = JsonSerializer.Serialize(mjo, new JsonSerializerOptions {
                WriteIndented = true
            });

            Console.WriteLine(jsonFormatted);

            MyJsonObject myJsonObject = JsonSerializer.Deserialize<MyJsonObject>(jsonFormatted);
        }
    }
}