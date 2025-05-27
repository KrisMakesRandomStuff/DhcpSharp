using System.Net;

namespace DhcpSharp.Models.DhcpOptions;

public sealed class DomainNameServerOption(byte[] data) : DhcpOption {
    public override byte Code => 6;
    public IReadOnlyList<IPAddress> DnsServers { get; } =
        Enumerable.Range(0, data.Length / 4)
                  .Select(i => new IPAddress(data.Skip(i * 4).Take(4).ToArray()))
                  .ToList();
}
