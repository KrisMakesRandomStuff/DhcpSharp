namespace DhcpSharp.Models.DhcpOptions
{
    public sealed class TimeOffsetOption(byte[] data) : DhcpOption
    {
        public override byte Code => 2;
        public int TimeOffset { get; } = BitConverter.ToInt32(data.Reverse().ToArray(), 0);
    }
}
