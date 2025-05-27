using System.Net;

namespace DhcpSharp.Models.DhcpOptions;

public sealed class DhcpServerIdentifierOption(byte[] raw) : DhcpOption {
    public override byte Code => 54;

    public IPAddress ServerIp { get; } = new IPAddress(raw);
}
