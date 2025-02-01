using log4net;

namespace SISONKE_Invoicing_RESTAPI.DesignPatterns
{
    public sealed class LogManagerSingleton
    {
        private static readonly Lazy<LogManagerSingleton> _instance = new Lazy<LogManagerSingleton>(() => new LogManagerSingleton());

        private LogManagerSingleton() { }

        public static LogManagerSingleton Instance => _instance.Value;

        public ILog GetLogger(string loggerName)
        {
            return LogManager.GetLogger(loggerName);
        }
    }
}
