using DhcpSharp.Models;
using System.Net;
using System.Net.Sockets;

namespace DhcpSharp;

public class DhcpServer {
    public int Port { get; set; }

    public DhcpServer(int port) => this.Port = port;

    public void Start() {
        IPEndPoint local = new(IPAddress.Any, this.Port);
        UdpClient udp = new(local);

        Console.WriteLine("Server started on port " + this.Port);

        while (true) {
            IPEndPoint remote = new(IPAddress.Any, 0);
            byte[] data = udp.Receive(ref remote);

            DhcpPacket packet = DhcpPacket.Parse(data);
            Console.WriteLine(packet.ToString());
        }
    }
}
