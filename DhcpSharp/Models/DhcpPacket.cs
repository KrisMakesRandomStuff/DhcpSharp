using System.Net;
using System.Text;

namespace DhcpSharp.Models;

public class DhcpPacket {
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
Options: {string.Join(", ", this.Options.Select(x => x.GetType().Name))}
";
    }
}
