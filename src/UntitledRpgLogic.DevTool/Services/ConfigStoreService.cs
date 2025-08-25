using System.Collections.Concurrent;
using System.Globalization;
using Microsoft.JSInterop;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Interfaces.Data;
using UntitledRpgLogic.DevTool.Services.Contracts;

namespace UntitledRpgLogic.DevTool.Services;

internal sealed class ConfigStoreService : IConfigStore
{
	private readonly ConcurrentDictionary<Ulid, ITomlConfig> configs = new();
	private readonly IJSRuntime jsRuntime;
	private readonly ILogger<ConfigStoreService> logger;

	public ConfigStoreService(IJSRuntime jsRuntime, ILogger<ConfigStoreService> logger)
	{
		this.jsRuntime = jsRuntime;
		this.logger = logger;

		_ = this.InitializeFromCache().ConfigureAwait(true);
	}

	public ModuleInfoConfig ModuleInfo { get; set; } = new() { Name = string.Empty };

	/// <inheritdoc />
	public string ModuleName
	{
		get => this.ModuleInfo.Name;
		set
		{
			if (this.ModuleInfo.Name == value)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				this.ModuleInfo.Name = string.Empty;
			}
			else
			{
				this.ModuleInfo.Name = value;
			}

			_ = Task.Run(async () =>
					await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleName", this.ModuleInfo.Name).ConfigureAwait(false))
				.ConfigureAwait(true);
			this.OnChange?.Invoke();
		}
	}

	/// <inheritdoc />
	public string ModuleDescription
	{
		get => this.ModuleInfo.Description;
		set
		{
			if (this.ModuleInfo.Description == value)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				this.ModuleInfo.Description = string.Empty;
			}
			else
			{
				this.ModuleInfo.Description = value;
			}

			_ = Task.Run(async () => await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleDescription", this.ModuleInfo.Description)
				.ConfigureAwait(false)).ConfigureAwait(true);
			this.OnChange?.Invoke();
		}
	}

	/// <inheritdoc />
	public string ModuleVersion
	{
		get => this.ModuleInfo.Version;
		set
		{
			if (this.ModuleInfo.Version == value)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				this.ModuleInfo.Version = "1.0.0";
			}
			else
			{
				this.ModuleInfo.Version = value;
			}

			_ = Task.Run(async () =>
					await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleVersion", this.ModuleInfo.Version).ConfigureAwait(false))
				.ConfigureAwait(true);
			this.OnChange?.Invoke();
		}
	}

	/// <inheritdoc />
	public int ModuleVersionNumber
	{
		get => this.ModuleInfo.VersionNumber;
		set
		{
			if (this.ModuleInfo.VersionNumber == value)
			{
				return;
			}

			this.ModuleInfo.VersionNumber = value;
			_ = Task.Run(async () => await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleVersionNumber", this.ModuleInfo.VersionNumber)
				.ConfigureAwait(false)).ConfigureAwait(true);
			this.OnChange?.Invoke();
		}
	}

	/// <inheritdoc />
	public string ModuleIdentifier
	{
		get => this.ModuleInfo.Identifier.ToString();
		set
		{
			if (this.ModuleInfo.Identifier.ToString() == value)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				this.ModuleInfo.Identifier = Ulid.NewUlid();
			}
			else
			{
				if (Ulid.TryParse(value, out var ulid))
				{
					this.ModuleInfo.Identifier = ulid;
				}
				else
				{
					throw new ArgumentException("Invalid ULID format.", nameof(value));
				}
			}


			_ = Task.Run(async () =>
				await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleGuid", this.ModuleInfo.Identifier.ToString())
					.ConfigureAwait(false)).ConfigureAwait(true);
			this.OnChange?.Invoke();
		}
	}

	/// <inheritdoc />
	public event Action? OnChange;

	/// <inheritdoc />
	public bool IsReady { get; private set; }

	/// <inheritdoc />
	public ITomlConfig? GetConfig(Ulid key)
	{
		_ = this.configs.TryGetValue(key, out var config);
		return config;
	}

	/// <inheritdoc />
	public void UpdateConfig(Ulid key, ITomlConfig config) => this.configs[key] = config;

	/// <inheritdoc />
	public IEnumerable<Ulid> GetAllKeys() => this.configs.Keys;

	/// <inheritdoc />
	public AuthorConfig Author { get; } =
		new() { AuthorName = "", Website = "", Identifier = Ulid.NewUlid() };

	/// <inheritdoc />
	public string AuthorIdentifier
	{
		get => this.Author.Identifier.ToString();
		set
		{
			if (this.Author.Identifier.ToString() == value)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				this.Author.Identifier = Ulid.NewUlid();
			}
			else
			{
				if (Ulid.TryParse(value, out var ulid))
				{
					this.Author.Identifier = ulid;
				}
				else
				{
					throw new ArgumentException("Invalid ULID format.", nameof(value));
				}
			}

			_ = Task.Run(async () => await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "authorGuid", this.Author.Identifier.ToString())
					.ConfigureAwait(false))
				.ConfigureAwait(true);
			this.OnChange?.Invoke();
		}
	}

	/// <inheritdoc />
	public string AuthorName
	{
		get => this.Author.AuthorName;
		set
		{
			if (this.Author.AuthorName == value)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				this.Author.AuthorName = string.Empty;
			}

			this.Author.AuthorName = value;
			_ = Task.Run(async () =>
					await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "authorName", this.Author.AuthorName).ConfigureAwait(false))
				.ConfigureAwait(true);
			this.OnChange?.Invoke();
		}
	}

	/// <inheritdoc />
	public string AuthorWebsite
	{
		get => this.Author.Website;
		set
		{
			if (this.Author.Website == value)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				this.Author.Website = string.Empty;
			}

			this.Author.Website = value;

			_ = Task.Run(async () =>
					await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "authorUrl", this.Author.Website).ConfigureAwait(false))
				.ConfigureAwait(true);
			this.OnChange?.Invoke();
		}
	}

	private async Task InitializeFromCache()
	{
		this.Author.AuthorName = await this.jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "authorName").ConfigureAwait(false);
		this.Author.Website = await this.jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "authorUrl").ConfigureAwait(false);
		this.ModuleInfo.Name = await this.jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "moduleName").ConfigureAwait(false);
		this.ModuleInfo.Description =
			await this.jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "moduleDescription").ConfigureAwait(false);
		this.ModuleInfo.Version = await this.jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "moduleVersion").ConfigureAwait(false);
		this.ModuleInfo.VersionNumber =
			await this.jsRuntime.InvokeAsync<int>("LoadIntegerFromStorage", "moduleVersionNumber").ConfigureAwait(false);
		var authorIdentifier = await this.jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "authorIdentifier").ConfigureAwait(false);
		var moduleIdentifier = await this.jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "authorIdentifier").ConfigureAwait(false);

		this.logger.LogInformation("Checking for cached AuthorGuid: {AuthorGuid}, ModuleGuid: {ModuleGuid}",
			authorIdentifier, moduleIdentifier);
		this.Author.Identifier = string.IsNullOrWhiteSpace(authorIdentifier)
			? Ulid.NewUlid()
			: Ulid.Parse(authorIdentifier, CultureInfo.InvariantCulture);
		this.ModuleInfo.Identifier = string.IsNullOrWhiteSpace(moduleIdentifier)
			? Ulid.NewUlid()
			: Ulid.Parse(moduleIdentifier, CultureInfo.InvariantCulture);

		await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "authorGuid", this.Author.Identifier.ToString()).ConfigureAwait(false);
		await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleGuid", this.ModuleInfo.Identifier.ToString()).ConfigureAwait(false);

		this.logger.LogInformation("Initialized: {AuthorData}, {ModuleData}",
			this.Author, this.ModuleInfo);
		this.IsReady = true;
		this.OnChange?.Invoke();
	}
}
