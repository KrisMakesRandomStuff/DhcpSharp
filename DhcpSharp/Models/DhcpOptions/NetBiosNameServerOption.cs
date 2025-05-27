using System.Net;

namespace DhcpSharp.Models.DhcpOptions
{
    public sealed class NetBiosNameServerOption(byte[] data) : DhcpOption
    {
        public override byte Code => 44;
        public IReadOnlyList<IPAddress> NameServers { get; } =
            Enumerable.Range(0, data.Length / 4)
                      .Select(i => new IPAddress(data.Skip(i * 4).Take(4).ToArray()))
                      .ToList();
    }
}
