using System.Net;

namespace DhcpSharp.Models.DhcpOptions
{
    public sealed class BroadcastAddressOption(byte[] data) : DhcpOption
    {
        public override byte Code => 28;
        public IPAddress BroadcastAddress { get; } = new IPAddress(data);
    }
}
