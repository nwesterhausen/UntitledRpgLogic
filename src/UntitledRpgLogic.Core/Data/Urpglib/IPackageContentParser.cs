namespace UntitledRpgLogic.Core.Data.Urpglib;

/// <summary>
/// 	Contract for service which parses the payload of a `.urpglib` file for its content.
/// </summary>
public interface IPackageContentParser
{
	/// <summary>
	/// 	Parses the payload stream of a `.urpglib` file.
	/// </summary>
	/// <param name="payloadStream">Readable stream containing the raw .urpglib payload.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>Parsed content of the payload</returns>
	Task<ExtractedPackageContent> ParsePayloadAsync(Stream payloadStream, CancellationToken cancellationToken = default);
}
