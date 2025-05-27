namespace DhcpSharp.Models.DhcpOptions;

public sealed class NetBiosNodeTypeOption(byte[] data) : DhcpOption {
    public override byte Code => 46;
    public byte NodeType { get; } = data[0];
}
