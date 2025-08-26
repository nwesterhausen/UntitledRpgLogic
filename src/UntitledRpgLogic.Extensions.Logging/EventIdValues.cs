namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
///     Contains the constant integer values for all EventIds used in the application for structured logging.
/// </summary>
public static class EventIdValues
{
	/// ----------------------------------
	/// System & Error (0 - 9xx)
	/// ----------------------------------
	/// 
	/// <summary>
	/// An invalid or uninitialized event ID.
	/// </summary>
	public const int None = 0;

	/// System & Error
	/// <summary>
	/// Indicates an unexpected error occurred.
	/// </summary>
	public const int UnexpectedError = 900;

	/// ----------------------------------
	/// Player-Driven Events (1000 - 49xx)
	/// ----------------------------------
	/// Sub-category: Game Session (1000 - 10xx)
	public const int PlayerLogin = 1000;
	public const int PlayerLogout = 1001;

	/// Sub-category: Inventory (1100 - 11xx)
	/// <summary>
	/// Indicates an item has been equipped.
	/// </summary>
	public const int PlayerEquipItem = 1100;
	/// <summary>
	/// Indicates an item has been moved within the inventory.
	/// </summary>
	public const int PlayerMoveItem = 1101;
	/// <summary>
	/// Indicates the inventory is full and cannot accept more items.
	/// </summary>
	public const int PlayerInventoryFull = 1102;
	public const int PlayerLootItem = 1103;

	/// Sub-category: Combat and Abilities (1200 - 12xx)
	public const int PlayerUseAbility = 1200;
	public const int PlayerAttack = 1201;
	public const int PlayerTakeDamage = 1202;
	public const int PlayerHeal = 1203;
	public const int PlayerDie = 1204;
	public const int PlayerRespawn = 1205;
	public const int PlayerCriticalHit = 1206;
	public const int PlayerMissedAttack = 1207;
	public const int PlayerStatusEffectApplied = 1208;
	public const int PlayerStatusEffectRemoved = 1209;
	public const int PlayerLevelUp = 1210;
	public const int PlayerSkillIncreased = 1211;
	public const int PlayerAttributeIncreased = 1212;
	public const int PlayerBlock = 1213;
	public const int PlayerDodge = 1214;
	public const int PlayerParry = 1215;

	/// Sub-category: Social Interactions (1300 - 13xx)
	public const int PlayerChatMessageSent = 1300;
	public const int PlayerChatMessageReceived = 1301;
	public const int PlayerChatMessageFailed = 1302;
	public const int PlayerPrivateMessageSent = 1303;
	public const int PlayerPrivateMessageReceived = 1304;
	public const int PlayerPrivateMessageFailed = 1305;
	public const int PlayerEmotePerformed = 1306;
	public const int PlayerFriendRequestSent = 1307;
	public const int PlayerFriendRequestReceived = 1308;
	public const int PlayerFriendRequestAccepted = 1309;
	public const int PlayerFriendRequestDeclined = 1310;
	public const int PlayerBlockedUser = 1311;
	public const int PlayerUnblockedUser = 1312;

	/// Sub-category: Quests and Achievements (1400 - 14xx)

	public const int PlayerQuestAccepted = 1400;
	public const int PlayerQuestCompleted = 1401;
	public const int PlayerQuestFailed = 1402;
	public const int PlayerQuestAbandoned = 1403;
	public const int PlayerAchievementUnlocked = 1404;

	/// ----------------------------------
	/// System-Driven Events (5000 - 69xx)
	/// ----------------------------------
	/// Sub-category: Application Lifecycle (5000 - 50xx)
	/// <summary>
	/// Indicates the application has loaded and is starting up.
	/// </summary>
	public const int ApplicationStarting = 5000;
	public const int ApplicationStarted = 5001;
	public const int ApplicationStopping = 5002;
	public const int ApplicationStopped = 5003;

	/// Sub-category: Configuration and Library File Operations (5100 - 51xx)
	public const int LoadingLibraryFile = 5100;
	public const int LibraryFileLoaded = 5101;
	public const int LibraryFileLoadFailed = 5102;
	public const int LibraryHeaderMismatch = 5103;
	public const int SavingLibraryFile = 5104;
	public const int LibraryFileSaved = 5105;
	public const int LibraryFileSaveFailed = 5106;
	public const int LibraryFileHeaderParsed = 5107;
	public const int LibraryFileHeaderParseFailed = 5108;
	public const int LibraryFileDataParsed = 5109;
	public const int LibraryFileDataParseFailed = 5110;
	public const int ConfigurationFilesEnumerated = 5111;
	public const int ConfigurationFileLoaded = 5112;
	public const int ConfigurationFileLoadFailed = 5113;
	public const int ConfigurationFileSaved = 5114;
	public const int ConfigurationFileSaveFailed = 5115;
	public const int ConfigurationFileCompressionFailed = 5116;
	public const int ConfigurationFileDecompressionFailed = 5117;

	/// Sub-category: Network Operations (5200 - 52xx)
	public const int NetworkListenerStarting = 5200;
	public const int NetworkListenerStarted = 5201;
	public const int NetworkListenerStopping = 5202;
	public const int NetworkListenerStopped = 5203;
	public const int NetworkListenerError = 5204;
	public const int NetworkPacketReceived = 5205;
	public const int NetworkPacketProcessed = 5206;
	public const int NetworkPacketProcessingFailed = 5207;
	public const int NetworkPacketSent = 5208;
	public const int NetworkPacketSendFailed = 5209;
	public const int NetworkConnectionEstablished = 5210;
	public const int NetworkConnectionClosed = 5211;
	public const int NetworkConnectionError = 5212;
	public const int NetworkLatencyDetected = 5213;
	public const int NetworkThroughputMeasured = 5214;
	public const int NetworkConfigurationLoaded = 5215;
	public const int NetworkConfigurationLoadFailed = 5216;
	public const int NetworkConfigurationSaved = 5217;
	public const int NetworkConfigurationSaveFailed = 5218;
	public const int ClientHandshakeReceived = 5219;
	public const int ClientHandshakeSuccessful = 5220;
	public const int ClientHandshakeFailed = 5221;
	public const int ClientConnectionEstablished = 5222;
	public const int ClientConnectionTerminated = 5223;
	public const int ClientConnectionError = 5224;
	public const int MessageReceived = 5225;
	public const int InvalidMessageReceived = 5226;
	public const int MessageProcessed = 5227;
	public const int MessageProcessingFailed = 5228;
	public const int MessageSent = 5229;
	public const int MessageSendFailed = 5230;
	public const int ClientAuthenticationStarted = 5231;
	public const int ClientAuthenticationSucceeded = 5232;
	public const int ClientAuthenticationFailed = 5233;

	/// Sub-category: Database Operations (5300 - 53xx)
	public const int DatabaseConnecting = 5300;
	public const int DatabaseConnected = 5301;
	public const int DatabaseConnectionFailed = 5302;
	public const int MigrationStarted = 5303;
	public const int MigrationCompleted = 5304;
	public const int MigrationFailed = 5305;
	public const int DatabaseQueryExecuted = 5306;
	public const int DatabaseQueryFailed = 5307;
	public const int DatabaseTransactionStarted = 5308;
	public const int DatabaseTransactionCommitted = 5309;
	public const int DatabaseTransactionRolledBack = 5310;
	public const int DatabaseDisconnecting = 5311;
	public const int DatabaseDisconnected = 5312;
	public const int DatabaseDisconnectionFailed = 5313;
	public const int DatabaseConfigurationLoaded = 5314;
	public const int DatabaseConfigurationLoadFailed = 5315;
	public const int DatabaseConfigurationSaved = 5316;
	public const int DatabaseConfigurationSaveFailed = 5317;
	public const int ManagedDatabaseServerStarting = 5318;
	public const int ManagedDatabaseServerStarted = 5319;
	public const int ManagedDatabaseServerStopping = 5320;
	public const int ManagedDatabaseServerStopped = 5321;
	public const int ManagedDatabaseServerError = 5322;
	public const int ManagedDatabaseServerBackupStarted = 5323;
	public const int ManagedDatabaseServerBackupCompleted = 5324;
	public const int ManagedDatabaseServerBackupFailed = 5325;

	/// Sub-category: Persistence Operations (5400 - 54xx)
	public const int PlayerStateSaving = 5400;
	public const int PlayerStateSaved = 5401;
	public const int PlayerStateSaveFailed = 5402;
	public const int PlayerStateLoading = 5403;
	public const int PlayerStateLoaded = 5404;
	public const int PlayerStateLoadFailed = 5405;

	/// Sub-category: World Events (5500 - 55xx)
	public const int WorldStateChanged = 5500;
	public const int WeatherChanged = 5501;
	public const int TimeOfDayChanged = 5502;
	public const int LootGenerated = 5503;
	public const int LootGenerationFailed = 5504;

	/// Sub-cagegory: Entity Events (5600 - 56xx)
	public const int EntitySpawning = 5600;
	public const int EntitySpawned = 5601;
	public const int EntityDespawning = 5602;
	public const int EntityDespawned = 5603;
	public const int EntityStateChanged = 5604;
	public const int EntityInteraction = 5605;
	public const int EntityKilled = 5606;

}
