using MediatR;
using System.Threading;
using System.Threading.Tasks;
using OpenFTTH.DesktopBridge.Bridge;

namespace OpenFTTH.DesktopBridge.Retrieve
{
    public class RetrieveSelectedHandler : IRequestHandler<RetrieveSelected>
    {
        private readonly IBridgeServer _bridgeServer;

        public async Task<Unit> Handle(RetrieveSelected request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new Unit());
        }
    }
}
