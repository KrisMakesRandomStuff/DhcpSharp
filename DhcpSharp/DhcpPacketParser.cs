using DhcpSharp.Extensions;
using DhcpSharp.Models;
using DhcpSharp.Models.DhcpOptions;

namespace DhcpSharp;

public class DhcpPacketParser {
    private const int MAIN_PACKET_LENGTH = 236;

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
}
