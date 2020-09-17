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
        }
    }
}
