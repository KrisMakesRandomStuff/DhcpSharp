using System.Net;
using System.Text;

namespace DhcpSharp.Models
{
    public class DhcpPacket
    {
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

        public uint MagicCookie {  get; set; }

        // Other options
        public List<byte> Options { get; set; } = new();

        public static DhcpPacket Parse(byte[] raw) {
            DhcpPacket packet = new();

            using BinaryReader reader = new(new MemoryStream(raw));

            packet.OpCode = reader.ReadByte();
            packet.HwType = reader.ReadByte();
            packet.HwLen = reader.ReadByte();
            packet.Hops = reader.ReadByte();
            packet.Xid = reader.ReadUInt32();
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
            packet.Options = [.. reader.ReadBytes(raw.Length - MAIN_PACKET_LENGTH)];

            return packet;
        }

        public override string ToString() {
            return $@"
OpCode: {OpCode}
Hardware Type: {HwType}
Hardware address length: {HwLen}
Hops: {Hops}
Transaction id: {Xid}
Seconds: {Seconds}
Flags: {(int)(Flags & 1)}

Client addr: {IPAddress.Parse(this.CiAddr.ToString())}
'Your' addr: {IPAddress.Parse(this.YiAddr.ToString())}
Server addr: {IPAddress.Parse(this.SiAddr.ToString())}
Gateway addr: {IPAddress.Parse(this.GiAddr.ToString())}
Client mac: {ChAddr}

Server name: '{Encoding.ASCII.GetString(this.Sname)}'
";
        }
    }
}
