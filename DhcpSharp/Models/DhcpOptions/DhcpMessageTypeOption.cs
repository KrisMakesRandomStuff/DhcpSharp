namespace DhcpSharp.Models.DhcpOptions;

public sealed class DhcpMessageTypeOption(byte[] data) : DhcpOption {
    public override byte Code => 53;
    public byte MessageType { get; } = data[0];
}
