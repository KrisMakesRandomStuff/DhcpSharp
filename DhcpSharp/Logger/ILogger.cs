using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhcpSharp.Logger
{
    public interface ILogger
    {
        void LogInfo(string msg);
        void LogWarning(string msg);
        void LogError(string msg);
    }
}
