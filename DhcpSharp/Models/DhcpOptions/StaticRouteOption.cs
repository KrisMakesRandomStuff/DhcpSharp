using System.Net;

namespace DhcpSharp.Models.DhcpOptions
{
    public sealed class StaticRouteOption(byte[] data) : DhcpOption
    {
        public override byte Code => 33;
        public IReadOnlyList<(IPAddress Destination, IPAddress Gateway)> Routes { get; } =
            Enumerable.Range(0, data.Length / 8)
                      .Select(i => (
                          new IPAddress(data.Skip(i * 8).Take(4).ToArray()),
                          new IPAddress(data.Skip(i * 8 + 4).Take(4).ToArray())
                      ))
                      .ToList();
    }
}
