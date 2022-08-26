using Discord;
using Microsoft.Extensions.Logging;

namespace RushRoyale.Discord;

public static class LoggingExtensions
{
    public static LogLevel ToLogLevel(this LogSeverity severity) => severity switch
    {
        LogSeverity.Critical => LogLevel.Critical,
        LogSeverity.Error => LogLevel.Error,
        LogSeverity.Warning => LogLevel.Warning,
        LogSeverity.Info => LogLevel.Information,
        LogSeverity.Verbose => LogLevel.Trace,
        LogSeverity.Debug => LogLevel.Debug,
        _ => throw new ArgumentOutOfRangeException(nameof(severity), severity, null)
    };
}