namespace DhcpSharp.Extensions;

public static class BinaryReaderExtensions {
    public static ushort ReadUInt16BE(this BinaryReader reader) {
        byte[] bytes = reader.ReadBytes(2);
        Array.Reverse(bytes);
        return BitConverter.ToUInt16(bytes, 0);
    }

    public static short ReadInt16BE(this BinaryReader reader) {
        byte[] bytes = reader.ReadBytes(2);
        Array.Reverse(bytes);
        return BitConverter.ToInt16(bytes, 0);
    }

    public static uint ReadUInt32BE(this BinaryReader reader) {
        byte[] bytes = reader.ReadBytes(4);
        Array.Reverse(bytes);
        return BitConverter.ToUInt32(bytes, 0);
    }

    public static int ReadInt32BE(this BinaryReader reader) {
        byte[] bytes = reader.ReadBytes(4);
        Array.Reverse(bytes);
        return BitConverter.ToInt32(bytes, 0);
    }

    public static float ReadSingleBE(this BinaryReader reader) {
        byte[] bytes = reader.ReadBytes(4);
        Array.Reverse(bytes);
        return BitConverter.ToSingle(bytes, 0);
    }

    public static double ReadDoubleBE(this BinaryReader reader) {
        byte[] bytes = reader.ReadBytes(8);
        Array.Reverse(bytes);
        return BitConverter.ToDouble(bytes, 0);
    }
}
