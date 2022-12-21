using Microsoft.Extensions.Hosting;
using OpenFTTH.DesktopBridge.Internal;
using System.Threading.Tasks;

namespace OpenFTTH.DesktopBridge;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        using var host = HostConfig.Configure();
        await host.StartAsync().ConfigureAwait(false);
        await host.WaitForShutdownAsync().ConfigureAwait(false);
    }
}
