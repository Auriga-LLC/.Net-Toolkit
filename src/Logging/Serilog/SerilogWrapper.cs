using Microsoft.Extensions.Logging;
using Serilog.Core;

using MicrosoftLogger = Microsoft.Extensions.Logging.ILogger;
using SerilogLogger = Serilog.ILogger;
using SerilogProvider = Serilog.Log;

namespace Auriga.Toolkit.Logging.Serilog;

/// <summary>
/// Serilog logger service wrapper.
/// </summary>
/// <param name="className">Logged service name.</param>
internal sealed class SerilogWrapper(string className) : MicrosoftLogger
{
	private readonly SerilogLogger _logger = SerilogProvider.Logger.ForContext(Constants.SourceContextPropertyName, className);

	/// <inheritdoc/>
	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
		=> _logger.Write(LogLevelMapper.Map(logLevel), formatter(state, exception));

	/// <inheritdoc/>
	public bool IsEnabled(LogLevel logLevel)
		=> _logger.IsEnabled(LogLevelMapper.Map(logLevel));

	/// <inheritdoc/>
	public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;
}
