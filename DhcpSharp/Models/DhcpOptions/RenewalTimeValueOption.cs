namespace DhcpSharp.Models.DhcpOptions
{
    public sealed class RenewalTimeValueOption(byte[] data) : DhcpOption
    {
        public override byte Code => 58;
        public uint RenewalTime { get; } = BitConverter.ToUInt32(data.Reverse().ToArray(), 0);
    }
}
