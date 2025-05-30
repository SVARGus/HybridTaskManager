using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace HybridTaskManager.LogSystem
{
    public static class AppLogger
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void Info(string message) => logger.Info(message);
        public static void Warn(string message) => logger.Warn(message);
        public static void Error(string message) => logger.Error(message);
        public static void Error(Exception ex) => logger.Error(ex, ex.Message);
    }
}
