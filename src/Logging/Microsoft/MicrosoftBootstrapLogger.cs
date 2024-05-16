using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Toolkit.Extensions.Plugins;

namespace Toolkit.Extensions.Logging.Microsoft;

/// <summary>
/// Microsoft bootstrap logger setup.
/// </summary>
internal sealed class MicrosoftBootstrapLogger : IBootstrapLoggerPlugin
{
	private const string DefaultLogLevelSectionName = "Logging:LogLevel:Default";
	private const string ConsoleLogSectionName = "Logging:Console";

	private readonly ConcurrentDictionary<string, ILogger> _loggers = new(StringComparer.Ordinal);

	private ILoggerFactory? _loggerFactory;
	private bool _alreadyDisposed;

	///<inheritdoc/>
	public void Init(IConfiguration configuration)
	{
		_loggerFactory = LoggerFactory.Create(builder =>
		{
			_ = builder
				.SetMinimumLevel(configuration.GetValue<LogLevel>(DefaultLogLevelSectionName));
			if (configuration.GetSection(ConsoleLogSectionName).Exists())
			{
				_ = builder.AddConsole();
			}
		});
	}

	///<inheritdoc/>
	public ILogger GetBootstrapLogger(string categoryName)
		=> _loggers.GetOrAdd(categoryName, _loggerFactory!.CreateLogger(categoryName));

	private void Dispose(bool disposing)
	{
		if (_alreadyDisposed)
		{
			return;
		}

		if (disposing)
		{
			_loggerFactory!.Dispose();
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
