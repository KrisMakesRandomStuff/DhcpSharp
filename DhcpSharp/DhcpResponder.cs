using DhcpSharp.Models;

namespace DhcpSharp;

public class DhcpResponder {
    public static byte[] Respond(byte[] data) {
        DhcpPacket packet = DhcpPacketParser.Parse(data);

        packet.OpCode = 2;
        packet.YiAddr =
    }
}
