using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace ProtocolServer {
class Program {
    static void Main(string[] args) {
        Console.WriteLine("Starting server..");
        
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        TcpListener listener = new TcpListener(ip, 5000);
        listener.Start();

        Console.WriteLine("Server started..");

        while (true) {
            TcpClient client = listener.AcceptTcpClient();
            
            Console.WriteLine("Client connected");
            new Thread(() => HandleClientRequest(client)).Start();
        }
    }

    private static void HandleClientRequest(TcpClient client) {
        NetworkStream stream = client.GetStream();
            
        // read
        TransferObj to = GetObject(stream);
        switch (to.Action)
        {
            case "LOGIN": {
                HandleLogin(to, stream);
                break;
            }    
            case "ADD_PERSON" : HandleAddPerson(to, stream);
                break;
        }
            
        // respond
        
            
        // close connection
        client.Close();
    }

    private static void HandleAddPerson(TransferObj to, NetworkStream stream) {
        Person pers = JsonSerializer.Deserialize<Person>(to.Arg);
        
        // make call to data layer to add person
        
        string result = "OK"; // or ERROR
        byte[] dataToClient = Encoding.ASCII.GetBytes($"Returning {to}");
        stream.Write(dataToClient, 0, dataToClient.Length);
    }

    private static void HandleLogin(TransferObj to, NetworkStream stream) {
        User user = JsonSerializer.Deserialize<User>(to.Arg);

        // handle log in here
        
        string result = "OK";
        byte[] dataToClient = Encoding.ASCII.GetBytes($"Returning {to}");
        stream.Write(dataToClient, 0, dataToClient.Length);
    }

    private static TransferObj GetObject(NetworkStream stream) {
        byte[] dataFromClient = new byte[1024];
        int bytesRead = stream.Read(dataFromClient, 0, dataFromClient.Length);
        string s = Encoding.ASCII.GetString(dataFromClient, 0, bytesRead);
        TransferObj transferObj = JsonSerializer.Deserialize<TransferObj>(s);
        return transferObj;
    }
}
}