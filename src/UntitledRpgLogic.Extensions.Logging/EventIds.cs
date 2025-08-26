using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
/// Contains definitions for all EventIds used in the application for structured logging.
/// </summary>
public static class EventIds
{
	// General Errors (0-999)
	public static readonly EventId UnexpectedError = new(EventIdValues.UnexpectedError, "UnexpectedError");


}

