using System;
using System.Net;
using System.Net.Sockets;
using NetCoreServer;

namespace OpenFTTH.DesktopBridge
{
    public class Server : WsServer
    {
        public Server(IPAddress address, int port) : base(address, port) { }

        protected override TcpSession CreateSession() { return new Session(this); }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat WebSocket server caught an error with code {error}");
        }
    }
}
