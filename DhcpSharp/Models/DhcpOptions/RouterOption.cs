using System.Net;

namespace DhcpSharp.Models.DhcpOptions
{
    public sealed class RouterOption(byte[] data) : DhcpOption
    {
        public override byte Code => 3;
        public IReadOnlyList<IPAddress> Routers { get; } = 
            Enumerable.Range(0, data.Length / 4)
                      .Select(i => new IPAddress(data.Skip(i * 4).Take(4).ToArray()))
                      .ToList();
    }
}
