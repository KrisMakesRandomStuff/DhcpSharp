using System.Net;

namespace DhcpSharp.Models.DhcpOptions;

public sealed class RequestedIpAddressOption(byte[] data) : DhcpOption {
    public override byte Code => 50;
    public IPAddress RequestedIpAddress { get; } = new IPAddress(data);
}
