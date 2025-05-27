using System.Text;

namespace DhcpSharp.Models.DhcpOptions
{
    public sealed class HostNameOption(byte[] data) : DhcpOption
    {
        public override byte Code => 12;
        public string HostName { get; } = Encoding.ASCII.GetString(data);
    }
}
