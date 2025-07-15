using System.Collections.Concurrent;
using Microsoft.JSInterop;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Interfaces;
using UntitledRpgLogic.DevTool.Services.Contracts;

namespace UntitledRpgLogic.DevTool.Services;

public class ConfigStoreService : IConfigStore
{
    private readonly ConcurrentDictionary<Guid, ITomlConfig> _configs = new();
    private readonly IJSRuntime _jsRuntime;
    private readonly ILogger<ConfigStoreService> _logger;

    public ConfigStoreService(IJSRuntime jsRuntime, ILogger<ConfigStoreService> logger)
    {
        _jsRuntime = jsRuntime;
        _logger = logger;

        InitializeFromCache().ConfigureAwait(true);
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

            _jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleName", this.ModuleInfo.Name)
                .ConfigureAwait(true);
            OnChange?.Invoke();
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

            _jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleDescription", this.ModuleInfo.Description)
                .ConfigureAwait(true);
            OnChange?.Invoke();
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

            _jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleVersion", this.ModuleInfo.Version)
                .ConfigureAwait(true);
            OnChange?.Invoke();
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
            _jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleVersionNumber", this.ModuleInfo.VersionNumber)
                .ConfigureAwait(true);
            OnChange?.Invoke();
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
                if (Guid.TryParse(value, out Guid guid))
                {
                    this.ModuleInfo.Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid GUID format.", nameof(value));
                }
            }

            _jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleGuid", this.ModuleInfo.Id.ToString())
                .ConfigureAwait(true);
            OnChange?.Invoke();
        }
    }

    /// <inheritdoc />
    public event Action? OnChange;

    /// <inheritdoc />
    public bool IsReady { get; private set; } = false;

    /// <inheritdoc />
    public ITomlConfig? GetConfig(Guid key)
    {
        _ = _configs.TryGetValue(key, out ITomlConfig? config);
        return config;
    }

    /// <inheritdoc />
    public void UpdateConfig(Guid key, ITomlConfig config)
    {
        _configs[key] = config;
    }

    /// <inheritdoc />
    public IEnumerable<Guid> GetAllKeys() => _configs.Keys;

    /// <inheritdoc />
    public AuthorConfig Author { get; } =
        new() { AuthorName = "", Website = "", AuthorId = Guid.NewGuid() };

    /// <inheritdoc />
    public string AuthorGuid
    {
        get => Author.AuthorId.ToString();
        set
        {
            if (Author.AuthorId.ToString() == value)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                Author.AuthorId = Guid.NewGuid();
            }
            else
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    Author.AuthorId = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid GUID format.", nameof(value));
                }
            }

            _jsRuntime.InvokeVoidAsync("SaveToStorage", "authorGuid", Author.AuthorId.ToString())
                .ConfigureAwait(true);
            OnChange?.Invoke();
        }
    }

    /// <inheritdoc />
    public string AuthorName
    {
        get => Author.AuthorName;
        set
        {
            if (Author.AuthorName == value)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                Author.AuthorName = string.Empty;
            }

            Author.AuthorName = value;
            _jsRuntime.InvokeVoidAsync("SaveToStorage", "authorName", Author.AuthorName)
                .ConfigureAwait(true);
            OnChange?.Invoke();
        }
    }

    /// <inheritdoc />
    public string AuthorWebsite
    {
        get => Author.Website;
        set
        {
            if (Author.Website == value)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                Author.Website = string.Empty;
            }

            Author.Website = value;
            _jsRuntime.InvokeVoidAsync("SaveToStorage", "authorUrl", Author.Website)
                .ConfigureAwait(true);
            OnChange?.Invoke();
        }
    }

    private async Task InitializeFromCache()
    {
        Author.AuthorName = await _jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "authorName");
        Author.Website = await _jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "authorUrl");
        ModuleInfo.Name = await _jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "moduleName");
        ModuleInfo.Description = await _jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "moduleDescription");
        ModuleInfo.Version = await _jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "moduleVersion");
        ModuleInfo.VersionNumber = await _jsRuntime.InvokeAsync<int>("LoadIntegerFromStorage", "moduleVersionNumber");
        string authorGuid = await _jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "authorGuid");
        string moduleGuid = await _jsRuntime.InvokeAsync<string>("LoadStringFromStorage", "moduleGuid");

        _logger.LogInformation("Checking for cached AuthorGuid: {AuthorGuid}, ModuleGuid: {ModuleGuid}",
            authorGuid, moduleGuid);
        Author.AuthorId = string.IsNullOrWhiteSpace(authorGuid) ? Guid.NewGuid() : Guid.Parse(authorGuid);
        ModuleInfo.Id = string.IsNullOrWhiteSpace(moduleGuid) ? Guid.NewGuid() : Guid.Parse(moduleGuid);

        await _jsRuntime.InvokeVoidAsync("SaveToStorage", "authorGuid", Author.AuthorId.ToString());
        await _jsRuntime.InvokeVoidAsync("SaveToStorage", "moduleGuid", ModuleInfo.Id.ToString());

        _logger.LogInformation("Initialized: {AuthorData}, {ModuleData}",
            Author, ModuleInfo);
        this.IsReady = true;
        OnChange?.Invoke();
    }
}
