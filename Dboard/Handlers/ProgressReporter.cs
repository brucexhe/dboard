using Docker.DotNet.Models;
using log4net;

namespace Dboard.Handlers
{
    public class ProgressReporter : IProgress<JSONMessage>
    {
        protected ILog log = LogManager.GetLogger("ProgressReporter");

        public void Report(JSONMessage value)
        {
            log.Info(value.ErrorMessage != null ? value.ErrorMessage : value.Status);
        }
    }
}
