using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces.Data;
using UntitledRpgLogic.Core.Interfaces.Inventory;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Database model and container representing an inventory owned by an <see cref="Entity" />.
/// </summary>
[Table("entity_inventories")]
public record EntityInventory : IInventory, IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="EntityInventory" /> record for EF Core.
	/// </summary>
	public EntityInventory()
	{
		this.Id = Ulid.NewUlid();
		this.EntityId = Ulid.Empty;
		this.Capacity = 20;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="EntityInventory" /> record for a specific entity.
	/// </summary>
	/// <param name="entityId">The identifier of the owning entity.</param>
	public EntityInventory(Ulid entityId) : this() => this.EntityId = entityId;

	#region Persisted State

	/// <summary>
	///     The unique primary key for this inventory instance.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     Foreign key linking this inventory back to its owning entity.
	/// </summary>
	public Ulid EntityId { get; init; }

	/// <summary>
	///     Navigation property to the owning entity.
	/// </summary>
	[ForeignKey(nameof(EntityId))]
	public virtual Entity? Entity { get; init; }

	/// <summary>
	///     The maximum number of item slots this inventory can hold.
	/// </summary>
	public int Capacity { get; set; }

	/// <summary>
	///     The collection of item instances held within this inventory.
	/// </summary>
	public virtual ICollection<ItemInstance> Items { get; init; } = [];

	#endregion

	#region Calculated State (Not Mapped to Database)

	/// <inheritdoc />
	[NotMapped]
	public int ItemCount => this.Items.Sum(i => i.Quantity);

	/// <inheritdoc />
	[NotMapped]
	public int UniqueItemCount => this.Items.Count;

	/// <inheritdoc />
	[NotMapped]
	public bool HasLimitedStorage => this.Capacity > 0;

	/// <inheritdoc />
	[NotMapped]
	public bool IsFull => this.HasLimitedStorage && this.UniqueItemCount >= this.Capacity;

	/// <inheritdoc />
	[NotMapped]
	public float Usage => this.HasLimitedStorage ? (float)this.UniqueItemCount / this.Capacity : 0f;

	/// <inheritdoc />
	[NotMapped]
	public float SpaceRemaining => throw new NotImplementedException();

	/// <inheritdoc />
	[NotMapped]
	public float SpaceUsed => throw new NotImplementedException();

	/// <inheritdoc />
	[NotMapped]
	public bool HasUnlimitedCurrencyStorage => throw new NotImplementedException();

	/// <inheritdoc />
	[NotMapped]
	public bool AllowsStoringInventories => throw new NotImplementedException();

	/// <inheritdoc />
	[NotMapped]
	public bool AllowsStacking => throw new NotImplementedException();

	#endregion

	#region Behavior (Not Mapped to Database)

	/// <inheritdoc />
	public bool StoreItem(IStorable item) => throw new NotImplementedException();

	/// <inheritdoc />
	public bool TryRetrieveItem(Ulid itemId, out IStorable? item) => throw new NotImplementedException();

	/// <inheritdoc />
	public bool TryRetrieveItem(string itemName, out IStorable? item) => throw new NotImplementedException();

	/// <inheritdoc />
	public ICurrency? DepositCurrency(ICurrency currency, int? amount = null) => throw new NotImplementedException();

	/// <inheritdoc />
	public ICurrency? WithdrawCurrency(ICurrency currency, int amount = 1) => throw new NotImplementedException();

	/// <inheritdoc />
	public event EventHandler<SuccessfulItemStorageEventArgs>? ItemStored;

	/// <inheritdoc />
	public event EventHandler<CancelableItemActionEventArgs>? StoringItem;

	/// <inheritdoc />
	public event EventHandler<SuccessfulItemStorageEventArgs>? ItemRetrieved;

	/// <inheritdoc />
	public event EventHandler<CancelableItemActionEventArgs>? RetrievingItem;

	/// <inheritdoc />
	public event EventHandler<CurrencyMovedEventArgs>? CurrencyDeposited;

	/// <inheritdoc />
	public event EventHandler<CurrencyMovedEventArgs>? CurrencyWithdrawn;

	#endregion
}
