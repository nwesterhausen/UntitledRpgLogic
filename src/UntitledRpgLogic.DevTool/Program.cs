using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;
using Serilog.Extensions.Logging;
using UntitledRpgLogic.DevTool;
using UntitledRpgLogic.DevTool.Services;
using UntitledRpgLogic.DevTool.Services.Contracts;
using UntitledRpgLogic.Infrastructure.Configuration;

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.WriteTo.BrowserConsole()
	.CreateLogger();

try
{
	var builder = WebAssemblyHostBuilder.CreateDefault(args);
	builder.RootComponents.Add<App>("#app");
	builder.RootComponents.Add<HeadOutlet>("head::after");

	_ = builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

	_ = builder.Services.AddConfigurationInfrastructure();
	_ = builder.Services.AddSingleton<IConfigStore, ConfigStoreService>();

	// Replace Microsoft logger for Serilog
#pragma warning disable CA2000
	_ = builder.Logging.ClearProviders();
	_ = builder.Logging.AddProvider(new SerilogLoggerProvider(Log.Logger));
#pragma warning restore CA2000

	await builder.Build().RunAsync().ConfigureAwait(false);
}
catch (Exception ex)
{
	Log.Fatal(ex, "An error occurred during application startup");
	throw;
}
finally
{
	await Log.CloseAndFlushAsync().ConfigureAwait(false);
}
