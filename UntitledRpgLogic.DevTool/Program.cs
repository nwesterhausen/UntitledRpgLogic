using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UntitledRpgLogic.DevTool;
using UntitledRpgLogic.DevTool.Services;
using UntitledRpgLogic.DevTool.Services.Contracts;
using UntitledRpgLogic.Infrastructure.Configuration;
using Serilog;
using Serilog.Extensions.Logging;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.BrowserConsole()
    .CreateLogger();

try
{
    WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");

    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

    builder.Services.AddConfigurationInfrastructure();
    builder.Services.AddSingleton<IConfigStore, ConfigStoreService>();

    // Replace Microsoft logger for Serilog
    builder.Logging.ClearProviders();
    builder.Logging.AddProvider(new SerilogLoggerProvider(Log.Logger));


    await builder.Build().RunAsync();
} catch (Exception ex)
{
    Log.Fatal(ex, "An error occurred during application startup");
}
finally
{
    Log.CloseAndFlush();
}
