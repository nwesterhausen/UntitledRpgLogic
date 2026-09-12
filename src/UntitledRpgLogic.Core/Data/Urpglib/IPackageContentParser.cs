using System.Formats.Tar;

namespace UntitledRpgLogic.Core.Data.Urpglib;

/// <summary>
///     Contract for service which parses the payload of a `.urpglib` package for its content definitions.
/// </summary>
public interface IPackageContentParser
{
	/// <summary>
	///     Parses the TAR payload of a `.urpglib` package.
	/// </summary>
	/// <param name="tarReader">Active TarReader positioned over the package payload.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>Extracted definition collections ready for persistence.</returns>
	public Task<ExtractedPackageContent> ParsePayloadAsync(TarReader tarReader,
		CancellationToken cancellationToken = default);
}
