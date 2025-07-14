using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UntitledRpgLogic.DevTool;
using UntitledRpgLogic.DevTool.Services;
using UntitledRpgLogic.DevTool.Services.Contracts;
using UntitledRpgLogic.Infrastructure.Configuration;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.BrowserConsole()
    .CreateLogger();

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddConfigurationInfrastructure();
builder.Services.AddSingleton<IConfigStore, ConfigStoreService>();

await builder.Build().RunAsync();
