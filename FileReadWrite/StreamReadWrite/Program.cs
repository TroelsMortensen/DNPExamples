using System;
using System.IO;
using System.Text;

namespace ConsoleApp1 {
    class Program {
        static void Main(string[] args) {
            WriteFile();
            string result = ReadFile();
            Console.WriteLine(result);
        }

        private static void WriteFile() {
            string[] lines = {"hello", "world", "how", "are", "you?"};

            using (StreamWriter file = new StreamWriter("MyLines.txt")) {
                foreach (string line in lines) {
                    file.WriteLine(line);
                }
            }
        }

        private static string ReadFile() {
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader("MyLines.txt")) {
                string line;
                while ((line = sr.ReadLine()) != null) {
                    sb.AppendLine(line);
                }
            }

            return sb.ToString();
        }
    }
}