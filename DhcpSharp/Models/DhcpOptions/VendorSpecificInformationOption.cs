namespace DhcpSharp.Models.DhcpOptions;

public sealed class VendorSpecificInformationOption(byte[] data) : DhcpOption {
    public override byte Code => 43;
    public byte[] VendorData { get; } = data;
}
