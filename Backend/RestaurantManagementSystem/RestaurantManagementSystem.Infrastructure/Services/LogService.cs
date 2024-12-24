using Newtonsoft.Json;
using NLog;
using RestaurantManagementSystem.Infrastructure.Services.Interfaces;

namespace RestaurantManagementSystem.Infrastructure.Services
{
    public class LogService : ILogService
    {
        private readonly Logger _logger;

        public LogService()
        {
            LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration("NLog.config");
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void LogTrace(string message)
        {
            _logger.Trace(message);
        }

        public void LogError(Exception ex)
        {
            _logger.Error(JsonConvert.SerializeObject(ex));
        }

        public void LogError(dynamic ex)
        {
            _logger.Error(ex.errorCode, ex.message);
        }


        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }

        public void LogInformation(string message)
        {
            _logger.Info(message);
        }

        public void LogFatal(string message)
        {
            _logger.Fatal(message);
        }

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }
    }
}
