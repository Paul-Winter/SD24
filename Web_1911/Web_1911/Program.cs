using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Web_1911
{
    internal class Program
    {
        static int Port = 5000;
        static readonly ConcurrentDictionary<Guid, TcpClient> Clients
            = new();
        private const int MaxMessageSize = 1024 * 1024;
        static async Task Main(string[] args)
        {
            await RunServerAsync();
        }
        static async Task RunClientAsync(string address) 
        {
            using var client = new TcpClient();

            await client.ConnectAsync(address, Port);

            using NetworkStream stream = client.GetStream();
            Console.WriteLine("Connected!");
            Console.WriteLine("/send to send the message, /exit to quit");
            Console.Write("Your message: ");
            var messageBuilder = new StringBuilder();
            bool hasLines = false;

            while (true) 
            {
                string? line = Console.ReadLine();

                if(line == null || line == "/exit") 
                {
                    break;
                }
                if (line == "/send")
                {
                    string message = messageBuilder.ToString();
                    await SendMessageAsync(stream, message);

                    string? response = await ReceiveMessageAsync(stream);

                    if (response == null)
                    {
                        Console.WriteLine("Server terminated connection");
                        break;
                    }
                    Console.WriteLine("Server response:");
                    Console.WriteLine(response);
                    Console.WriteLine("Enter the next message");
                    messageBuilder.Clear();
                    hasLines = false;
                }
                else 
                {
                    if (hasLines) 
                    {
                        messageBuilder.Append('\n');
                    }

                    messageBuilder.Append(line);
                    hasLines = true;
                }
            }
        }
        static async Task RunServerAsync() 
        {
            using var listener = new TcpListener(IPAddress.Any, Port);

            listener.Start();
            Console.WriteLine($"Server listens on port: {Port}");

            while (true) 
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                Guid id = Guid.NewGuid();
                Clients[id] = client;
                Console.WriteLine($"Client {id} has connected");

                _ = HandleClientAsync(id, client);
            }
        }
        static async Task HandleClientAsync(Guid id, TcpClient client) 
        {
            try
            {
                using (client)
                {
                    using NetworkStream stream = client.GetStream();

                    while (true)
                    {
                        string? message = await ReceiveMessageAsync(stream);
                        if (message == null)
                        {
                            break;
                        }
                        Console.WriteLine($"Message from {id}: {message}");
                        

                        await SendMessageAsync(stream, message);
                    }
                }
            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine($"Invalid message from {id}: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"IO error on {id} {ex.Message}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Socket error on {id} {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong on {id}!: {ex.Message}");
            }
            finally 
            {
                Clients.TryRemove(id, out _);
                Console.WriteLine($"Client {id} has been processed");
            }
        }
        static async Task SendMessageAsync(NetworkStream stream, string message) 
        {
            int length = Encoding.UTF8.GetByteCount(message);

            if (length > MaxMessageSize) 
            {
                throw new InvalidDataException($"Message is too large");
            }
            byte[] data = Encoding.UTF8.GetBytes(message);
            byte[] header = new byte[4];

            BinaryPrimitives.WriteInt32BigEndian(header, length);

            await stream.WriteAsync(header.AsMemory());
            await stream.WriteAsync(data.AsMemory());
        }
        private static async Task<string> ReceiveMessageAsync(NetworkStream stream) 
        {
            byte[] header = new byte[4];
            int bytesRead = await stream.ReadAsync(header.AsMemory());

            if (bytesRead == 0) 
            {
                return null;
            }

            await stream.ReadExactlyAsync(header.AsMemory(bytesRead));
            int length = BinaryPrimitives.ReadInt32BigEndian(header);

            if (length < 0 || length > MaxMessageSize) 
            {
                throw new InvalidDataException(
                    $"Invalid message length"
                    );
            }
            byte[] data = new byte[length];
            await stream.ReadExactlyAsync(data.AsMemory());

            return Encoding.UTF8.GetString(data);
        }
    }
}
