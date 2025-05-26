namespace DhcpSharp.Models
{
    public class DhcpPacket
    {
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
        public string Sname { get; set; } = string.Empty;

        // Boot file
        public string File { get; set; } = string.Empty;

        // Other options
        public List<byte> Options { get; set; } = new();
    }
}
