
using System.IO;
using System.Runtime.CompilerServices;

namespace Marc.Mono.Service.Logging;

public class CsvLogger : ILogger
{
    private readonly string _logFilePath;
    private readonly string _logFileName; 

    private static readonly object _lock = new object();
    public CsvLogger(IConfiguration configuration)
    {
        _logFilePath = configuration["Logging:LogFilePath"];
        _logFileName = configuration["Logging:LogFileName"];

        if (!Directory.Exists(_logFilePath))
        {
            Directory.CreateDirectory(_logFilePath);
        }
    }

    

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {

        var logEntry= $"{DateTime.Now},{logLevel},{formatter(state,exception)}{Environment.NewLine}";
        lock (_lock)
        {
            File.AppendAllText(Path.Combine(_logFilePath, _logFileName), logEntry);
        }
    }

    public IDisposable BeginScope<TState>(TState state)
        {
            // No need to implement scope handling, return null
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // For simplicity, assume all log levels are enabled
            return true;
        }

   
}