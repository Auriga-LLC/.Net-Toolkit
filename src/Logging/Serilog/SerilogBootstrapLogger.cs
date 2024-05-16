using System.Collections.Concurrent;
using Auriga.Toolkit.Plugins;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using MicrosoftLogger = Microsoft.Extensions.Logging.ILogger;

namespace Auriga.Toolkit.Logging.Serilog;

/// <summary>
/// Serilog bootstrap logger setup.
/// </summary>
internal sealed class SerilogBootstrapLogger : IBootstrapLoggerPlugin
{
	private readonly ConcurrentDictionary<string, MicrosoftLogger> _loggers = new(StringComparer.Ordinal);
	private bool _alreadyDisposed;

	///<inheritdoc/>
	public void Init(IConfiguration configuration)
	{
		// Initialize early, without access to configuration or services
		Log.Logger = new LoggerConfiguration()
			.ReadFrom.Configuration(configuration)
			.Enrich.WithExceptionDetails()
			.CreateBootstrapLogger();
	}

	///<inheritdoc/>
	public MicrosoftLogger GetBootstrapLogger(string categoryName)
		=> _loggers.GetOrAdd(categoryName, new SerilogWrapper(categoryName));

	private void Dispose(bool disposing)
	{
		if (_alreadyDisposed)
		{
			return;
		}

		if (disposing)
		{
			_loggers.Clear();
		}

		_alreadyDisposed = true;
	}

	///<inheritdoc/>
	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
