using System.Collections.Concurrent;
using Microsoft.JSInterop;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Interfaces;
using UntitledRpgLogic.DevTool.Services.Contracts;

namespace UntitledRpgLogic.DevTool.Services;

internal sealed class ConfigStoreService : IConfigStore
{
	private readonly ConcurrentDictionary<Guid, ITomlConfig> configs = new();
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

			_ = Task.Run(async () => await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleName", this.ModuleInfo.Name).ConfigureAwait(false)).ConfigureAwait(true);
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
			_ = Task.Run(async () => await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleDescription", this.ModuleInfo.Description).ConfigureAwait(false)).ConfigureAwait(true);
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

			_ = Task.Run(async () => await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleVersion", this.ModuleInfo.Version).ConfigureAwait(false)).ConfigureAwait(true);
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
			_ = Task.Run(async () => await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleVersionNumber", this.ModuleInfo.VersionNumber).ConfigureAwait(false)).ConfigureAwait(true);
			this.OnChange?.Invoke();
		}
	}

	/// <inheritdoc />
	public string ModuleGuid
	{
		get => this.ModuleInfo.Id.ToString();
		set
		{
			if (this.ModuleInfo.Id.ToString() == value)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				this.ModuleInfo.Id = Guid.NewGuid();
			}
			else
			{
				if (Guid.TryParse(value, out var guid))
				{
					this.ModuleInfo.Id = guid;
				}
				else
				{
					throw new ArgumentException("Invalid GUID format.", nameof(value));
				}
			}


			_ = Task.Run(async () =>
					await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleGuid", this.ModuleInfo.Id.ToString())
				.ConfigureAwait(false)).ConfigureAwait(true);
			this.OnChange?.Invoke();
		}
	}

	/// <inheritdoc />
	public event Action? OnChange;

	/// <inheritdoc />
	public bool IsReady { get; private set; }

	/// <inheritdoc />
	public ITomlConfig? GetConfig(Guid key)
	{
		_ = this.configs.TryGetValue(key, out var config);
		return config;
	}

	/// <inheritdoc />
	public void UpdateConfig(Guid key, ITomlConfig config) => this.configs[key] = config;

	/// <inheritdoc />
	public IEnumerable<Guid> GetAllKeys() => this.configs.Keys;

	/// <inheritdoc />
	public AuthorConfig Author { get; } =
		new() { AuthorName = "", Website = "", AuthorId = Guid.NewGuid() };

	/// <inheritdoc />
	public string AuthorGuid
	{
		get => this.Author.AuthorId.ToString();
		set
		{
			if (this.Author.AuthorId.ToString() == value)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				this.Author.AuthorId = Guid.NewGuid();
			}
			else
			{
				if (Guid.TryParse(value, out var guid))
				{
					this.Author.AuthorId = guid;
				}
				else
				{
					throw new ArgumentException("Invalid GUID format.", nameof(value));
				}
			}

			_ = Task.Run(async () => await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "authorGuid", this.Author.AuthorId.ToString()).ConfigureAwait(false))
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
			_ = Task.Run(async () => await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "authorName", this.Author.AuthorName).ConfigureAwait(false)).ConfigureAwait(true);
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

			_ = Task.Run(async () => await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "authorUrl", this.Author.Website).ConfigureAwait(false)).ConfigureAwait(true);
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
		var authorGuid = await this.jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "authorGuid").ConfigureAwait(false);
		var moduleGuid = await this.jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "moduleGuid").ConfigureAwait(false);

		this.logger.LogInformation("Checking for cached AuthorGuid: {AuthorGuid}, ModuleGuid: {ModuleGuid}",
			authorGuid, moduleGuid);
		this.Author.AuthorId = string.IsNullOrWhiteSpace(authorGuid) ? Guid.NewGuid() : Guid.Parse(authorGuid);
		this.ModuleInfo.Id = string.IsNullOrWhiteSpace(moduleGuid) ? Guid.NewGuid() : Guid.Parse(moduleGuid);

		await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "authorGuid", this.Author.AuthorId.ToString()).ConfigureAwait(false);
		await this.jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleGuid", this.ModuleInfo.Id.ToString()).ConfigureAwait(false);

		this.logger.LogInformation("Initialized: {AuthorData}, {ModuleData}",
			this.Author, this.ModuleInfo);
		this.IsReady = true;
		this.OnChange?.Invoke();
	}
}
