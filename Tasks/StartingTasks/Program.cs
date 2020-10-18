using System;
using System.Threading.Tasks;

namespace StartingTasks {
class Program {
    static void Main(string[] args) {
        new Program().ExampleMethod();
    }

    private void ExampleMethod() {
        
        Parallel.Invoke(PrintX, PrintO);
        
        Parallel.Invoke(() => PrintX(), () => PrintO());
        
    }

    private async void ExampleMethod2() {
        Task task = Task.Run(PrintO);
    }

    private void PrintO() {
        while (true) {
            Console.WriteLine("O");
        }
    }

    private void PrintX() {
        while (true) {
            Console.WriteLine("X");
        }
    }
}
}