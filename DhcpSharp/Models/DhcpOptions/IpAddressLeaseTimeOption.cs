namespace DhcpSharp.Models.DhcpOptions;

public sealed class IpAddressLeaseTimeOption(byte[] data) : DhcpOption {
    public override byte Code => 51;
    public uint LeaseTime { get; } = BitConverter.ToUInt32(data.Reverse().ToArray(), 0);
}
