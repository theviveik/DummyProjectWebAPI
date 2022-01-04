using Microsoft.Extensions.Logging;

namespace BusinessAccessLayer.Service
{
    public class CustomLog : ICustomLog
    {
        private readonly ILogger<CustomLog> _logger;

        public CustomLog(ILogger<CustomLog> logger)
        {
            _logger = logger;
        }
        public void Error(string message)
        {
            _logger.LogError(message);
        }
    }
}
