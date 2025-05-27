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

            DhcpPacket response = DhcpResponder.Respond(data);

            UdpClient to_client = new(new IPEndPoint(new IPAddress(response.CiAddr), 68));
        }
    }
}
