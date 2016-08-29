using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NLog
{
    public class LogConfig
    {
        public static void Init()
        {
            LogInit();
        }

        private static void LogInit()
        {
            //var config = new LoggingConfiguration();
            //var fileTarget = new FileTarget();
            //config.AddTarget("file", fileTarget);
            //fileTarget.FileName = "${basedir}/Log/${shortdate}-${logger}.txt";
            //fileTarget.Layout = @"${date} ${logger} ${message}";
            //var rule = new LoggingRule("*", LogLevel.Debug, fileTarget);
            //config.LoggingRules.Add(rule);
            //LogManager.Configuration = config;
        }
    }

}
