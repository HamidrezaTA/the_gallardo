namespace Application.Utilities.Logger
{
    public interface IApplicationLogger<T> where T : class
    {
        void LogCritical(string message, params object[]? args);
        void LogCritical(long requestId, string message, params object[]? args);
        void LogError(string message, params object[]? args);
        void LogError(long requestId, string message, params object[]? args);
        void LogWarning(string message, params object[]? args);
        void LogWarning(long requestId, string message, params object[]? args);
        void LogInformation(string message, params object[]? args);
        void LogInformation(long requestId, string message, params object[]? args);
        void LogDebug(string message, params object[]? args);
        void LogDebug(long requestId, string message, params object[]? args);
    }
}