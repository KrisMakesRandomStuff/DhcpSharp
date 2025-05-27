using DhcpSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhcpSharp.Extensions
{
    public static class DhcpPacketExtensions
    {
        private const int RECOMENDED_PACKET_SIZE = 570;

        public static byte[] ToBytes(this DhcpPacket packet) {
            using MemoryStream ms = new();
            using BinaryWriter writer = new(ms);

            writer.Write(packet.OpCode);
            writer.Write(packet.HwType);
            writer.Write(packet.HwLen);
            writer.Write(packet.Hops);
            writer.Write(packet.Xid);
            writer.Write(packet.Seconds);
            writer.Write(packet.Flags);
            writer.Write(packet.CiAddr);
            writer.Write(packet.YiAddr);
            writer.Write(packet.SiAddr);
            writer.Write(packet.GiAddr);
            writer.Write(packet.ChAddr);
            writer.Write(packet.Sname);
            writer.Write(packet.File);
            writer.Write(packet.MagicCookie);

            foreach(DhcpOption option in packet.Options) {
                writer.Write(option.ToBytes());
            }

            writer.Write(0xff);
            
            if(ms.Length < RECOMENDED_PACKET_SIZE) {
                for (int i = 0; i < RECOMENDED_PACKET_SIZE - ms.Length; i++) {
                    writer.Write(0x00);
                }
            }

            return ms.ToArray();
        }
    }
}
