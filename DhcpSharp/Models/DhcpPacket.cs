using DhcpSharp.Extensions;
using DhcpSharp.Models.DhcpOptions;
using System.Net;
using System.Text;

namespace DhcpSharp.Models;

public class DhcpPacket {
    private const int MAIN_PACKET_LENGTH = 236;

    // Operation Code
    public byte OpCode { get; set; }

    // Hardware Type
    public byte HwType { get; set; }

    // Hardware address Length
    public byte HwLen { get; set; }

    // Hops
    public byte Hops { get; set; }

    // Transaction id
    public uint Xid { get; set; }

    // Seconds
    public ushort Seconds { get; set; }

    // DHCP flags
    public ushort Flags { get; set; }

    // Client Address
    public uint CiAddr { get; set; }

    // "Your" Address
    public uint YiAddr { get; set; }

    // Server Address
    public uint SiAddr { get; set; }

    // Gateway Address
    public uint GiAddr { get; set; }

    // Client hardware Address
    public byte[] ChAddr { get; set; } = new byte[16];

    // Server Name
    public byte[] Sname { get; set; } = new byte[64];

    // Boot file
    public byte[] File { get; set; } = new byte[128];

    public uint MagicCookie { get; set; }

    // Other options
    public List<DhcpOption> Options { get; set; } = [];

    public static DhcpPacket Parse(byte[] raw) {
        DhcpPacket packet = new();

        using BinaryReader reader = new(new MemoryStream(raw));

        packet.OpCode = reader.ReadByte();
        packet.HwType = reader.ReadByte();
        packet.HwLen = reader.ReadByte();
        packet.Hops = reader.ReadByte();
        packet.Xid = reader.ReadUInt32BE();
        packet.Seconds = reader.ReadUInt16();
        packet.Flags = reader.ReadUInt16();

        packet.CiAddr = reader.ReadUInt32();
        packet.YiAddr = reader.ReadUInt32();
        packet.SiAddr = reader.ReadUInt32();
        packet.GiAddr = reader.ReadUInt32();
        packet.ChAddr = reader.ReadBytes(16);

        packet.Sname = reader.ReadBytes(64);
        packet.File = reader.ReadBytes(128);
        packet.MagicCookie = reader.ReadUInt32();

        byte[] options_raw = reader.ReadBytes(raw.Length - MAIN_PACKET_LENGTH);
        packet.Options = ParseDhcpOptions(options_raw);

        return packet;
    }

    private static List<DhcpOption> ParseDhcpOptions(byte[] raw) {
        List<DhcpOption> list = [];
        int index = 0;

        while (index < raw.Length) {
            byte code = raw[index++];
            if (code == 0) continue;
            if (code == 255) break;

            byte len = raw[index++];
            byte[] data = [.. raw.Skip(index).Take(len)];
            index += len;

            list.Add(ParseOption(code, data));
        }
        return list;
    }

    private static DhcpOption ParseOption(byte code, byte[] raw) {
        return code switch {
            1 => new SubnetMaskOption(raw),
            2 => new TimeOffsetOption(raw),
            3 => new RouterOption(raw),
            6 => new DomainNameServerOption(raw),
            12 => new HostNameOption(raw),
            28 => new BroadcastAddressOption(raw),
            33 => new StaticRouteOption(raw),
            43 => new VendorSpecificInformationOption(raw),
            44 => new NetBiosNameServerOption(raw),
            46 => new NetBiosNodeTypeOption(raw),
            50 => new RequestedIpAddressOption(raw),
            51 => new IpAddressLeaseTimeOption(raw),
            53 => new DhcpMessageTypeOption(raw),
            54 => new ServerIdentifierOption(raw),
            55 => new ParameterRequestListOption(raw),
            58 => new RenewalTimeValueOption(raw),
            59 => new RebindingTimeValueOption(raw),
            61 => new ClientIdentifierOption(raw),
            66 => new TftpServerNameOption(raw),
            67 => new BootFileNameOption(raw),
            _ => new UnknownOption(code, raw)
        };
    }

    public override string ToString() {
        return $@"
OpCode: {this.OpCode}
Hardware Type: {this.HwType}
Hardware address length: {this.HwLen}
Hops: {this.Hops}
Transaction id: {this.Xid:X}
Seconds: {this.Seconds}
Flags: {this.Flags & (1 << 15)}

Client addr: {IPAddress.Parse(this.CiAddr.ToString())}
'Your' addr: {IPAddress.Parse(this.YiAddr.ToString())}
Server addr: {IPAddress.Parse(this.SiAddr.ToString())}
Gateway addr: {IPAddress.Parse(this.GiAddr.ToString())}
Client mac: {Convert.ToHexString(this.ChAddr)}

Server name: '{Encoding.ASCII.GetString(this.Sname)}'
";
    }
}
