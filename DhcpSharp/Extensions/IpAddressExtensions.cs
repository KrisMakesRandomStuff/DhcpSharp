using System.Net;

namespace DhcpSharp.Extensions;

public static class IpAddressExtensions {
    public static uint ToUInt32(this IPAddress ip) => BitConverter.ToUInt32(ip.GetAddressBytes());

    public static int ToInt32(this IPAddress ip) => BitConverter.ToInt32(ip.GetAddressBytes());
}
