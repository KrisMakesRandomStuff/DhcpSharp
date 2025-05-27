using System.Text;

namespace DhcpSharp.Models.DhcpOptions
{
    public sealed class BootFileNameOption(byte[] data) : DhcpOption
    {
        public override byte Code => 67;
        public string BootFileName { get; } = Encoding.ASCII.GetString(data);
    }
}
