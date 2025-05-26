using System.Net;
using System.Net.Sockets;

namespace DhcpSharp
{
    public class DhcpServer
    {
        public string Ip { get; set; }
        public int Port { get; set; }

        public DhcpServer(string ip, int port) {
            this.Ip = ip;
            this.Port = port;
        }

        public void Start() {
            IPEndPoint local = new(IPAddress.Parse(this.Ip), this.Port);
            UdpClient udp = new(local);
            

        }
    }
}
