using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Logging
{
    public static class LogManager
    {
        static LogManager()
        {
            Current = NLog.LogManager.GetLogger("*");
        }
        public static Logger Current { get; set; }
    }
}
