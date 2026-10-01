using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpg.LibraryFile;

namespace UntitledRpgLogic.Core.Data;

/// <summary>
///     Details on an available .urpglib module
/// </summary>
/// <param name="packageManifest"></param>
public record ModuleInfo
{
	public ModuleInfo() => this.Manifest = new PackageManifest { AuthorName = "noone", Name = "noname" };

	public ModuleInfo(PackageManifest packageManifest)
	{
		this.Manifest = packageManifest;
		this.ModuleId = this.Manifest.Id;
	}

	public ICollection<ContentSummaryEntry> ContentSummary { get; init; } = new List<ContentSummaryEntry>();

	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public required Ulid ModuleId { get; init; }

	[NotMapped] public string Name => this.Manifest.Name;

	[NotMapped] public string Description => this.Manifest.Description;

	[NotMapped] public string Version => this.Manifest.Version.ToString();

	[NotMapped] public string Author => this.Manifest.AuthorName;

	[NotMapped] public Ulid AuthorId => this.Manifest.AuthorId;

	public string FilePath { get; init; } = string.Empty;
	public PackageManifest Manifest { get; init; }

	/// <summary>
	///     Add 1 instance of the definition type to the summary
	/// </summary>
	/// <param name="definitionType"></param>
	public void AddDefinitionCount(string definitionType)
	{
		if (this.ContentSummary.Any(contentSummary => contentSummary.Category != definitionType))
		{
			this.ContentSummary.First(contentSummary => contentSummary.Category == definitionType).Count++;
		}
		else
		{
			this.ContentSummary.Add(new ContentSummaryEntry(definitionType, 1));
		}
	}

	/// <summary>
	///     Set the amount of definition type in the summary
	/// </summary>
	/// <param name="definitionType"></param>
	/// <param name="count"></param>
	public void SetDefinitionCount(string definitionType, uint count)
	{
		if (this.ContentSummary.Any(contentSummary => contentSummary.Category != definitionType))
		{
			this.ContentSummary.First(contentSummary => contentSummary.Category == definitionType).Count = count;
		}
		else
		{
			this.ContentSummary.Add(new ContentSummaryEntry(definitionType, count));
		}
	}

	/// <summary>
	///     Clear all data in the content summary
	/// </summary>
	public void ResetContentSummary() => this.ContentSummary.Clear();
}
