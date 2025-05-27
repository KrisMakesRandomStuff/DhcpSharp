namespace DhcpSharp.Models.DhcpOptions;

public class UnknownOption : DhcpOption {
    public override byte Code { get; }
    public byte[] RawData { get; }

    public UnknownOption(byte code, byte[] data) {
        this.Code = code;
        this.RawData = data;
    }
}
