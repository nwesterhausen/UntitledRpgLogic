namespace UntitledRpgLogic.Core.Enums;

/// <summary>
/// Defines the type of a network message, indicating how its payload should be interpreted.
/// </summary>
public enum MessageType
{
	/// <summary>
	/// Represents an unassigned or invalid message type. This is the default value.
	/// </summary>
	None = 0,

	// --------------------------------------------------
	// Session & Connection Management (1-99)
	// --------------------------------------------------
	/// <summary>
	/// A message to acknowledge a connection or a specific message.
	/// </summary>
	Ack = 1,

	/// <summary>
	/// A message sent by a client to request authentication.
	/// </summary>
	AuthRequest,

	/// <summary>
	/// A response from the server indicating the result of an authentication attempt.
	/// </summary>
	AuthResponse,

	/// <summary>
	/// A message indicating a client has successfully connected and is ready.
	/// </summary>
	ClientReady,

	/// <summary>
	/// A message indicating a client is disconnecting.
	/// </summary>
	Disconnect,

	// --------------------------------------------------
	// Player & Entity State (100-199)
	// --------------------------------------------------
	/// <summary>
	/// A message containing an update to an entity's position, rotation, and velocity.
	/// </summary>
	EntityStateUpdate = 100,

	/// <summary>
	/// A message to spawn a new entity in the world.
	/// </summary>
	SpawnEntity,

	/// <summary>
	/// A message to despawn an entity from the world.
	/// </summary>
	DespawnEntity,

	/// <summary>
	/// A message containing an update to an entity's stats (e.g., health, mana).
	/// </summary>
	StatUpdate,

	// --------------------------------------------------
	// Player Actions & Abilities (200-299)
	// --------------------------------------------------
	/// <summary>
	/// A message indicating a player is using an ability.
	/// </summary>
	UseAbility = 200,

	/// <summary>
	/// A message indicating a player is interacting with an object in the world.
	/// </summary>
	Interact,

	// --------------------------------------------------
	// Inventory & Item Management (300-399)
	// --------------------------------------------------
	/// <summary>
	/// A message to move an item within an inventory or between inventories.
	/// </summary>
	MoveItem = 300,

	/// <summary>
	/// A message to equip an item.
	/// </summary>
	EquipItem,

	/// <summary>
	/// A message to unequip an item.
	/// </summary>
	UnequipItem,

	/// <summary>
	/// A message sent when a player loots an item from the world or a container.
	/// </summary>
	LootItem,

	// --------------------------------------------------
	// Social Systems (400-499)
	// --------------------------------------------------
	/// <summary>
	/// A message for global chat.
	/// </summary>
	GlobalChat = 400,

	/// <summary>
	/// A private message sent from one player to another.
	/// </summary>
	Whisper,

	/// <summary>
	/// A message sent to a player's party.
	/// </summary>
	PartyChat,

	/// <summary>
	/// A message sent to a player's guild.
	/// </summary>
	GuildChat,

	/// <summary>
	/// A message to invite a player to a party.
	/// </summary>
	PartyInvite,

	// --------------------------------------------------
	// World State (500-599)
	// --------------------------------------------------
	/// <summary>
	/// A message containing an update to the world's time.
	/// </summary>
	TimeUpdate = 500,

	/// <summary>
	/// A message containing an update to the world's weather.
	/// </summary>
	WeatherUpdate,
}
