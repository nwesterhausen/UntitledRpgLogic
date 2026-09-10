using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Core.Interfaces.Entities;

/// <summary>
///     Defines an interactive entity capable of training, learning, and exercising skills.
/// </summary>
public interface IHasSkills
{
	/// <summary>
	///     Gets the active collection of skills acquired by this entity.
	/// </summary>
	ICollection<EntitySkills> Skills { get; }
}
