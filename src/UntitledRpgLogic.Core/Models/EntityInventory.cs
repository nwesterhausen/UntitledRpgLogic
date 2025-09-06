using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces.Data;
using UntitledRpgLogic.Core.Interfaces.Inventory;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Link table relating an Entity to the ItemInstances it owns/holds.
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class EntityInventory: IInventory, IDbEntity<int>
{

    #region Persisted State

    /// <summary>
    ///     The unique primary key for this inventory.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    ///     The maximum number of unique items this inventory can hold.
    ///     This is part of the inventory's essential, persisted state.
    /// </summary>
    public int Capacity { get; set; }

    /// <summary>
    ///     The foreign key linking this inventory back to its owning Entity.
    /// </summary>
    public Ulid EntityId { get; set; }

    /// <summary>
    ///     Navigation property to the owning Entity.
    /// </summary>
    [ForeignKey(nameof(EntityId))]
    public virtual Entity Entity { get; set; } = null!;

    /// <summary>
    ///     The collection of item instances held within this inventory.
    ///     EF Core will manage this one-to-many relationship.
    /// </summary>
    public virtual IReadOnlyCollection<ItemInstance> Items { get; set; } = new List<ItemInstance>();

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

    // --- Other IInventory properties are implemented here as calculated properties ---
    /// <inheritdoc />
    [NotMapped]
    public float Usage => this.HasLimitedStorage ? (float)this.UniqueItemCount / this.Capacity : 0;

    #endregion

    #region Behavior (Not Mapped to Database)

    /// <inheritdoc />
    public bool StoreItem(IStorable item) => throw new NotImplementedException();
    /// <inheritdoc />
    public bool TryRetrieveItem(Guid itemId, out IStorable? item) => throw new NotImplementedException();
    /// <inheritdoc />
    public bool TryRetrieveItem(string itemName, out IStorable? item) => throw new NotImplementedException();
    /// <inheritdoc />
    public ICurrency? DepositCurrency(ICurrency currency, int? amount = null) => throw new NotImplementedException();
    /// <inheritdoc />
    public ICurrency? WithdrawCurrency(ICurrency currency, int amount = 1) => throw new NotImplementedException();

    // Events are not mapped to the database by default.
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
}
