using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPI.WiringPiWrapper.Helpers.Loggers
{
    public class NLogger:RPI.WiringPiWrapper.Interfaces.ILogger
    {
        Logger logger;

        public NLogger()
        {
            Prepare();

            logger = NLog.LogManager.GetCurrentClassLogger();

            logger.Info("NLog logger is turned on");
        }
        
        public void WriteMessage(string message)
        {
            logger.Info(message);
        }

        public void WriteMessage(object objectToWrite)
        {
            logger.Info(objectToWrite.ToString());
        }

        private void Prepare()
        {
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "log.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole);

            NLog.LogManager.Configuration = config;

        }
    }
}
