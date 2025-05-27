namespace DhcpSharp;

internal class Program {
    static void Main(string[] args) {
        DhcpServer server = new(67);
        server.Start();
    }
}
