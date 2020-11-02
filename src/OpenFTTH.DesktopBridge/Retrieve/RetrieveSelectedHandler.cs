using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace OpenFTTH.DesktopBridge.Retrieve
{
    public class RetrieveSelectedHandler : IRequestHandler<RetrieveSelected>
    {
        public async Task<Unit> Handle(RetrieveSelected request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new Unit());
        }
    }
}
