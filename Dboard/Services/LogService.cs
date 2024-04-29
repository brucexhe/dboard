using log4net;

namespace Dboard.Services
{
    public class LogService<T>
    {
        private ILog log;

        public LogService()
        {
            log = LogManager.GetLogger(typeof(T));
        }



        public void Info(Object message)
        {
            log.Info(message);
        }
    }
}
