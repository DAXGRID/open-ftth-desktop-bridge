using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using OpenFTTH.DesktopBridge.Internal;

namespace OpenFTTH.DesktopBridge
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            using (var host = HostConfig.Configure())
            {
                await host.StartAsync();
                await host.WaitForShutdownAsync();
            }

            // WebSocket server port
            // int port = 5000;
            // if (args.Length > 0)
            //     port = int.Parse(args[0]);

            // Console.WriteLine($"WebSocket server port: {port}");
            // Console.WriteLine($"WebSocket server website: http://localhost:{port}/chat/index.html");

            // Console.WriteLine();

            // // Create a new WebSocket server
            // var server = new Server(IPAddress.Any, port);

            // // Start the server
            // Console.Write("Server starting...");
            // server.Start();
            // Console.WriteLine("Done!");

            // Console.WriteLine("Press Enter to stop the server or '!' to restart the server...");

            // // Stop the server
            // Console.Write("Server stopping...");
            // server.Stop();
            // Console.WriteLine("Done!");
        }
    }
}
