using DhcpSharp.Models;
using System.Net;
using System.Reflection;
using System.Text;

namespace DhcpSharp.Extensions;

public static class DhcpOptionExtensions {
    public static byte[] ToBytes(this DhcpOption option) {
        using MemoryStream ms = new();
        using BinaryWriter writer = new(ms);

        foreach (PropertyInfo prop in option.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
            object? value = prop.GetValue(option);

            switch (value) {
                case byte b:
                    writer.Write(b);
                    break;
                case short s:
                    writer.Write(BitConverter.GetBytes(s));
                    break;
                case ushort us:
                    writer.Write(BitConverter.GetBytes(us));
                    break;
                case int i:
                    writer.Write(BitConverter.GetBytes(i));
                    break;
                case uint ui:
                    writer.Write(BitConverter.GetBytes(ui));
                    break;
                case long l:
                    writer.Write(BitConverter.GetBytes(l));
                    break;
                case float f:
                    writer.Write(BitConverter.GetBytes(f));
                    break;
                case double d:
                    writer.Write(BitConverter.GetBytes(d));
                    break;
                case bool bo:
                    writer.Write(bo ? (byte)1 : (byte)0);
                    break;
                case string str:
                    byte[] strBytes = Encoding.ASCII.GetBytes(str);
                    writer.Write((byte)strBytes.Length);
                    writer.Write(strBytes);
                    break;
                case byte[] barr:
                    writer.Write((byte)barr.Length);
                    writer.Write(barr);
                    break;
                case Enum e:
                    object underlying = Convert.ChangeType(e, Enum.GetUnderlyingType(e.GetType()));
                    writer.Write(BitConverter.GetBytes((int)underlying!));
                    break;
                case IPAddress ip:
                    byte[] bytes = ip.GetAddressBytes();
                    writer.Write((byte)bytes.Length);
                    writer.Write(bytes);
                    break;
                case IReadOnlyList<byte> byteList:
                    writer.Write((byte)byteList.Count);
                    writer.Write(byteList.ToArray());
                    break;
                case IReadOnlyList<IPAddress> ipList:
                    writer.Write((byte)ipList.Count);
                    foreach (IPAddress ipAddr in ipList) {
                        byte[] ipBytes = ipAddr.GetAddressBytes();
                        writer.Write((byte)ipBytes.Length);
                        writer.Write(ipBytes);
                    }
                    break;
                default:
                    throw new NotSupportedException($"Property {prop.Name} of type {prop.PropertyType.Name} is not supported");
            }
        }

        return ms.ToArray();
    }
}
