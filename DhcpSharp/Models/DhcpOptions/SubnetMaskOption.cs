using System.Net;

namespace DhcpSharp.Models.DhcpOptions;

public sealed class SubnetMaskOption(byte[] data) : DhcpOption {
    public override byte Code => 1;

    public IPAddress SubnetMask { get; } = new IPAddress(data);
}
