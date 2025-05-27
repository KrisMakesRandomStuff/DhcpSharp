namespace DhcpSharp.Models.DhcpOptions;

public sealed class ParameterRequestListOption(byte[] data) : DhcpOption {
    public override byte Code => 55;
    public IReadOnlyList<byte> RequestedParameters { get; } = data.ToList();
}
