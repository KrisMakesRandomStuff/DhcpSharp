namespace DhcpSharp.Models.DhcpOptions
{
    public sealed class RebindingTimeValueOption(byte[] data) : DhcpOption
    {
        public override byte Code => 59;
        public uint RebindingTime { get; } = BitConverter.ToUInt32(data.Reverse().ToArray(), 0);
    }
}
