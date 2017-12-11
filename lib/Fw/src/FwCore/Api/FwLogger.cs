using Microsoft.Extensions.Logging;

namespace FwCore.Api
{
    public class FwLogger
    {
        private static ILoggerFactory _Factory = null;
        private static ILogger logger = null;
        //------------------------------------------------------------------------------------
        public static ILoggerFactory LoggerFactory
        {
            get { return _Factory; }
            set { _Factory = value; }
        }
        //------------------------------------------------------------------------------------
        public static void log(string str)
        {
            if (logger == null)
            {
                logger = LoggerFactory.CreateLogger("application log");
            }
            logger.LogInformation(str);
        }
        //------------------------------------------------------------------------------------
    }
}
