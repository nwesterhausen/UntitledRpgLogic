using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
/// Contains definitions for all EventIds used in the application for structured logging.
/// </summary>
public static class EventIds
{
	// System & Error (0-999)
	/// <summary>
	/// An invalid or uninitialized event ID.
	/// </summary>
	public static readonly EventId None = new(EventIdValues.None, "None");

	/// <summary>
	/// An unexpected error occurred that was not handled by specific error handling logic.
	/// </summary>
	public static readonly EventId UnexpectedError = new(EventIdValues.UnexpectedError, "UnexpectedError");

	/// <summary>
	/// A validation error occurred when processing user input or data.
	/// </summary>
	public static readonly EventId ValidationError = new(EventIdValues.ValidationError, "ValidationError");

	/// <summary>
	/// An operation was attempted that is not supported in the current context.
	/// </summary>
	public static readonly EventId OperationNotSupported = new(EventIdValues.OperationNotSupported, "OperationNotSupported");

	// Player-Driven Events (1000-4999)
	// Sub-category: Game Session (1000-10xx)
	/// <summary>
	/// A player has logged into the game.
	/// </summary>
	public static readonly EventId PlayerLogin = new(EventIdValues.PlayerLogin, "PlayerLogin");

	/// <summary>
	/// A player has logged out of the game.
	/// </summary>
	public static readonly EventId PlayerLogout = new(EventIdValues.PlayerLogout, "PlayerLogout");

	// Sub-category: Inventory (1100-11xx)
	/// <summary>
	/// A player has equipped an item.
	/// </summary>
	public static readonly EventId PlayerEquipItem = new(EventIdValues.PlayerEquipItem, "PlayerEquipItem");

	/// <summary>
	/// A player has moved an item within their inventory.
	/// </summary>
	public static readonly EventId PlayerMoveItem = new(EventIdValues.PlayerMoveItem, "PlayerMoveItem");

	/// <summary>
	/// A player's inventory is full and cannot accept more items.
	/// </summary>
	public static readonly EventId PlayerInventoryFull = new(EventIdValues.PlayerInventoryFull, "PlayerInventoryFull");

	/// <summary>
	/// A player has looted an item.
	/// </summary>
	public static readonly EventId PlayerLootItem = new(EventIdValues.PlayerLootItem, "PlayerLootItem");

	// Sub-category: Combat and Abilities (1200-12xx)
	/// <summary>
	/// A player has used an ability.
	/// </summary>
	public static readonly EventId PlayerUseAbility = new(EventIdValues.PlayerUseAbility, "PlayerUseAbility");

	/// <summary>
	/// A player has performed an attack.
	/// </summary>
	public static readonly EventId PlayerAttack = new(EventIdValues.PlayerAttack, "PlayerAttack");

	/// <summary>
	/// A player has taken damage.
	/// </summary>
	public static readonly EventId PlayerTakeDamage = new(EventIdValues.PlayerTakeDamage, "PlayerTakeDamage");

	/// <summary>
	/// A player has been healed.
	/// </summary>
	public static readonly EventId PlayerHeal = new(EventIdValues.PlayerHeal, "PlayerHeal");

	/// <summary>
	/// A player has died.
	/// </summary>
	public static readonly EventId PlayerDie = new(EventIdValues.PlayerDie, "PlayerDie");

	/// <summary>
	/// A player has respawned.
	/// </summary>
	public static readonly EventId PlayerRespawn = new(EventIdValues.PlayerRespawn, "PlayerRespawn");

	/// <summary>
	/// A player has landed a critical hit.
	/// </summary>
	public static readonly EventId PlayerCriticalHit = new(EventIdValues.PlayerCriticalHit, "PlayerCriticalHit");

	/// <summary>
	/// A player's attack has missed.
	/// </summary>
	public static readonly EventId PlayerMissedAttack = new(EventIdValues.PlayerMissedAttack, "PlayerMissedAttack");

	/// <summary>
	/// A status effect has been applied to a player.
	/// </summary>
	public static readonly EventId PlayerStatusEffectApplied = new(EventIdValues.PlayerStatusEffectApplied, "PlayerStatusEffectApplied");

	/// <summary>
	/// A status effect has been removed from a player.
	/// </summary>
	public static readonly EventId PlayerStatusEffectRemoved = new(EventIdValues.PlayerStatusEffectRemoved, "PlayerStatusEffectRemoved");

	/// <summary>
	/// A player has leveled up.
	/// </summary>
	public static readonly EventId PlayerLevelUp = new(EventIdValues.PlayerLevelUp, "PlayerLevelUp");

	/// <summary>
	/// A player's skill has increased.
	/// </summary>
	public static readonly EventId PlayerSkillIncreased = new(EventIdValues.PlayerSkillIncreased, "PlayerSkillIncreased");

	/// <summary>
	/// A player's attribute has increased.
	/// </summary>
	public static readonly EventId PlayerAttributeIncreased = new(EventIdValues.PlayerAttributeIncreased, "PlayerAttributeIncreased");

	/// <summary>
	/// A player has blocked an attack.
	/// </summary>
	public static readonly EventId PlayerBlock = new(EventIdValues.PlayerBlock, "PlayerBlock");

	/// <summary>
	/// A player has dodged an attack.
	/// </summary>
	public static readonly EventId PlayerDodge = new(EventIdValues.PlayerDodge, "PlayerDodge");

	/// <summary>
	/// A player has parried an attack.
	/// </summary>
	public static readonly EventId PlayerParry = new(EventIdValues.PlayerParry, "PlayerParry");

	// Sub-category: Social Interactions (1300-13xx)
	/// <summary>
	/// A player has sent a chat message.
	/// </summary>
	public static readonly EventId PlayerChatMessageSent = new(EventIdValues.PlayerChatMessageSent, "PlayerChatMessageSent");

	/// <summary>
	/// A player has received a chat message.
	/// </summary>
	public static readonly EventId PlayerChatMessageReceived = new(EventIdValues.PlayerChatMessageReceived, "PlayerChatMessageReceived");

	/// <summary>
	/// A player's chat message failed to send.
	/// </summary>
	public static readonly EventId PlayerChatMessageFailed = new(EventIdValues.PlayerChatMessageFailed, "PlayerChatMessageFailed");

	/// <summary>
	/// A player has sent a private message.
	/// </summary>
	public static readonly EventId PlayerPrivateMessageSent = new(EventIdValues.PlayerPrivateMessageSent, "PlayerPrivateMessageSent");

	/// <summary>
	/// A player has received a private message.
	/// </summary>
	public static readonly EventId PlayerPrivateMessageReceived = new(EventIdValues.PlayerPrivateMessageReceived, "PlayerPrivateMessageReceived");

	/// <summary>
	/// A player's private message failed to send.
	/// </summary>
	public static readonly EventId PlayerPrivateMessageFailed = new(EventIdValues.PlayerPrivateMessageFailed, "PlayerPrivateMessageFailed");

	/// <summary>
	/// A player has performed an emote.
	/// </summary>
	public static readonly EventId PlayerEmotePerformed = new(EventIdValues.PlayerEmotePerformed, "PlayerEmotePerformed");

	/// <summary>
	/// A player has sent a friend request.
	/// </summary>
	public static readonly EventId PlayerFriendRequestSent = new(EventIdValues.PlayerFriendRequestSent, "PlayerFriendRequestSent");

	/// <summary>
	/// A player has received a friend request.
	/// </summary>
	public static readonly EventId PlayerFriendRequestReceived = new(EventIdValues.PlayerFriendRequestReceived, "PlayerFriendRequestReceived");

	/// <summary>
	/// A player has accepted a friend request.
	/// </summary>
	public static readonly EventId PlayerFriendRequestAccepted = new(EventIdValues.PlayerFriendRequestAccepted, "PlayerFriendRequestAccepted");

	/// <summary>
	/// A player has declined a friend request.
	/// </summary>
	public static readonly EventId PlayerFriendRequestDeclined = new(EventIdValues.PlayerFriendRequestDeclined, "PlayerFriendRequestDeclined");

	/// <summary>
	/// A player has blocked another user.
	/// </summary>
	public static readonly EventId PlayerBlockedUser = new(EventIdValues.PlayerBlockedUser, "PlayerBlockedUser");

	/// <summary>
	/// A player has unblocked another user.
	/// </summary>
	public static readonly EventId PlayerUnblockedUser = new(EventIdValues.PlayerUnblockedUser, "PlayerUnblockedUser");

	// Sub-category: Quests and Achievements (1400-14xx)
	/// <summary>
	/// A player has accepted a quest.
	/// </summary>
	public static readonly EventId PlayerQuestAccepted = new(EventIdValues.PlayerQuestAccepted, "PlayerQuestAccepted");

	/// <summary>
	/// A player has completed a quest.
	/// </summary>
	public static readonly EventId PlayerQuestCompleted = new(EventIdValues.PlayerQuestCompleted, "PlayerQuestCompleted");

	/// <summary>
	/// A player has failed a quest.
	/// </summary>
	public static readonly EventId PlayerQuestFailed = new(EventIdValues.PlayerQuestFailed, "PlayerQuestFailed");

	/// <summary>
	/// A player has abandoned a quest.
	/// </summary>
	public static readonly EventId PlayerQuestAbandoned = new(EventIdValues.PlayerQuestAbandoned, "PlayerQuestAbandoned");

	/// <summary>
	/// A player has unlocked an achievement.
	/// </summary>
	public static readonly EventId PlayerAchievementUnlocked = new(EventIdValues.PlayerAchievementUnlocked, "PlayerAchievementUnlocked");

	// System-Driven Events (5000-6999)
	// Sub-category: Application Lifecycle (5000-50xx)
	/// <summary>
	/// The application is starting up.
	/// </summary>
	public static readonly EventId ApplicationStarting = new(EventIdValues.ApplicationStarting, "ApplicationStarting");

	/// <summary>
	/// The application has started successfully.
	/// </summary>
	public static readonly EventId ApplicationStarted = new(EventIdValues.ApplicationStarted, "ApplicationStarted");

	/// <summary>
	/// The application is stopping.
	/// </summary>
	public static readonly EventId ApplicationStopping = new(EventIdValues.ApplicationStopping, "ApplicationStopping");

	/// <summary>
	/// The application has stopped.
	/// </summary>
	public static readonly EventId ApplicationStopped = new(EventIdValues.ApplicationStopped, "ApplicationStopped");

	// Sub-category: Configuration and Library File Operations (5100-51xx)
	/// <summary>
	/// A library file is being loaded.
	/// </summary>
	public static readonly EventId LoadingLibraryFile = new(EventIdValues.LoadingLibraryFile, "LoadingLibraryFile");

	/// <summary>
	/// A library file has been loaded successfully.
	/// </summary>
	public static readonly EventId LibraryFileLoaded = new(EventIdValues.LibraryFileLoaded, "LibraryFileLoaded");

	/// <summary>
	/// A library file failed to load.
	/// </summary>
	public static readonly EventId LibraryFileLoadFailed = new(EventIdValues.LibraryFileLoadFailed, "LibraryFileLoadFailed");

	/// <summary>
	/// A library header mismatch was detected.
	/// </summary>
	public static readonly EventId LibraryHeaderMismatch = new(EventIdValues.LibraryHeaderMismatch, "LibraryHeaderMismatch");

	/// <summary>
	/// A library file is being saved.
	/// </summary>
	public static readonly EventId SavingLibraryFile = new(EventIdValues.SavingLibraryFile, "SavingLibraryFile");

	/// <summary>
	/// A library file has been saved successfully.
	/// </summary>
	public static readonly EventId LibraryFileSaved = new(EventIdValues.LibraryFileSaved, "LibraryFileSaved");

	/// <summary>
	/// A library file failed to save.
	/// </summary>
	public static readonly EventId LibraryFileSaveFailed = new(EventIdValues.LibraryFileSaveFailed, "LibraryFileSaveFailed");

	/// <summary>
	/// A library file header has been parsed.
	/// </summary>
	public static readonly EventId LibraryFileHeaderParsed = new(EventIdValues.LibraryFileHeaderParsed, "LibraryFileHeaderParsed");

	/// <summary>
	/// A library file header failed to parse.
	/// </summary>
	public static readonly EventId LibraryFileHeaderParseFailed = new(EventIdValues.LibraryFileHeaderParseFailed, "LibraryFileHeaderParseFailed");

	/// <summary>
	/// Library file data has been parsed.
	/// </summary>
	public static readonly EventId LibraryFileDataParsed = new(EventIdValues.LibraryFileDataParsed, "LibraryFileDataParsed");

	/// <summary>
	/// Library file data failed to parse.
	/// </summary>
	public static readonly EventId LibraryFileDataParseFailed = new(EventIdValues.LibraryFileDataParseFailed, "LibraryFileDataParseFailed");

	/// <summary>
	/// Configuration files have been enumerated.
	/// </summary>
	public static readonly EventId ConfigurationFilesEnumerated = new(EventIdValues.ConfigurationFilesEnumerated, "ConfigurationFilesEnumerated");

	/// <summary>
	/// A configuration file has been loaded.
	/// </summary>
	public static readonly EventId ConfigurationFileLoaded = new(EventIdValues.ConfigurationFileLoaded, "ConfigurationFileLoaded");

	/// <summary>
	/// A configuration file failed to load.
	/// </summary>
	public static readonly EventId ConfigurationFileLoadFailed = new(EventIdValues.ConfigurationFileLoadFailed, "ConfigurationFileLoadFailed");

	/// <summary>
	/// A configuration file has been saved.
	/// </summary>
	public static readonly EventId ConfigurationFileSaved = new(EventIdValues.ConfigurationFileSaved, "ConfigurationFileSaved");

	/// <summary>
	/// A configuration file failed to save.
	/// </summary>
	public static readonly EventId ConfigurationFileSaveFailed = new(EventIdValues.ConfigurationFileSaveFailed, "ConfigurationFileSaveFailed");

	/// <summary>
	/// Configuration file compression failed.
	/// </summary>
	public static readonly EventId ConfigurationFileCompressionFailed = new(EventIdValues.ConfigurationFileCompressionFailed, "ConfigurationFileCompressionFailed");

	/// <summary>
	/// Configuration file decompression failed.
	/// </summary>
	public static readonly EventId ConfigurationFileDecompressionFailed = new(EventIdValues.ConfigurationFileDecompressionFailed, "ConfigurationFileDecompressionFailed");

	// Sub-category: Network Operations (5200-52xx)
	/// <summary>
	/// A network listener is starting.
	/// </summary>
	public static readonly EventId NetworkListenerStarting = new(EventIdValues.NetworkListenerStarting, "NetworkListenerStarting");

	/// <summary>
	/// A network listener has started.
	/// </summary>
	public static readonly EventId NetworkListenerStarted = new(EventIdValues.NetworkListenerStarted, "NetworkListenerStarted");

	/// <summary>
	/// A network listener is stopping.
	/// </summary>
	public static readonly EventId NetworkListenerStopping = new(EventIdValues.NetworkListenerStopping, "NetworkListenerStopping");

	/// <summary>
	/// A network listener has stopped.
	/// </summary>
	public static readonly EventId NetworkListenerStopped = new(EventIdValues.NetworkListenerStopped, "NetworkListenerStopped");

	/// <summary>
	/// A network listener encountered an error.
	/// </summary>
	public static readonly EventId NetworkListenerError = new(EventIdValues.NetworkListenerError, "NetworkListenerError");

	/// <summary>
	/// A network packet has been received.
	/// </summary>
	public static readonly EventId NetworkPacketReceived = new(EventIdValues.NetworkPacketReceived, "NetworkPacketReceived");

	/// <summary>
	/// A network packet has been processed.
	/// </summary>
	public static readonly EventId NetworkPacketProcessed = new(EventIdValues.NetworkPacketProcessed, "NetworkPacketProcessed");

	/// <summary>
	/// Network packet processing failed.
	/// </summary>
	public static readonly EventId NetworkPacketProcessingFailed = new(EventIdValues.NetworkPacketProcessingFailed, "NetworkPacketProcessingFailed");

	/// <summary>
	/// A network packet has been sent.
	/// </summary>
	public static readonly EventId NetworkPacketSent = new(EventIdValues.NetworkPacketSent, "NetworkPacketSent");

	/// <summary>
	/// A network packet failed to send.
	/// </summary>
	public static readonly EventId NetworkPacketSendFailed = new(EventIdValues.NetworkPacketSendFailed, "NetworkPacketSendFailed");

	/// <summary>
	/// A network connection has been established.
	/// </summary>
	public static readonly EventId NetworkConnectionEstablished = new(EventIdValues.NetworkConnectionEstablished, "NetworkConnectionEstablished");

	/// <summary>
	/// A network connection has been closed.
	/// </summary>
	public static readonly EventId NetworkConnectionClosed = new(EventIdValues.NetworkConnectionClosed, "NetworkConnectionClosed");

	/// <summary>
	/// A network connection encountered an error.
	/// </summary>
	public static readonly EventId NetworkConnectionError = new(EventIdValues.NetworkConnectionError, "NetworkConnectionError");

	/// <summary>
	/// Network latency has been detected.
	/// </summary>
	public static readonly EventId NetworkLatencyDetected = new(EventIdValues.NetworkLatencyDetected, "NetworkLatencyDetected");

	/// <summary>
	/// Network throughput has been measured.
	/// </summary>
	public static readonly EventId NetworkThroughputMeasured = new(EventIdValues.NetworkThroughputMeasured, "NetworkThroughputMeasured");

	/// <summary>
	/// Network configuration has been loaded.
	/// </summary>
	public static readonly EventId NetworkConfigurationLoaded = new(EventIdValues.NetworkConfigurationLoaded, "NetworkConfigurationLoaded");

	/// <summary>
	/// Network configuration failed to load.
	/// </summary>
	public static readonly EventId NetworkConfigurationLoadFailed = new(EventIdValues.NetworkConfigurationLoadFailed, "NetworkConfigurationLoadFailed");

	/// <summary>
	/// Network configuration has been saved.
	/// </summary>
	public static readonly EventId NetworkConfigurationSaved = new(EventIdValues.NetworkConfigurationSaved, "NetworkConfigurationSaved");

	/// <summary>
	/// Network configuration failed to save.
	/// </summary>
	public static readonly EventId NetworkConfigurationSaveFailed = new(EventIdValues.NetworkConfigurationSaveFailed, "NetworkConfigurationSaveFailed");

	/// <summary>
	/// A client handshake has been received.
	/// </summary>
	public static readonly EventId ClientHandshakeReceived = new(EventIdValues.ClientHandshakeReceived, "ClientHandshakeReceived");

	/// <summary>
	/// A client handshake was successful.
	/// </summary>
	public static readonly EventId ClientHandshakeSuccessful = new(EventIdValues.ClientHandshakeSuccessful, "ClientHandshakeSuccessful");

	/// <summary>
	/// A client handshake failed.
	/// </summary>
	public static readonly EventId ClientHandshakeFailed = new(EventIdValues.ClientHandshakeFailed, "ClientHandshakeFailed");

	/// <summary>
	/// A client connection has been established.
	/// </summary>
	public static readonly EventId ClientConnectionEstablished = new(EventIdValues.ClientConnectionEstablished, "ClientConnectionEstablished");

	/// <summary>
	/// A client connection has been terminated.
	/// </summary>
	public static readonly EventId ClientConnectionTerminated = new(EventIdValues.ClientConnectionTerminated, "ClientConnectionTerminated");

	/// <summary>
	/// A client connection encountered an error.
	/// </summary>
	public static readonly EventId ClientConnectionError = new(EventIdValues.ClientConnectionError, "ClientConnectionError");

	/// <summary>
	/// A message has been received.
	/// </summary>
	public static readonly EventId MessageReceived = new(EventIdValues.MessageReceived, "MessageReceived");

	/// <summary>
	/// An invalid message has been received.
	/// </summary>
	public static readonly EventId InvalidMessageReceived = new(EventIdValues.InvalidMessageReceived, "InvalidMessageReceived");

	/// <summary>
	/// A message has been processed.
	/// </summary>
	public static readonly EventId MessageProcessed = new(EventIdValues.MessageProcessed, "MessageProcessed");

	/// <summary>
	/// Message processing failed.
	/// </summary>
	public static readonly EventId MessageProcessingFailed = new(EventIdValues.MessageProcessingFailed, "MessageProcessingFailed");

	/// <summary>
	/// A message has been sent.
	/// </summary>
	public static readonly EventId MessageSent = new(EventIdValues.MessageSent, "MessageSent");

	/// <summary>
	/// A message failed to send.
	/// </summary>
	public static readonly EventId MessageSendFailed = new(EventIdValues.MessageSendFailed, "MessageSendFailed");

	/// <summary>
	/// Client authentication has started.
	/// </summary>
	public static readonly EventId ClientAuthenticationStarted = new(EventIdValues.ClientAuthenticationStarted, "ClientAuthenticationStarted");

	/// <summary>
	/// User authentication was successful.
	/// </summary>
	public static readonly EventId ClientAuthenticationSucceeded = new(EventIdValues.ClientAuthenticationSucceeded, "ClientAuthenticationSucceeded");

	/// <summary>
	/// User authentication failed due to invalid credentials or other authentication issues.
	/// </summary>
	public static readonly EventId ClientAuthenticationFailed = new(EventIdValues.ClientAuthenticationFailed, "ClientAuthenticationFailed");

	// Sub-category: Database Operations (5300-53xx)
	/// <summary>
	/// Connecting to the database.
	/// </summary>
	public static readonly EventId DatabaseConnecting = new(EventIdValues.DatabaseConnecting, "DatabaseConnecting");

	/// <summary>
	/// Successfully connected to the database.
	/// </summary>
	public static readonly EventId DatabaseConnected = new(EventIdValues.DatabaseConnected, "DatabaseConnected");

	/// <summary>
	/// Failed to connect to the database.
	/// </summary>
	public static readonly EventId DatabaseConnectionFailed = new(EventIdValues.DatabaseConnectionFailed, "DatabaseConnectionFailed");

	/// <summary>
	/// A database migration has started.
	/// </summary>
	public static readonly EventId MigrationStarted = new(EventIdValues.MigrationStarted, "MigrationStarted");

	/// <summary>
	/// A database migration has completed.
	/// </summary>
	public static readonly EventId MigrationCompleted = new(EventIdValues.MigrationCompleted, "MigrationCompleted");

	/// <summary>
	/// A database migration has failed.
	/// </summary>
	public static readonly EventId MigrationFailed = new(EventIdValues.MigrationFailed, "MigrationFailed");

	/// <summary>
	/// A database query has been executed.
	/// </summary>
	public static readonly EventId DatabaseQueryExecuted = new(EventIdValues.DatabaseQueryExecuted, "DatabaseQueryExecuted");

	/// <summary>
	/// A database query has failed.
	/// </summary>
	public static readonly EventId DatabaseQueryFailed = new(EventIdValues.DatabaseQueryFailed, "DatabaseQueryFailed");

	/// <summary>
	/// A database transaction has started.
	/// </summary>
	public static readonly EventId DatabaseTransactionStarted = new(EventIdValues.DatabaseTransactionStarted, "DatabaseTransactionStarted");

	/// <summary>
	/// A database transaction has been committed.
	/// </summary>
	public static readonly EventId DatabaseTransactionCommitted = new(EventIdValues.DatabaseTransactionCommitted, "DatabaseTransactionCommitted");

	/// <summary>
	/// A database transaction has been rolled back.
	/// </summary>
	public static readonly EventId DatabaseTransactionRolledBack = new(EventIdValues.DatabaseTransactionRolledBack, "DatabaseTransactionRolledBack");

	/// <summary>
	/// Disconnecting from the database.
	/// </summary>
	public static readonly EventId DatabaseDisconnecting = new(EventIdValues.DatabaseDisconnecting, "DatabaseDisconnecting");

	/// <summary>
	/// Successfully disconnected from the database.
	/// </summary>
	public static readonly EventId DatabaseDisconnected = new(EventIdValues.DatabaseDisconnected, "DatabaseDisconnected");

	/// <summary>
	/// Failed to disconnect from the database.
	/// </summary>
	public static readonly EventId DatabaseDisconnectionFailed = new(EventIdValues.DatabaseDisconnectionFailed, "DatabaseDisconnectionFailed");

	/// <summary>
	/// Database configuration has been loaded.
	/// </summary>
	public static readonly EventId DatabaseConfigurationLoaded = new(EventIdValues.DatabaseConfigurationLoaded, "DatabaseConfigurationLoaded");

	/// <summary>
	/// Database configuration failed to load.
	/// </summary>
	public static readonly EventId DatabaseConfigurationLoadFailed = new(EventIdValues.DatabaseConfigurationLoadFailed, "DatabaseConfigurationLoadFailed");

	/// <summary>
	/// Database configuration has been saved.
	/// </summary>
	public static readonly EventId DatabaseConfigurationSaved = new(EventIdValues.DatabaseConfigurationSaved, "DatabaseConfigurationSaved");

	/// <summary>
	/// Database configuration failed to save.
	/// </summary>
	public static readonly EventId DatabaseConfigurationSaveFailed = new(EventIdValues.DatabaseConfigurationSaveFailed, "DatabaseConfigurationSaveFailed");

	/// <summary>
	/// A managed database server is starting.
	/// </summary>
	public static readonly EventId ManagedDatabaseServerStarting = new(EventIdValues.ManagedDatabaseServerStarting, "ManagedDatabaseServerStarting");

	/// <summary>
	/// A managed database server has started.
	/// </summary>
	public static readonly EventId ManagedDatabaseServerStarted = new(EventIdValues.ManagedDatabaseServerStarted, "ManagedDatabaseServerStarted");

	/// <summary>
	/// A managed database server is stopping.
	/// </summary>
	public static readonly EventId ManagedDatabaseServerStopping = new(EventIdValues.ManagedDatabaseServerStopping, "ManagedDatabaseServerStopping");

	/// <summary>
	/// A managed database server has stopped.
	/// </summary>
	public static readonly EventId ManagedDatabaseServerStopped = new(EventIdValues.ManagedDatabaseServerStopped, "ManagedDatabaseServerStopped");

	/// <summary>
	/// A managed database server encountered an error.
	/// </summary>
	public static readonly EventId ManagedDatabaseServerError = new(EventIdValues.ManagedDatabaseServerError, "ManagedDatabaseServerError");

	/// <summary>
	/// A managed database server backup has started.
	/// </summary>
	public static readonly EventId ManagedDatabaseServerBackupStarted = new(EventIdValues.ManagedDatabaseServerBackupStarted, "ManagedDatabaseServerBackupStarted");

	/// <summary>
	/// A managed database server backup has completed.
	/// </summary>
	public static readonly EventId ManagedDatabaseServerBackupCompleted = new(EventIdValues.ManagedDatabaseServerBackupCompleted, "ManagedDatabaseServerBackupCompleted");

	/// <summary>
	/// A managed database server backup has failed.
	/// </summary>
	public static readonly EventId ManagedDatabaseServerBackupFailed = new(EventIdValues.ManagedDatabaseServerBackupFailed, "ManagedDatabaseServerBackupFailed");

	// Sub-category: Persistence Operations (5400-54xx)
	/// <summary>
	/// Player state is being saved.
	/// </summary>
	public static readonly EventId PlayerStateSaving = new(EventIdValues.PlayerStateSaving, "PlayerStateSaving");

	/// <summary>
	/// Player state has been saved successfully.
	/// </summary>
	public static readonly EventId PlayerStateSaved = new(EventIdValues.PlayerStateSaved, "PlayerStateSaved");

	/// <summary>
	/// Player state failed to save.
	/// </summary>
	public static readonly EventId PlayerStateSaveFailed = new(EventIdValues.PlayerStateSaveFailed, "PlayerStateSaveFailed");

	/// <summary>
	/// Player state is being loaded.
	/// </summary>
	public static readonly EventId PlayerStateLoading = new(EventIdValues.PlayerStateLoading, "PlayerStateLoading");

	/// <summary>
	/// Player state has been loaded successfully.
	/// </summary>
	public static readonly EventId PlayerStateLoaded = new(EventIdValues.PlayerStateLoaded, "PlayerStateLoaded");

	/// <summary>
	/// Player state failed to load.
	/// </summary>
	public static readonly EventId PlayerStateLoadFailed = new(EventIdValues.PlayerStateLoadFailed, "PlayerStateLoadFailed");

	// Sub-category: World Events (5500-55xx)
	/// <summary>
	/// The world state has changed.
	/// </summary>
	public static readonly EventId WorldStateChanged = new(EventIdValues.WorldStateChanged, "WorldStateChanged");

	/// <summary>
	/// The weather has changed.
	/// </summary>
	public static readonly EventId WeatherChanged = new(EventIdValues.WeatherChanged, "WeatherChanged");

	/// <summary>
	/// The time of day has changed.
	/// </summary>
	public static readonly EventId TimeOfDayChanged = new(EventIdValues.TimeOfDayChanged, "TimeOfDayChanged");

	/// <summary>
	/// Loot has been generated.
	/// </summary>
	public static readonly EventId LootGenerated = new(EventIdValues.LootGenerated, "LootGenerated");

	/// <summary>
	/// Loot generation has failed.
	/// </summary>
	public static readonly EventId LootGenerationFailed = new(EventIdValues.LootGenerationFailed, "LootGenerationFailed");

	// Sub-category: Entity Events (5600-56xx)
	/// <summary>
	/// An entity is being spawned.
	/// </summary>
	public static readonly EventId EntitySpawning = new(EventIdValues.EntitySpawning, "EntitySpawning");

	/// <summary>
	/// An entity has been spawned.
	/// </summary>
	public static readonly EventId EntitySpawned = new(EventIdValues.EntitySpawned, "EntitySpawned");

	/// <summary>
	/// An entity is being despawned.
	/// </summary>
	public static readonly EventId EntityDespawning = new(EventIdValues.EntityDespawning, "EntityDespawning");

	/// <summary>
	/// An entity has been despawned.
	/// </summary>
	public static readonly EventId EntityDespawned = new(EventIdValues.EntityDespawned, "EntityDespawned");

	/// <summary>
	/// An entity's state has changed.
	/// </summary>
	public static readonly EventId EntityStateChanged = new(EventIdValues.EntityStateChanged, "EntityStateChanged");

	/// <summary>
	/// An entity interaction has occurred.
	/// </summary>
	public static readonly EventId EntityInteraction = new(EventIdValues.EntityInteraction, "EntityInteraction");

	/// <summary>
	/// An entity has been killed.
	/// </summary>
	public static readonly EventId EntityKilled = new(EventIdValues.EntityKilled, "EntityKilled");
}

