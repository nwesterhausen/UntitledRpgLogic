namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
///     Contains the constant integer values for all EventIds used in the application for structured logging.
/// </summary>
public static class EventIdValues
{
    /// <summary>
    ///     An invalid or uninitialized event ID.
    /// </summary>
    public const int None = 0;

    /// ----------------------------------
    /// System and Error (0 - 9xx)
    /// ----------------------------------
    /// <summary>
    ///     Indicates an unexpected error occurred.
    /// </summary>
    public const int UnexpectedError = 900;

    /// <summary>
    ///     Indicates a validation error occurred.
    /// </summary>
    public const int ValidationError = 901;

    /// <summary>
    ///     Indicates an operation is not supported.
    /// </summary>
    public const int OperationNotSupported = 902;

    /// ----------------------------------
    /// Player-Driven Events (1000 - 49xx)
    /// ----------------------------------
    /// Sub-category: Game Session (1000 - 10xx)
    /// <summary>
    ///     Indicates a player has logged in.
    /// </summary>
    public const int PlayerLogin = 1000;

    /// <summary>
    ///     Indicates a player has logged out.
    /// </summary>
    public const int PlayerLogout = 1001;

    /// Sub-category: Inventory (1100 - 11xx)
    /// <summary>
    ///     Indicates an item has been equipped.
    /// </summary>
    public const int PlayerEquipItem = 1100;

    /// <summary>
    ///     Indicates an item has been moved within the inventory.
    /// </summary>
    public const int PlayerMoveItem = 1101;

    /// <summary>
    ///     Indicates the inventory is full and cannot accept more items.
    /// </summary>
    public const int PlayerInventoryFull = 1102;





    /// <summary>
    ///     Indicates an item has been looted.
    /// </summary>
    public const int PlayerLootItem = 1103;

    /// Sub-category: Combat and Abilities (1200 - 12xx)
    /// <summary>
    ///     Indicates an ability has been used.
    /// </summary>
    public const int PlayerUseAbility = 1200;

    /// <summary>
    ///     Indicates a player has made an attack.
    /// </summary>
    public const int PlayerAttack = 1201;

    /// <summary>
    ///     Indicates a player has taken damage.
    /// </summary>
    public const int PlayerTakeDamage = 1202;

    /// <summary>
    ///     Indicates a player has healed.
    /// </summary>
    public const int PlayerHeal = 1203;

    /// <summary>
    ///     Indicates a player has died.
    /// </summary>
    public const int PlayerDie = 1204;

    /// <summary>
    ///     Indicates a player has respawned.
    /// </summary>
    public const int PlayerRespawn = 1205;

    /// <summary>
    ///     Indicates a player has performed a critical hit.
    /// </summary>
    public const int PlayerCriticalHit = 1206;

    /// <summary>
    ///     Indicates a player has missed an attack.
    /// </summary>
    public const int PlayerMissedAttack = 1207;

    /// <summary>
    ///     Indicates a player has had a status effect applied.
    /// </summary>
    public const int PlayerStatusEffectApplied = 1208;

    /// <summary>
    ///     Indicates a player has had a status effect removed.
    /// </summary>
    public const int PlayerStatusEffectRemoved = 1209;

    /// <summary>
    ///     Indicates a player has leveled up.
    /// </summary>
    public const int PlayerLevelUp = 1210;

    /// <summary>
    ///     Indicates a player has increased a skill.
    /// </summary>
    public const int PlayerSkillIncreased = 1211;

    /// <summary>
    ///     Indicates a player has increased an attribute.
    /// </summary>
    public const int PlayerAttributeIncreased = 1212;

    /// <summary>
    ///     Indicates a player has blocked an attack.
    /// </summary>
    public const int PlayerBlock = 1213;

    /// <summary>
    ///     Indicates a player has dodged an attack.
    /// </summary>
    public const int PlayerDodge = 1214;

    /// <summary>
    ///     Indicates a player has parried an attack.
    /// </summary>
    public const int PlayerParry = 1215;

    /// Sub-category: Social Interactions (1300 - 13xx)
    /// <summary>
    ///     Indicates a player has sent a chat message.
    /// </summary>
    public const int PlayerChatMessageSent = 1300;

    /// <summary>
    ///     Indicates a player has received a chat message.
    /// </summary>
    public const int PlayerChatMessageReceived = 1301;

    /// <summary>
    ///     Indicates a player has failed to send a chat message.
    /// </summary>
    public const int PlayerChatMessageFailed = 1302;

    /// <summary>
    ///     Indicates a player has sent a private message.
    /// </summary>
    public const int PlayerPrivateMessageSent = 1303;

    /// <summary>
    ///     Indicates a player has received a private message.
    /// </summary>
    public const int PlayerPrivateMessageReceived = 1304;

    /// <summary>
    ///     Indicates a player has failed to send a private message.
    /// </summary>
    public const int PlayerPrivateMessageFailed = 1305;

    /// <summary>
    ///     Indicates a player has performed an emote.
    /// </summary>
    public const int PlayerEmotePerformed = 1306;

    /// <summary>
    ///     Indicates a player has sent a friend request.
    /// </summary>
    public const int PlayerFriendRequestSent = 1307;

    /// <summary>
    ///     Indicates a player has received a friend request.
    /// </summary>
    public const int PlayerFriendRequestReceived = 1308;

    /// <summary>
    ///     Indicates a player has accepted a friend request.
    /// </summary>
    public const int PlayerFriendRequestAccepted = 1309;

    /// <summary>
    ///     Indicates a player has declined a friend request.
    /// </summary>
    public const int PlayerFriendRequestDeclined = 1310;

    /// <summary>
    ///     Indicates a player has blocked a user.
    /// </summary>
    public const int PlayerBlockedUser = 1311;

    /// <summary>
    ///     Indicates a player has unblocked a user.
    /// </summary>
    public const int PlayerUnblockedUser = 1312;

    /// Sub-category: Quests and Achievements (1400 - 14xx)
    /// <summary>
    ///     Indicates a player has accepted a quest.
    /// </summary>
    public const int PlayerQuestAccepted = 1400;

    /// <summary>
    ///     Indicates a player has completed a quest.
    /// </summary>
    public const int PlayerQuestCompleted = 1401;

    /// <summary>
    ///     Indicates a player has failed a quest.
    /// </summary>
    public const int PlayerQuestFailed = 1402;

    /// <summary>
    ///     Indicates a player has abandoned a quest.
    /// </summary>
    public const int PlayerQuestAbandoned = 1403;

    /// <summary>
    ///     Indicates a player has unlocked an achievement.
    /// </summary>
    public const int PlayerAchievementUnlocked = 1404;

    /// ----------------------------------
    /// System-Driven Events (5000 - 69xx)
    /// ----------------------------------
    /// Sub-category: Application Lifecycle (5000 - 50xx)
    /// <summary>
    ///     Indicates the application has loaded and is starting up.
    /// </summary>
    public const int ApplicationStarting = 5000;

    /// <summary>
    ///     Indicates the application has started.
    /// </summary>
    public const int ApplicationStarted = 5001;

    /// <summary>
    ///     Indicates the application is stopping.
    /// </summary>
    public const int ApplicationStopping = 5002;

    /// <summary>
    ///     Indicates the application has stopped.
    /// </summary>
    public const int ApplicationStopped = 5003;

    /// Sub-category: Configuration and Library File Operations (5100 - 51xx)
    /// <summary>
    ///     Indicates the application is loading a library file.
    /// </summary>
    public const int LoadingLibraryFile = 5100;

    /// <summary>
    ///     Indicates the application has loaded a library file.
    /// </summary>
    public const int LibraryFileLoaded = 5101;

    /// <summary>
    ///     Indicates the application has failed to load a library file.
    /// </summary>
    public const int LibraryFileLoadFailed = 5102;

    /// <summary>
    ///     Indicates the application has detected a mismatch in the library file header.
    /// </summary>
    public const int LibraryHeaderMismatch = 5103;

    /// <summary>
    ///     Indicates the application is saving a library file.
    /// </summary>
    public const int SavingLibraryFile = 5104;

    /// <summary>
    ///     Indicates the application has saved a library file.
    /// </summary>
    public const int LibraryFileSaved = 5105;

    /// <summary>
    ///     Indicates the application has failed to save a library file.
    /// </summary>
    public const int LibraryFileSaveFailed = 5106;

    /// <summary>
    ///     Indicates the application has parsed the library file header.
    /// </summary>
    public const int LibraryFileHeaderParsed = 5107;

    /// <summary>
    ///     Indicates the application has failed to parse the library file header.
    /// </summary>
    public const int LibraryFileHeaderParseFailed = 5108;

    /// <summary>
    ///     Indicates the application has parsed the library file data.
    /// </summary>
    public const int LibraryFileDataParsed = 5109;

    /// <summary>
    ///     Indicates the application has failed to parse the library file data.
    /// </summary>
    public const int LibraryFileDataParseFailed = 5110;

    /// <summary>
    ///     Indicates the application has enumerated configuration files.
    /// </summary>
    public const int ConfigurationFilesEnumerated = 5111;

    /// <summary>
    ///     Indicates the application has loaded a configuration file.
    /// </summary>
    public const int ConfigurationFileLoaded = 5112;

    /// <summary>
    ///     Indicates the application has failed to load a configuration file.
    /// </summary>
    public const int ConfigurationFileLoadFailed = 5113;

    /// <summary>
    ///     Indicates the application has saved a configuration file.
    /// </summary>
    public const int ConfigurationFileSaved = 5114;

    /// <summary>
    ///     Indicates the application has failed to save a configuration file.
    /// </summary>
    public const int ConfigurationFileSaveFailed = 5115;

    /// <summary>
    ///     Indicates the application has failed to compress a configuration file.
    /// </summary>
    public const int ConfigurationFileCompressionFailed = 5116;

    /// <summary>
    ///     Indicates the application has failed to decompress a configuration file.
    /// </summary>
    public const int ConfigurationFileDecompressionFailed = 5117;

    /// Sub-category: Network Operations (5200 - 52xx)
    /// <summary>
    ///     Indicates the application is starting a network listener.
    /// </summary>
    public const int NetworkListenerStarting = 5200;

    /// <summary>
    ///     Indicates the application has started a network listener.
    /// </summary>
    public const int NetworkListenerStarted = 5201;

    /// <summary>
    ///     Indicates the application is stopping a network listener.
    /// </summary>
    public const int NetworkListenerStopping = 5202;

    /// <summary>
    ///     Indicates the application has stopped a network listener.
    /// </summary>
    public const int NetworkListenerStopped = 5203;

    /// <summary>
    ///     Indicates the application has encountered an error with a network listener.
    /// </summary>
    public const int NetworkListenerError = 5204;

    /// <summary>
    ///     Indicates the application has received a network packet.
    /// </summary>
    public const int NetworkPacketReceived = 5205;

    /// <summary>
    ///     Indicates the application has processed a network packet.
    /// </summary>
    public const int NetworkPacketProcessed = 5206;

    /// <summary>
    ///     Indicates the application has failed to process a network packet.
    /// </summary>
    public const int NetworkPacketProcessingFailed = 5207;

    /// <summary>
    ///     Indicates the application has sent a network packet.
    /// </summary>
    public const int NetworkPacketSent = 5208;

    /// <summary>
    ///     Indicates the application has failed to send a network packet.
    /// </summary>
    public const int NetworkPacketSendFailed = 5209;

    /// <summary>
    ///     Indicates the application has established a network connection.
    /// </summary>
    public const int NetworkConnectionEstablished = 5210;

    /// <summary>
    ///     Indicates the application has closed a network connection.
    /// </summary>
    public const int NetworkConnectionClosed = 5211;

    /// <summary>
    ///     Indicates the application has encountered an error with a network connection.
    /// </summary>
    public const int NetworkConnectionError = 5212;

    /// <summary>
    ///     Indicates the application has detected a network latency.
    /// </summary>
    public const int NetworkLatencyDetected = 5213;

    /// <summary>
    ///     Indicates the application has measured network throughput.
    /// </summary>
    public const int NetworkThroughputMeasured = 5214;

    /// <summary>
    ///     Indicates the application has loaded network configuration.
    /// </summary>
    public const int NetworkConfigurationLoaded = 5215;

    /// <summary>
    ///     Indicates the application has failed to load network configuration.
    /// </summary>
    public const int NetworkConfigurationLoadFailed = 5216;

    /// <summary>
    ///     Indicates the application has saved network configuration.
    /// </summary>
    public const int NetworkConfigurationSaved = 5217;

    /// <summary>
    ///     Indicates the application has failed to save network configuration.
    /// </summary>
    public const int NetworkConfigurationSaveFailed = 5218;

    /// <summary>
    ///     Indicates the application has received a client handshake.
    /// </summary>
    public const int ClientHandshakeReceived = 5219;

    /// <summary>
    ///     Indicates the application has successfully completed a client handshake.
    /// </summary>
    public const int ClientHandshakeSuccessful = 5220;

    /// <summary>
    ///     Indicates the application has failed to complete a client handshake.
    /// </summary>
    public const int ClientHandshakeFailed = 5221;

    /// <summary>
    ///     Indicates the application has established a client connection.
    /// </summary>
    public const int ClientConnectionEstablished = 5222;

    /// <summary>
    ///     Indicates the application has terminated a client connection.
    /// </summary>
    public const int ClientConnectionTerminated = 5223;

    /// <summary>
    ///     Indicates the application has encountered an error during a client connection.
    /// </summary>
    public const int ClientConnectionError = 5224;

    /// <summary>
    ///     Indicates the application has received a message.
    /// </summary>
    public const int MessageReceived = 5225;

    /// <summary>
    ///     Indicates the application has received an invalid message.
    /// </summary>
    public const int InvalidMessageReceived = 5226;

    /// <summary>
    ///     Indicates the application has processed a message.
    /// </summary>
    public const int MessageProcessed = 5227;

    /// <summary>
    ///     Indicates the application has failed to process a message.
    /// </summary>
    public const int MessageProcessingFailed = 5228;

    /// <summary>
    ///     Indicates the application has sent a message.
    /// </summary>
    public const int MessageSent = 5229;

    /// <summary>
    ///     Indicates the application has failed to send a message.
    /// </summary>
    public const int MessageSendFailed = 5230;

    /// <summary>
    ///     Indicates the application has started client authentication.
    /// </summary>
    public const int ClientAuthenticationStarted = 5231;

    /// <summary>
    ///     Indicates the application has successfully completed client authentication.
    /// </summary>
    public const int ClientAuthenticationSucceeded = 5232;

    /// <summary>
    ///     Indicates the application has failed to complete client authentication.
    /// </summary>
    public const int ClientAuthenticationFailed = 5233;

    /// Sub-category: Database Operations (5300 - 53xx)
    /// <summary>
    ///     Indicates the application is attempting to connect to the database.
    /// </summary>
    public const int DatabaseConnecting = 5300;

    /// <summary>
    ///     Indicates the application has successfully connected to the database.
    /// </summary>
    public const int DatabaseConnected = 5301;

    /// <summary>
    ///     Indicates the application has failed to connect to the database.
    /// </summary>
    public const int DatabaseConnectionFailed = 5302;

    /// <summary>
    ///     Indicates the application is attempting to migrate the database.
    /// </summary>
    public const int MigrationStarted = 5303;

    /// <summary>
    ///     Indicates the application has successfully migrated the database.
    /// </summary>
    public const int MigrationCompleted = 5304;

    /// <summary>
    ///     Indicates the application has failed to migrate the database.
    /// </summary>
    public const int MigrationFailed = 5305;

    /// <summary>
    ///     Indicates the application has executed a database query.
    /// </summary>
    public const int DatabaseQueryExecuted = 5306;

    /// <summary>
    ///     Indicates the application has failed to execute a database query.
    /// </summary>
    public const int DatabaseQueryFailed = 5307;

    /// <summary>
    ///     Indicates the application has started a database transaction.
    /// </summary>
    public const int DatabaseTransactionStarted = 5308;

    /// <summary>
    ///     Indicates the application has committed a database transaction.
    /// </summary>
    public const int DatabaseTransactionCommitted = 5309;

    /// <summary>
    ///     Indicates the application has rolled back a database transaction.
    /// </summary>
    public const int DatabaseTransactionRolledBack = 5310;

    /// <summary>
    ///     Indicates the application is disconnecting from the database.
    /// </summary>
    public const int DatabaseDisconnecting = 5311;

    /// <summary>
    ///     Indicates the application has disconnected from the database.
    /// </summary>
    public const int DatabaseDisconnected = 5312;

    /// <summary>
    ///     Indicates the application failed to disconnect from the database.
    /// </summary>
    public const int DatabaseDisconnectionFailed = 5313;

    /// <summary>
    ///     Indicates the application has loaded the database configuration.
    /// </summary>
    public const int DatabaseConfigurationLoaded = 5314;

    /// <summary>
    ///     Indicates the application failed to load the database configuration.
    /// </summary>
    public const int DatabaseConfigurationLoadFailed = 5315;

    /// <summary>
    ///     Indicates the application has saved the database configuration.
    /// </summary>
    public const int DatabaseConfigurationSaved = 5316;

    /// <summary>
    ///     Indicates the application failed to save the database configuration.
    /// </summary>
    public const int DatabaseConfigurationSaveFailed = 5317;

    /// <summary>
    ///     Indicates the application is starting the managed database server.
    /// </summary>
    public const int ManagedDatabaseServerStarting = 5318;

    /// <summary>
    ///     Indicates the application has started the managed database server.
    /// </summary>
    public const int ManagedDatabaseServerStarted = 5319;

    /// <summary>
    ///     Indicates the application is stopping the managed database server.
    /// </summary>
    public const int ManagedDatabaseServerStopping = 5320;

    /// <summary>
    ///     Indicates the application has stopped the managed database server.
    /// </summary>
    public const int ManagedDatabaseServerStopped = 5321;

    /// <summary>
    ///     Indicates the application has encountered an error with the managed database server.
    /// </summary>
    public const int ManagedDatabaseServerError = 5322;

    /// <summary>
    ///     Indicates the application has started a backup of the managed database server.
    /// </summary>
    public const int ManagedDatabaseServerBackupStarted = 5323;

    /// <summary>
    ///     Indicates the application has completed a backup of the managed database server.
    /// </summary>
    public const int ManagedDatabaseServerBackupCompleted = 5324;

    /// <summary>
    ///     Indicates the application has failed to backup the managed database server.
    /// </summary>
    public const int ManagedDatabaseServerBackupFailed = 5325;

    /// Sub-category: Persistence Operations (5400 - 54xx)
    /// <summary>
    ///     Indicates the application is saving the player's state.
    /// </summary>
    public const int PlayerStateSaving = 5400;

    /// <summary>
    ///     Indicates the application has completed saving the player's state.
    /// </summary>
    public const int PlayerStateSaved = 5401;

    /// <summary>
    ///     Indicates the application has failed to save the player's state.
    /// </summary>
    public const int PlayerStateSaveFailed = 5402;

    /// <summary>
    ///     Indicates the application is loading the player's state.
    /// </summary>
    public const int PlayerStateLoading = 5403;

    /// <summary>
    ///     Indicates the application has completed loading the player's state.
    /// </summary>
    public const int PlayerStateLoaded = 5404;

    /// <summary>
    ///     Indicates the application has failed to load the player's state.
    /// </summary>
    public const int PlayerStateLoadFailed = 5405;

    /// Sub-category: World Events (5500 - 55xx)
    /// <summary>
    ///     Indicates the application has changed the world state.
    /// </summary>
    public const int WorldStateChanged = 5500;

    /// <summary>
    ///     Indicates the application has changed the weather.
    /// </summary>
    public const int WeatherChanged = 5501;

    /// <summary>
    ///     Indicates the application has changed the time of day.
    /// </summary>
    public const int TimeOfDayChanged = 5502;

    /// <summary>
    ///     Indicates the application has generated loot.
    /// </summary>
    public const int LootGenerated = 5503;

    /// <summary>
    ///     Indicates the application has failed to generate loot.
    /// </summary>
    public const int LootGenerationFailed = 5504;

    /// Sub-cagegory: Entity Events (5600 - 56xx)
    /// <summary>
    ///     Indicates the application is spawning an entity.
    /// </summary>
    public const int EntitySpawning = 5600;

    /// <summary>
    ///     Indicates the application has spawned an entity.
    /// </summary>
    public const int EntitySpawned = 5601;

    /// <summary>
    ///     Indicates the application is despawning an entity.
    /// </summary>
    public const int EntityDespawning = 5602;

    /// <summary>
    ///     Indicates the application has despawned an entity.
    /// </summary>
    public const int EntityDespawned = 5603;

    /// <summary>
    ///     Indicates the application has changed the state of an entity.
    /// </summary>
    public const int EntityStateChanged = 5604;

    /// <summary>
    ///     Indicates an entity has interacted with another entity.
    /// </summary>
    public const int EntityInteraction = 5605;

    /// <summary>
    ///     Indicates an entity has been killed.
    /// </summary>
    public const int EntityKilled = 5606;
}
