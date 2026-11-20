using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace Web_1711
{
    internal class Program
    {
        static int Port = 5000;
        static ConcurrentDictionary<Guid, TcpClient> Clients = new();
        static async Task Main(string[] args)
        {
           string? choice = Console.ReadLine();
            switch (choice) 
            {
                case "S":
                    await RunServerAsync();
                    break;
                case "C":
                    await RunClientAsync("192.168.88.68");
                    break;
            }
               
        
        }
        static async Task RunServerAsync() 
        {
           

            using var listner = new TcpListener(IPAddress.Any, Port);

            listner.Start();

            Console.WriteLine($"Server listens on {Port}");

            while (true) 
            {
                TcpClient client = await listner.AcceptTcpClientAsync();
                Guid id = Guid.NewGuid();
                Clients[id] = client;
                Console.WriteLine($"Client id: {id} connected!");
                _ = HandleClientAsync(client, id);
            }
        }
        static async Task HandleClientAsync(TcpClient client, Guid id) 
        {

            try
            {
                using (client)
                {
                    using NetworkStream stream = client.GetStream();

                    using var reader = new StreamReader(stream, leaveOpen: true);
                    using var writer = new StreamWriter(stream, leaveOpen: true)
                    {
                        AutoFlush = true
                    };

                    while (true)
                    {
                        string? message = await reader.ReadLineAsync();

                        if (message == null)
                        {
                            break;
                        }
                        Console.WriteLine($"{id} - {message}");

                        await writer.WriteAsync($"Echo: {message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
            finally 
            {
                Clients.TryRemove(id, out _);
                Console.WriteLine($"{id} has been processed!");
            }

        }
        static bool DisconnectClient(Guid id) 
        {
            if (!Clients.TryRemove(id, out TcpClient? client)) 
            {
                return false;
            }
            client.Dispose();
            return true;
        }
        static async Task RunClientAsync(string address) 
        {
            using var client = new TcpClient();

            await client.ConnectAsync(address, Port);

            using NetworkStream stream = client.GetStream();

            using var reader = new StreamReader(stream, leaveOpen: true);
            using var writer = new StreamWriter(stream, leaveOpen: true)
            {
                AutoFlush = true
            };
            Console.WriteLine("Connected to server");
            Console.WriteLine("Enter the message to the server:");

            while (true) 
            {
                Console.WriteLine(">");
                string? message = Console.ReadLine();

                if (message == null || message == "/exit") 
                {
                    break;
                }

                await writer.WriteLineAsync(message);

                string? response = await reader.ReadLineAsync();

                if(response == null) 
                {
                    Console.WriteLine("The connection has been terminated by the server");
                    break;
                }
                Console.WriteLine(response);
            }
        }
    }
}
