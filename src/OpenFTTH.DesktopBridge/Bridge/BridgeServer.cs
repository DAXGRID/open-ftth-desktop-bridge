using System;
using System.Net;
using System.Net.Sockets;
using NetCoreServer;

namespace OpenFTTH.DesktopBridge.Bridge
{
    public class BridgeServer : WsServer
    {
        public BridgeServer(IPAddress address, int port) : base(address, port) { }

        protected override TcpSession CreateSession() { return new BridgeSession(this); }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat WebSocket server caught an error with code {error}");
        }
    }
}
