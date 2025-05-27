using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhcpSharp.Models
{
    public abstract class DhcpOption
    {
        public abstract byte Code { get; }
    }
}
