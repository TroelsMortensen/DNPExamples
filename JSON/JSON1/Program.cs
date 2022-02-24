using System;
using System.Text.Json;
using System.Threading;

namespace JSON1 {
    class Program {
        static void Main(string[] args)
        {
            int s = 1;
            var serialize = JsonSerializer.Serialize(s);
            Console.WriteLine(serialize);
            // Test1();
        }

        private static void Test1()
        {
            MyJsonObject mjo = new MyJsonObject
            {
                B = true,
                Number = 42,
                Text = "thismytext",
                ManyStrings = new[] {"this", "my", "text"},
                ThisIsIgnored = "youll never see this"
            };

            string jsonFormatted = JsonSerializer.Serialize(mjo, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            Console.WriteLine(jsonFormatted);

            MyJsonObject myJsonObject = JsonSerializer.Deserialize<MyJsonObject>(jsonFormatted);
        }
    }
}