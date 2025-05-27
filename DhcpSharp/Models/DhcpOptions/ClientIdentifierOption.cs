namespace DhcpSharp.Models.DhcpOptions;

public sealed class ClientIdentifierOption(byte[] data) : DhcpOption {
    public override byte Code => 61;
    public byte[] Identifier { get; } = data;
}
