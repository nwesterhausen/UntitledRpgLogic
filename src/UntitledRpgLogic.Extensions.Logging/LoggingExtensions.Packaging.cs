using Microsoft.Extensions.Logging;
using ZLogger;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace UntitledRpgLogic.Extensions.Logging;

public static partial class PackagingLoggingExtensions
{
	[ZLoggerMessage(
		EventId = EventIdValues.ConfigurationFileOfUhandledType,
		Level = LogLevel.Error,
		Message = "Unable to handle type {definitionType}")]
	public static partial void ConfigurationFileOfUhandledType(
		this ILogger logger,
		Type definitionType
	);
}
