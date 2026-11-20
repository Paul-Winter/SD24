using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;

namespace WebStudy
{
    internal class Program
    {
        private const int Port = 5000;
        private const string DiscoveryRequest = "WHO_IS_THERE";
        static async Task Main(string[] args)
        {
            Console.Write("Mode selection, s - server, c - client: ");
            string? mode = Console.ReadLine();

            if (mode == "s")
                await RunServerAsync();
            else if (mode == "c")
                await RunClientAsync();
            else
                Console.WriteLine("Unknown");
        }
        private static async Task RunServerAsync() 
        {
            /* var listener = new TcpListener(IPAddress.Any, Port);
             listener.Start();
             PrintLocalAdresses();
             Console.WriteLine("Waiting for connections");

             using TcpClient client = await listener.AcceptTcpClientAsync();
             Console.WriteLine($"Connected: {client.Client.RemoteEndPoint}");

             NetworkStream stream = client.GetStream();

             var buffer = new byte[1024];
             int read = await stream.ReadAsync(buffer);
             Console.WriteLine($"Accepted: {Encoding.UTF8.GetString(buffer,0,read)}");

             await stream.WriteAsync(Encoding.UTF8.GetBytes("Hello from server!"));
             listener.Stop();*/
            while (true) 
            {
                await DiscoverClientsAsync();

                Console.WriteLine("Enter to look again");
                Console.ReadLine();
            }


        }
        private static async Task RunClientAsync() 
        {
            string name = "It_top";
            /* Console.WriteLine("IP-Adress");
             string? input = Console.ReadLine();
             if (!IPAddress.TryParse(input, out IPAddress? serverAdress)) 
             {
                 Console.WriteLine("Incorrect format");
                 return;
             }
             using var client = new TcpClient();
             await client.ConnectAsync(serverAdress, Port);

             NetworkStream stream = client.GetStream();

             await stream.WriteAsync(Encoding.UTF8.GetBytes("Hello, server!"));

             var buffer = new byte[1024];
             int read = await stream.ReadAsync(buffer);
             Console.WriteLine($"Answer: {Encoding.UTF8.GetString(buffer, 0, read)}");*/
            using var udp = new UdpClient(Port);
            Console.WriteLine($"{name} has started");

            while (true) 
            {
                UdpReceiveResult result = await udp.ReceiveAsync();

                string text = Encoding.UTF8.GetString(result.Buffer);

                if (text != DiscoveryRequest)
                    continue;
                Console.WriteLine($"Request from{result.RemoteEndPoint.Address}, responding");

                byte[] reply = Encoding.UTF8.GetBytes(name);
                await udp.SendAsync(reply, reply.Length, result.RemoteEndPoint);
            }
        }
        private static void PrintLocalAdresses() 
        {
            Console.WriteLine("Adresses: ");
            foreach (IPAddress address in Dns.GetHostAddresses(Dns.GetHostName())) 
            {
                if (address.AddressFamily == AddressFamily.InterNetwork) 
                {
                    Console.WriteLine($"{address}: {Port}");
                }
            }
        }

        private static async Task DiscoverClientsAsync() 
        {
            using var udp = new UdpClient();
            udp.EnableBroadcast = true;

            byte[] request = Encoding.UTF8.GetBytes(DiscoveryRequest);
            var broadcast = new IPEndPoint(IPAddress.Broadcast, Port);
            await udp.SendAsync(request, request.Length, broadcast);
            Console.WriteLine("Looking for clients...");

            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(2));

            int count = 0;

            try
            {
                while (true) 
                {
                    UdpReceiveResult result = await udp.ReceiveAsync(timeout.Token);
                    string name = Encoding.UTF8.GetString(result.Buffer);
                    Console.WriteLine($"{name} = {result.RemoteEndPoint.Address}");
                    count++;
                }
            }
            catch (OperationCanceledException)
            {

                Console.WriteLine("TIMEOUT!");
            }
            Console.WriteLine($"Found {count} clients!");
        }

    }
}
