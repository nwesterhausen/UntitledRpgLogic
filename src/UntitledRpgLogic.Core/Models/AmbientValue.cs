using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents an ambient measurement or offset for an environmental property.
///     Stored as a compact scalar component in owned JSON collections.
/// </summary>
/// <param name="Type">The categorical type of ambient condition being measured.</param>
/// <param name="Value">The scalar magnitude or offset for the ambient condition.</param>
public record AmbientValue(AmbientType Type, float Value);
