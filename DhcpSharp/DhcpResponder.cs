using DhcpSharp.Extensions;
using DhcpSharp.Models;
using DhcpSharp.Models.DhcpOptions;
using System.Net;

namespace DhcpSharp;

public class DhcpResponder {
    public static DhcpPacket Respond(byte[] data) {
        DhcpPacket packet = DhcpPacketParser.Parse(data);

        packet.OpCode = 2;
        packet.YiAddr = IPAddress.Parse("192.168.1.25").ToUInt32();

        packet.Options = [
            new DhcpMessageTypeOption([0x2]),
            new DhcpServerIdentifierOption(IPAddress.Parse("192.168.1.22").GetAddressBytes()),
            new IpAddressLeaseTimeOption([0x0, 0x1, 0x4d, 0xb2]),
            new SubnetMaskOption([0xff, 0xff, 0xff, 0x00]),
            new RouterOption(IPAddress.Parse("192.168.1.1").GetAddressBytes()),
            new DomainNameServerOption(IPAddress.Parse("8.8.8.8").GetAddressBytes())
        ];

        return packet;
    }
}
