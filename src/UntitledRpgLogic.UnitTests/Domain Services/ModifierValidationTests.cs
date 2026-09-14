using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Services.Domains;

namespace UntitledRpgLogic.UnitTests.Domain_Services;

[TestClass]
public sealed class ModifierValidationTests
{
	private readonly ModifierApplicationService modifierService = new();

	[TestMethod]
	public void TryApplyModifier_NewModifier_AddsToCollection()
	{
		var entity = new Entity(new Name("Target"));
		var modDef = new ModifierDefinition(new Name("Poison")) { MaxStacks = 3, Duration = 10f };

		var now = DateTimeOffset.UtcNow;
		var applied = this.modifierService.TryApplyModifier(entity, modDef, now);

		Assert.IsTrue(applied);
		Assert.HasCount(1, entity.AppliedModifiers);
		Assert.AreEqual(1, entity.AppliedModifiers.First().Stacks);
		Assert.AreEqual(now.AddSeconds(10), entity.AppliedModifiers.First().ExpiresAt);
	}

	[TestMethod]
	public void TryApplyModifier_AtMaxStacks_RefreshesDurationWithoutIncreasingStack()
	{
		var entity = new Entity(new Name("Target"));
		var modDef = new ModifierDefinition(new Name("Rage")) { MaxStacks = 1, Duration = 5f };

		var t0 = DateTimeOffset.UtcNow;
		this.modifierService.TryApplyModifier(entity, modDef, t0);

		var t1 = t0.AddSeconds(2);
		var appliedAgain = this.modifierService.TryApplyModifier(entity, modDef, t1);

		Assert.IsFalse(appliedAgain);
		Assert.HasCount(1, entity.AppliedModifiers);
		Assert.AreEqual(1, entity.AppliedModifiers.First().Stacks);
		Assert.AreEqual(t1.AddSeconds(5), entity.AppliedModifiers.First().ExpiresAt);
	}

	[TestMethod]
	public void TickModifiers_ExpiredModifierWithLoseAllStacks_RemovesModifier()
	{
		var entity = new Entity(new Name("Target"));
		var modDef = new ModifierDefinition(new Name("Stun")) { Duration = 2f, LoseAllStacksOnExpiration = true };

		var now = DateTimeOffset.UtcNow;
		this.modifierService.TryApplyModifier(entity, modDef, now);

		// Tick at 3 seconds later
		this.modifierService.TickModifiers(entity, now.AddSeconds(3));

		Assert.IsEmpty(entity.AppliedModifiers);
	}
}
