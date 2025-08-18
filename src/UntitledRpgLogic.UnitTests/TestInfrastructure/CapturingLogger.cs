using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.UnitTests.TestInfrastructure;

public sealed class CapturingLoggerProvider : ILoggerProvider
{
	private readonly ConcurrentBag<LogEntry> _entries = new();

	public IReadOnlyCollection<LogEntry> Entries => _entries;

	public ILogger CreateLogger(string categoryName) => new CapturingLogger(categoryName, _entries);

	public void Dispose() { }
}

public sealed record LogEntry(string Category, LogLevel Level, EventId EventId, string? Message);

internal sealed class CapturingLogger : ILogger
{
	private readonly string _category;
	private readonly ConcurrentBag<LogEntry> _entries;

	public CapturingLogger(string category, ConcurrentBag<LogEntry> entries)
	{
		_category = category;
		_entries = entries;
	}

	public IDisposable BeginScope<TState>(TState state) where TState : notnull => NullScope.Instance;

	public bool IsEnabled(LogLevel logLevel) => true;

	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
	{
		var message = formatter(state, exception);
		_entries.Add(new LogEntry(_category, logLevel, eventId, message));
	}

	private sealed class NullScope : IDisposable
	{
		public static readonly NullScope Instance = new();
		public void Dispose() { }
	}
}
