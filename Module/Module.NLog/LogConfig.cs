using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Targets;

namespace Module.NLog
{
    public class LogConfig
    {
        public static void ConfigureFile()
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);
            fileTarget.FileName = "${basedir}/Log/${shortdate}-${logger}.txt";
            fileTarget.Layout = @"${date} ${logger} ${level} ${message}";
            var rule = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule);
            LogManager.Configuration = config;
        }
    }

}
