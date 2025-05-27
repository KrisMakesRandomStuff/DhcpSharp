using System.Text;

namespace DhcpSharp.Models.DhcpOptions
{
    public sealed class TftpServerNameOption(byte[] data) : DhcpOption
    {
        public override byte Code => 66;
        public string TftpServerName { get; } = Encoding.ASCII.GetString(data);
    }
}
