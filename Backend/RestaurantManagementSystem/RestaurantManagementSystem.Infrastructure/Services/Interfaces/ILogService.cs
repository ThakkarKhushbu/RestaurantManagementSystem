namespace RestaurantManagementSystem.Infrastructure.Services.Interfaces
{
    public interface ILogService
    {
        void LogTrace(string message);

        void LogError(Exception ex);

        void LogError(dynamic ex);

        void LogWarning(string message);

        void LogInformation(string message);

        void LogFatal(string message);

        void LogDebug(string message);
    }
}
