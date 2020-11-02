using MediatR;

namespace OpenFTTH.DesktopBridge.Retrieve
{
    public class RetrieveSelected : IRequest<Unit>
    {
        public string Username { get; }

        public RetrieveSelected(string username)
        {
            Username = username;
        }
    }
}
