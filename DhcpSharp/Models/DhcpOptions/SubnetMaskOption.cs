using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DhcpSharp.Models.DhcpOptions
{
    public sealed class SubnetMaskOption(byte[] data) : DhcpOption {
        public override byte Code => 1;

        public IPAddress SubnetMask { get; } = new IPAddress(data);
    }
}
