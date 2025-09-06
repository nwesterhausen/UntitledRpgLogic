using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Interfaces.Common;

/// <summary>
///     Interface for getting identity information.
/// </summary>
/// <remarks>
///		The unique domain identifier for this type of object is also its primary key for persistence.
/// </remarks>
public interface IHasIdentifier : IDbEntity<Ulid>;
