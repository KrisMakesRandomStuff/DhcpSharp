using System.Net;

namespace DhcpSharp.Models.DhcpOptions
{
    public sealed class ServerIdentifierOption(byte[] data) : DhcpOption
    {
        public override byte Code => 54;
        public IPAddress ServerIdentifier { get; } = new IPAddress(data);
    }
}
