using Microsoft.Extensions.Logging;
using TutorCenter.Domain.Interfaces.Logger;

namespace TutorCenter.Infrastructure.Logging;

public class LoggerAdapter<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;
    public LoggerAdapter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<T>();
    }

    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }

    public void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    } 
    public void LogError(string message, params object[] args)
    {
        _logger.LogError(message, args);
    }
    public void LogTrace(string message, params object[] args)
    {
        _logger.LogTrace(message, args);
    } 
    public void LogDebug(string message, params object[] args)
    {
        _logger.LogDebug(message, args);
    }
}
